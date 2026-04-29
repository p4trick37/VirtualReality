using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
namespace FinalProject
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Enemy Prefabs")]
        [SerializeField] private GameObject walkerPrefab;
        [SerializeField] private GameObject crawlerPrefab;
        [SerializeField] private GameObject flyerPrefab;
        [Header("Spawning")]
        [SerializeField] private float spawnRate;
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
            enemiesSpawned = 0;
        }

        private void Update()
        {
            if(spawnWave == true)
            {
                int enemiesToSpawn = EnemiesToSpawn(waveManager.Wave);
                waveAlreadyCompleted = false;
                SpawnEnemies(SpawnOrder(enemiesToSpawn, waveManager.NumOfFlyersSpawn()));
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
            float randomAngle = Random.Range(0, 180);
            float x = randomRadi * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
            float z = randomRadi * Mathf.Sin(randomAngle * Mathf.Deg2Rad);
            location = new Vector3(x, playerCamera.transform.position.y, z);
            return location;
        }

        private void SpawnEnemies(string[] enemyOrder)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                GameObject objEnemy;
                if (enemyOrder[enemiesSpawned].Equals("Flyer"))
                {
                    objEnemy = Instantiate(flyerPrefab, RandomLocation(), Quaternion.identity);
                }
                else
                {
                    objEnemy = Instantiate(walkerPrefab, RandomLocation(), Quaternion.identity);
                }

                enemies.Add(objEnemy.GetComponent<Enemy>());
                spawnTimer = spawnRate;
                enemiesSpawned++;
            }

            if (enemiesSpawned >= enemyOrder.Length)
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

        public string[] SpawnOrder(int enemiesToSpawn)
        {
            string[] enemies = new string[enemiesToSpawn];
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = "Walker";
            }
            return enemies;
        }

        public string[] SpawnOrder(int enemiesToSpawn, int numToSpawn)
        {
            string[] enemies = new string[enemiesToSpawn];
            int[] randomPositions = new int[numToSpawn];
            List<int> positions = new List<int>();
            for(int i = 0; i < enemiesToSpawn; i++)
            {
                positions.Add(i);
            }

            for(int i = 0; i < randomPositions.Length; i++)
            {
                int randomIndex = Random.Range(0, positions.Count);
                randomPositions[i] = positions[randomIndex];
                positions.RemoveAt(randomIndex);
            }

            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = "Walker";
            }

            for(int i = 0; i < enemies.Length; i++)
            {
                for(int j = 0; j < randomPositions.Length; j++)
                {
                    if (i == randomPositions[j])
                    {
                        enemies[i] = "Flyer";
                    }
                }
            }

            return enemies;
        }
    }
}
