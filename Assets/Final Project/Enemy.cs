using UnityEngine;

namespace FinalProject
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        public int Dmg => dmg;
        [SerializeField] protected int dmg;

        [SerializeField] private int health;
        [SerializeField] private float raycastDistance;
        [SerializeField] private float raycastRadius;
        protected Camera playerCamera;
        protected Player player;
        protected bool foundEnemy;

        private void Awake()
        {
            playerCamera = Camera.main;
            player = FindAnyObjectByType<Player>();
        }

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
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }

        protected void MoveTowardPlayer()
        {
            if (foundEnemy == false)
            {
                transform.LookAt(new Vector3(0, playerCamera.gameObject.transform.position.y, 0));
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }

        protected void FindEnemy()
        {
            if(Physics.SphereCast(transform.position, raycastRadius, transform.forward, out RaycastHit hit, raycastDistance))
            {
                Enemy enemyFound = hit.transform.GetComponent<Enemy>();
                if(enemyFound != null && enemyFound != gameObject)
                {
                    Debug.Log(enemyFound);
                    foundEnemy = true;
                } 
            }
            else
            {
                foundEnemy = false;
            }
        }

        private void OnDrawGizmos()
        {
            if (gameObject != null)
            {
                Gizmos.DrawLine(transform.position, transform.position + transform.forward * raycastDistance);
                Gizmos.DrawSphere(transform.position + transform.forward * raycastDistance, raycastRadius);
            }
        }
    }
}
