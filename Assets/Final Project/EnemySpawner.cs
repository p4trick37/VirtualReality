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
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float spawnHeight;
        private float spawnTimer;

        [Header("Wave Manager")]
        [SerializeField] private WaveManager waveManager;

        [Header("List of enemies")]
        [SerializeField] private List<Enemy> enemies = new List<Enemy>();

        private bool spawnWave;
        private int enemiesSpawned;
        private bool waveAlreadyCompleted = false;
        private bool orderCreated = false;

        private string[] order;
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
                if (orderCreated == false)
                {
                    order = SpawnOrder(enemiesToSpawn, waveManager.NumOfFlyersSpawn());
                    orderCreated = true;
                }
                waveAlreadyCompleted = false;
                SpawnEnemies(order);
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
            float randomAngle = Random.Range(minAngle, maxAngle);
            float x = randomRadi * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
            float z = randomRadi * Mathf.Sin(randomAngle * Mathf.Deg2Rad);
            location = new Vector3(x, spawnHeight, z);
            return location;
        }

        private void SpawnEnemies(string[] enemyOrder)
        {
            spawnTimer -= Time.deltaTime;
            Debug.Log(PrintOrder(enemyOrder));

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
                if(enemies[i] != null && enemies[i].Dead == false)
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
            orderCreated = false;
            spawnWave = true;
        }

        public void ClearList()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                Destroy(enemies[i]);
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

        private string PrintOrder(string[] order)
        {
            string orderStr = "";
            for(int i = 0; i < order.Length; i++)
            {
                orderStr += order[i] + ", ";
            }
            return orderStr;
        }

        private void OnDrawGizmos()
        {
            Vector3 direction1 = Vector3.zero;
            direction1.x = Mathf.Cos(minAngle * Mathf.Deg2Rad);
            direction1.y = 0;
            direction1.z = Mathf.Sin(minAngle * Mathf.Deg2Rad);
            Gizmos.DrawLine(Vector3.zero, direction1 * 20);
            Vector3 direction2 = Vector3.zero;
            direction2.x = Mathf.Cos(maxAngle * Mathf.Deg2Rad);
            direction2.y = 0;
            direction2.z = Mathf.Sin(maxAngle * Mathf.Deg2Rad);
            Gizmos.DrawLine(Vector3.zero, direction2 * 20);
        }
    }
}
