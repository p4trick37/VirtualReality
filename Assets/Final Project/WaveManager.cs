using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
namespace FinalProject
{
    public class WaveManager : MonoBehaviour
    {
        public int Wave => wave;
        [SerializeField] private int wave;
        [SerializeField] private float waveDelay;
        [SerializeField] private EnemySpawner enemySpawner;

        private bool waveCompleted;

        private void Update()
        {
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
            enemySpawner.ClearList();
            yield return new WaitForSeconds(waveDelay);
            waveCompleted = false;
            enemySpawner.StartWave();
        }

        public void SetWaveCompleted()
        {
            waveCompleted = true;
        }

    }
}
