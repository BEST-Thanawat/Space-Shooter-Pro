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
    [SerializeField]
    private GameObject[] _powerups;
    private bool _stopSpawning = false;
    
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
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

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            Instantiate(_powerups[Random.Range(0,3)], new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
}
