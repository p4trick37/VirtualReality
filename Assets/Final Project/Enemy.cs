using UnityEngine;

namespace FinalProject
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        public int Dmg => dmg;
        [SerializeField] private int dmg;

        [SerializeField] private int health;
        


        private void OnCollisionEnter(Collision collision)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player != null)
            {
                player.SubtractHealth(dmg);
                Destroy(gameObject);
            }

        }

        public void SubtractHealth(int dmg)
        {
            health -= dmg;
        }

        private void Update()
        {
            transform.LookAt(Vector3.zero);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
