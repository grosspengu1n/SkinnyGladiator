using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;           
        public float spawnDelay;          
        public Vector2 spawnAreaMin;       
        public Vector2 spawnAreaMax;       
        public GameObject[] enemyPrefabs;  
    }

    public Wave[] waves;                  
    private int currentWave = 0;
    public float timeBetweenWaves = 60f;   

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {

            yield return new WaitForSeconds(timeBetweenWaves);
            if (waves.Length > 0)
            {
                if (currentWave < waves.Length)
                {

                    StartCoroutine(SpawnWave(waves[currentWave]));
                    currentWave++;
                }
                else
                {

                    currentWave = 0;
                }
            }
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemyCount; i++)
        {

            Vector2 spawnPos = new Vector2(
                Random.Range(wave.spawnAreaMin.x, wave.spawnAreaMax.x),
                Random.Range(wave.spawnAreaMin.y, wave.spawnAreaMax.y)
            );

            if (wave.enemyPrefabs != null && wave.enemyPrefabs.Length > 0)
            {
                GameObject enemyPrefab = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
            yield return new WaitForSeconds(wave.spawnDelay);
        }
    }
}
