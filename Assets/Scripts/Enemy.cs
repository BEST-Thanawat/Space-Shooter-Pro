using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;
    private Animator _anim;

    private AudioSource _audioSource;
    private void Start()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null) Debug.LogError("The Player Animator is null.");

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (_player == null) Debug.LogError("The Player is null.");

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) Debug.LogError("AudioSource is null.");
    }
    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void Run()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5f)
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_player != null) _player.Damage();
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();

            Destroy(gameObject, 1.0f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
                _player.AddScore(10);

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();

            Destroy(gameObject, 1.0f);
        }
    }
}
