using TMPro;
using UnityEngine;

namespace FinalProject
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private TMP_Text healthText;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(health <= 0)
            {
                health = 0;
            }

            healthText.text = health.ToString();
        }

        public void SubtractHealth(int dmg)
        {
            health -= dmg;
        }
    }
}
