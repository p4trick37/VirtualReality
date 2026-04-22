using UnityEngine;

namespace FinalProject
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        public int Dmg => dmg;
        [SerializeField] private int dmg;
        

        private void OnCollisionEnter(Collision collision)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player != null)
            {
                player.SubtractHealth(dmg);
                Destroy(gameObject);
            }

            if(collision.gameObject.CompareTag("Sword"))
            {
                Destroy(gameObject);
            }
        }



        private void Update()
        {
            transform.LookAt(Vector3.zero);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
