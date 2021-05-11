using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public int enemyCounter;
    public int waveCounter = 1;
    private float spawnRange = 9.0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyWave(waveCounter);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        enemyCounter = FindObjectsOfType<Enemy>().Length;
        if (enemyCounter == 0)
        {
            waveCounter++;
            spawnEnemyWave(waveCounter);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    void spawnEnemyWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(spawnPosX, 0, spawnPosZ);

        return spawnPosition;
    }
}
