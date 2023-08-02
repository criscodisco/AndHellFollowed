using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject enemyPrefab;

    void Start()
    {    
        SpawnEnemy();
    }

    void OnEnable()
    {
        ZombieMovement.OnEnemyKilled += SpawnEnemy;
    }

    void SpawnEnemy()
    {
        if (enemyPrefab)
        {
            Instantiate(enemyPrefab, spawnLocation.transform.position, Quaternion.identity);
        }
    }
}
