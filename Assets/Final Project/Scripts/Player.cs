using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FinalProject
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private TMP_Text healthText;
      
        void Update()
        {
            if(health <= 0)
            {
                health = 0;
                SceneManager.LoadScene(2);
            }

            healthText.text = health.ToString();
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
