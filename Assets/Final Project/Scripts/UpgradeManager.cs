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
        [SerializeField] private GameObject[] swords;
        [SerializeField] private int currentSwordIndex;
        public static int enemiesKilled;
        

        private void Awake()
        {
            currentPrice = startingPrice;
            enemiesKilled = 0;
        }

        private void Update()
        {
            TurnOffAllSwords();
        }

        public void BuySword(GameObject swordGameObject)
        {
            currentSwordIndex++;
            Sword sword = swordGameObject.GetComponent<Sword>();
            enemiesKilled -= currentPrice;
            currentPrice += additionalPrice;
        }

        private void TurnOffAllSwords()
        {
            for(int i = 0; i < swords.Length - 1; i++)
            {
                if(i == currentSwordIndex)
                {
                    swords[i].SetActive(true);
                    swords[i + 1].SetActive(true);
                }
                else
                {
                    swords[i] = null;
                }
            }
        }
    }
}
