using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FinalProject
{
    public class Player : MonoBehaviour
    {
        public float Health => health;
        public float MaxHealth => maxHealth;
        [SerializeField] private float maxHealth;
        [SerializeField] private float health;
        //[SerializeField] private TMP_Text healthText;

        private void Awake()
        {
            health = maxHealth;
        }

        void Update()
        {
            if(health <= 0)
            {
                health = 0;
                SceneManager.LoadScene(2);
            }
            
            if(health > maxHealth)
            {
                health = maxHealth;
            }

            //healthText.text = health.ToString();
        }

        public void SubtractHealth(int dmg)
        {
            health -= dmg;
        }

        public void AddHealth(float amount)
        {
            health += amount;
        }
    }
}
