using UnityEngine;

namespace FinalProject
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int health;
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
        }

        public void SubtractHealth(int dmg)
        {
            health -= dmg;
        }
    }
}
