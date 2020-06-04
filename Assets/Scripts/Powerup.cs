using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private PowerUp PowerUpType;
    // Update is called once per frame
    [SerializeField]
    private AudioClip audioClip;
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                switch(PowerUpType)
                {
                    case PowerUp.TripleShot:
                        player.TripleShotActive();
                        break;
                    case PowerUp.Speed:
                        player.SpeedActive();
                        break;
                    case PowerUp.Shield:
                        player.ShieldActive();
                        break;
                    default:
                        break;
                }
                //
            }

            Destroy(gameObject);
        }
    }
}

public enum PowerUp
{
    TripleShot,
    Speed,
    Shield
}