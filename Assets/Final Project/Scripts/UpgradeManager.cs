using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace FinalProject
{
    public class UpgradeManager : MonoBehaviour
    {
        //Price equals number of kills
        [Header("Pricing")]
        [SerializeField] private int startingPrice;
        public int CurrentPrice => currentPrice;
        [SerializeField] private int currentPrice;
        [SerializeField] private int additionalPrice;
        [Header("Swords")]
        [SerializeField] private GameObject[] swordPrefabs;
        [SerializeField] private GameObject currentSword;
        [SerializeField] private GameObject nextSword;
        private GameObject previousSword;
        [SerializeField] private int currentSwordIndex;
        public static int enemiesKilled;
        [SerializeField] private bool atMaxSword;
        [Header("Box")]
        [SerializeField] private GameObject box;
        [Header("UI")]
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private TMP_Text killedBoxText;
        [SerializeField] private TMP_Text killedWaveText;

        private void Awake()
        {
            currentPrice = startingPrice;
            enemiesKilled = 0;
        }

        private void Update()
        {
            if(atMaxSword == true)
            {
                Destroy(gameObject);
                Destroy(box);
            }

            UpdateUI();
        }

        public void BuySword()
        {
            currentSwordIndex++;
            enemiesKilled -= currentPrice;
            currentPrice += additionalPrice;
            if (currentSwordIndex == swordPrefabs.Length - 1)
            {
                atMaxSword = true;
            }
            InstantiateNextSword();
            DestoryPreviousSword();
            
        }

        public void InstantiateNextSword()
        {
            // hayden was here
            previousSword = currentSword;
            currentSword = nextSword;
            if (currentSwordIndex == swordPrefabs.Length - 1)
            {
                nextSword = null;
            }
            else
            {
                nextSword = Instantiate(swordPrefabs[currentSwordIndex + 1], transform.position, Quaternion.identity);
            }
        }
        private void DestoryPreviousSword()
        {
            if(currentSwordIndex > 0)
            {
                Destroy(previousSword);
            }
        }

        private void UpdateUI()
        {
            priceText.text = "Price: " + currentPrice + " kills";
            killedBoxText.text = "Current Kills: " + enemiesKilled;
            killedWaveText.text = "CurrentKills: " + enemiesKilled;

        }
    }
}
