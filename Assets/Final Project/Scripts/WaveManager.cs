using System.Collections;
using TMPro;
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
        [SerializeField] private TMP_Text waveText;
        [SerializeField] private int startingFlyerWave;
        [Header("Enemy Scaling")]
        [SerializeField] private int walkerHealthScale;
        [SerializeField] private int flyerHealthScale;
        [SerializeField] private int walkerDamageScale;
        [SerializeField] private int flyerDamageScale;
        


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

            waveText.text = "Wave: " + wave;
        }

        private IEnumerator WaveDelay()
        {
            wave++;
            enemySpawner.ClearList();
            yield return new WaitForSeconds(waveDelay);
            waveCompleted = false;
            Walker.ScaleMaxHealth(walkerHealthScale);
            Walker.ScaleDamage(walkerDamageScale);
            if(wave >= startingFlyerWave)
            {
                Flyer.ScaleMaxHealth(flyerHealthScale);
                Flyer.ScaleDamage(flyerDamageScale);
            }
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
            if(wave < startingFlyerWave)
            {
                numToSpawn = 0;
            }
            return numToSpawn;
        }
    }
}
