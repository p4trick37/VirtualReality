using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
namespace FinalProject
{
    public class WaveManager : MonoBehaviour
    {
        public int Wave => wave;
        [SerializeField] private int wave;
        [SerializeField] private float waveDelay;
        [SerializeField] private EnemySpawner enemySpawner;
        public static int staticWave = 1;

        private bool waveCompleted;

        private void Update()
        {
            staticWave = wave;
            if(waveCompleted == true)
            {
                waveCompleted = false;
                StartCoroutine(WaveDelay());
            }
        }

        private IEnumerator WaveDelay()
        {
            wave++;
            enemySpawner.ClearList();
            yield return new WaitForSeconds(waveDelay);
            waveCompleted = false;
            enemySpawner.StartWave();
        }

        public void SetWaveCompleted()
        {
            waveCompleted = true;
        }

        public int NumOfFlyersSpawn()
        {
            float floatNum = 1 + (wave - 1) * 0.5f;
            int numToSpawn = Mathf.FloorToInt(floatNum);
            if(wave < 3 )
            {
                numToSpawn = 0;
            }
            return numToSpawn;
        }
    }
}
