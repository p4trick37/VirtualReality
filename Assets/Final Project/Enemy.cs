using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FinalProject
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Base")]
        [SerializeField] protected float moveSpeed;
        public int Dmg => dmg;
        [SerializeField] protected int dmg;

        [SerializeField] private int health;
        [SerializeField] protected float raycastDistance;
        [SerializeField] protected float raycastRadius;
        public bool Dead => dead;
        [SerializeField] protected bool dead = false;
        [SerializeField] private float despawnRate;
        [SerializeField] private float despawnTimer;
        [Header("Tool")]
        [SerializeField] protected bool autoKill;
        [SerializeField] private float autoKillRate;
        private float autoKillTimer;
        [SerializeField] protected bool autoTakeDamage;
        [SerializeField] private float damageRate;
        [SerializeField] private int amountOfDamage;
        private float autoDamageTimer;
        [Header("Player Reference")]
        [SerializeField] protected Camera playerCamera;
        [SerializeField] protected Player player;
        [Header("Hightlight")]
        [SerializeField] private GameObject flashHighlight;
        [SerializeField] private float maxAlpha;
        [SerializeField] private float timeInterval;

        protected bool foundEnemy;

        private void Awake()
        {
            autoKillTimer = autoKillRate;
            autoDamageTimer = damageRate;
            playerCamera = Camera.main;
            player = FindAnyObjectByType<Player>();
            despawnTimer = despawnRate;
        }

        public void TakeDamage(int dmg)
        {
            health -= dmg;
            StopAllCoroutines();
            StartCoroutine(Flash());
            Debug.Log("Took Damage");
        }


        protected virtual void MoveTowardPlayer()
        {
            if (foundEnemy == false)
            {
                transform.LookAt(new Vector3(0, transform.position.y, 0));
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }

        protected virtual void FindEnemy()
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

        protected void AutoTakeDamage()
        {
            autoDamageTimer -= Time.deltaTime;
            if(autoDamageTimer <= 0 )
            {
                TakeDamage(amountOfDamage);
                autoDamageTimer = damageRate;
            }
        }

        protected void CheckForHealth()
        {
            if(health <= 0)
            {
                dead = true;
            }
        }

        public void DealDamageToPlayer(int dmg)
        {
            player.SubtractHealth(dmg);
        }

        protected IEnumerator Flash()
        {
            flashHighlight.SetActive(true);
            float x = maxAlpha;
            Color currentColor = flashHighlight.GetComponent<MeshRenderer>().material.color;
            flashHighlight.GetComponent<MeshRenderer>().material.color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.InverseLerp(0, 255, maxAlpha));
            
            
            while(x > 0)
            {
                Color nextColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.InverseLerp(0, 255, x));
                flashHighlight.GetComponent<MeshRenderer>().material.color = nextColor;
                x--;
                yield return new WaitForSeconds(timeInterval);
            }
            flashHighlight.SetActive(false);
        }
        protected virtual void OnDeath()
        {
            despawnTimer -= Time.deltaTime;
            if(despawnTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
