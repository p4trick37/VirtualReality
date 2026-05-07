using TMPro;
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
        [SerializeField] private GameObject currentSword1;
        [SerializeField] private GameObject currentSword2;
        [SerializeField] private GameObject nextSword1;
        [SerializeField] private GameObject nextSword2;
        private GameObject previousSword1;
        private GameObject previousSword2;
        [SerializeField] private int currentSwordIndex;
        public static int enemiesKilled;
        [SerializeField] private bool atMaxSword;
        [Header("Box")]
        [SerializeField] private GameObject box;
        [Header("UI")]
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private TMP_Text killedBoxText;
        [SerializeField] private TMP_Text killedWaveText;
        [Header("Sound")]
        [SerializeField] private AudioSource audi;

        private bool checkToSpawnSword;

        private void Awake()
        {
            currentPrice = startingPrice;
            enemiesKilled = 0;
        }

        private void Update()
        {
            UpdateUI();

            if(checkToSpawnSword == true)
            {
                if(currentSword1.GetComponent<Sword>().BeenPickedUp == true && currentSword2.GetComponent<Sword>().BeenPickedUp == true)
                {
                    if (atMaxSword == false)
                    {
                        InstantiateNextSword();
                    }
                    else
                    {
                        Destroy(gameObject);
                        Destroy(box);
                    }
                    checkToSpawnSword = false;
                }
            }
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
            FlipSwords();
            DestoryPreviousSword();
            checkToSpawnSword = true;
            currentSword1.GetComponent<Sword>().SwordBeenBought();
            currentSword2.GetComponent<Sword>().SwordBeenBought();
            audi.Play();
        }

        public void FlipSwords()
        {
            // hayden was here
            previousSword1 = currentSword1;
            previousSword2 = currentSword2;
            currentSword1 = nextSword1;
            currentSword2 = nextSword2;        
        }

        private void InstantiateNextSword()
        {
            if (currentSwordIndex == swordPrefabs.Length - 1)
            {
                nextSword1 = null;
                nextSword2 = null;
            }
            else
            {
                nextSword1 = Instantiate(swordPrefabs[currentSwordIndex + 1], new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.126f), Quaternion.identity);
                nextSword1.GetComponent<Sword>().SetHand(true);
                nextSword2 = Instantiate(swordPrefabs[currentSwordIndex + 1], new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.126f), Quaternion.identity);
                nextSword2.GetComponent<Sword>().SetHand(false);
            }
        }
        private void DestoryPreviousSword()
        {
            if(currentSwordIndex > 0)
            {
                Destroy(previousSword1);
                Destroy(previousSword2);
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
