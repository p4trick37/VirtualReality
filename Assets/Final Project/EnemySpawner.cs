using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using System.Collections.Generic;
namespace FinalProject
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawning")]
        [SerializeField] private float spawnRate;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float minRadius;
        [SerializeField] private float maxRadius;
        [SerializeField] private Camera playerCamera;
        private float spawnTimer;

        [Header("Wave Manager")]
        [SerializeField] private WaveManager waveManager;

        [Header("List of enemies")]
        [SerializeField] private List<Enemy> enemies = new List<Enemy>();

        private bool spawnWave;
        private int enemiesSpawned;
        private bool waveAlreadyCompleted = false;
        
        private void Awake()
        {
            spawnTimer = spawnRate;
            spawnWave = true;
        }
        private void Update()
        {
            if(spawnWave == true)
            {
                int enemiesToSpawn = EnemiesToSpawn(waveManager.Wave);
                waveAlreadyCompleted = false;
                SpawnEnemies(enemiesToSpawn);
            }

            if(CheckForWaveCompleted() == true)
            {
                waveManager.SetWaveCompleted();
            }
        }

        private Vector3 RandomLocation()
        {
            Vector3 location = Vector3.zero;
            float randomRadi = Random.Range(minRadius, maxRadius);
            float randomAngle = Random.Range(0, 360);
            float x = randomRadi * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
            float z = randomRadi * Mathf.Sin(randomAngle * Mathf.Deg2Rad);
            location = new Vector3(x, playerCamera.transform.position.y, z);
            return location;
        }

        private void SpawnEnemies(int enemiesToSpawn)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                GameObject objEnemy = Instantiate(enemyPrefab, RandomLocation(), Quaternion.identity);
                enemies.Add(objEnemy.GetComponent<Enemy>());
                spawnTimer = spawnRate;
                enemiesSpawned++;
            }

            if (enemiesSpawned >= enemiesToSpawn)
            {
                spawnWave = false;
            }
        }

        private int EnemiesToSpawn(int wave)
        {
            //an = a1 + (n - 1)d
            int enemies = 5 + (wave - 1) * 2;
            return enemies;
        }

        private bool CheckForWaveCompleted()
        {
            bool objectNotNull = false;
            for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] != null )
                {
                    objectNotNull = true;
                }
            }

            if (objectNotNull == false && enemiesSpawned >= EnemiesToSpawn(waveManager.Wave))
            {
                enemiesSpawned = 0;
                waveAlreadyCompleted = true;
                return true;
            }

            return false;
        }

        public void StartWave()
        {
            spawnWave = true;
        }

        public void ClearList()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies.RemoveAt(i);
            }
        }
    }
}
