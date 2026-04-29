using UnityEngine;

namespace FinalProject
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        public int Dmg => dmg;
        [SerializeField] protected int dmg;

        [SerializeField] private int health;
        [SerializeField] private float raycastDistance;
        [SerializeField] private float raycastRadius;
        [Header("Tool")]
        [SerializeField] protected bool autoKill;
        [SerializeField] protected float autoKillRate;
        protected float autoKillTimer;
        [SerializeField] protected Camera playerCamera;
        [SerializeField] protected Player player;
        protected bool foundEnemy;

        private void Awake()
        {
            autoKillTimer = autoKillRate;
            playerCamera = Camera.main;
            player = FindAnyObjectByType<Player>();
        }

        public void SubtractHealth(int dmg)
        {
            health -= dmg;
        }


        protected virtual void MoveTowardPlayer()
        {
            if (foundEnemy == false)
            {
                transform.LookAt(new Vector3(0, transform.position.y, 0));
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

        protected void AutoKill()
        {
            autoKillTimer -= Time.deltaTime;
            if(autoKillTimer <= 0)
            {
                Destroy(gameObject);
                autoKillTimer = autoKillRate;
            }
        }

        protected void CheckForHealth()
        {
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void DealDamageToPlayer(int dmg)
        {
            player.SubtractHealth(dmg);
        }
    }
}
