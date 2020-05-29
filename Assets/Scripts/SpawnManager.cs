using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    public float waitTime = 5f;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemy, new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(waitTime);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
