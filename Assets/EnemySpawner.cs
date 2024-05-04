using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // D��man prefab'�
    public float minSpawnDelay = 1.0f; // Minimum spawn s�resi
    public float maxSpawnDelay = 5.0f; // Maksimum spawn s�resi

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
        // D��man�n spawn olaca�� pozisyon
        Vector3 spawnPosition = new Vector3(Random.Range(10f, 30f), 0, Random.Range(10f, 25f));

        // D��man� olu�tur
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
