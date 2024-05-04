using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Düþman prefab'ý
    public float minSpawnDelay = 1.0f; // Minimum spawn süresi
    public float maxSpawnDelay = 5.0f; // Maksimum spawn süresi

    void Start()
    {
        StartSpawningEnemies();
    }
    void StartSpawningEnemies()
    {
        StartCoroutine(SpawnEnemiesRandomly());
    }

    IEnumerator SpawnEnemiesRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Düþmanýn spawn olacaðý pozisyon
        Vector3 spawnPosition = new Vector3(Random.Range(10f, 30f), 0, Random.Range(10f, 25f));

        // Düþmaný oluþtur
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
