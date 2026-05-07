using UnityEngine;

namespace FinalProject
{
    public class Walker : Enemy
    {
        [Header("Walker")]
        [SerializeField] private float attackDistance;
        [SerializeField] private float attackSpeed;
        public static int maxHealth;
        public static int staticDamage;
        private float attackTimer;
        [Header("Animations")]
        [SerializeField] private Animator ani;
        [SerializeField] private string deathParameter;
        [SerializeField] private string attackParameter;
        [SerializeField] private string idleParameter;
        [Header("Collider")]
        [SerializeField] private BoxCollider enemyCollider;
        [Header("Audio")]
        [SerializeField] private AudioSource audi;
        [SerializeField] private AudioClip attackClip;
        [SerializeField] private AudioClip deathClip;

        private void Start()
        {
            health += maxHealth;
            dmg += staticDamage;
        }

        private void Update()
        {
            if (autoKill == true)
            {
                AutoKill();
            }

            if(autoTakeDamage == true)
            {
                AutoTakeDamage();
            }

            if(dead == false)
            {
                FindEnemy();


                if (PositionToAttack())
                {
                    GoToAttack();
                }
                else
                {
                    MoveTowardPlayer();
                }

                CheckForHealth();
            }
            else
            {
                OnDeath();
            }
        }

        protected override void MoveTowardPlayer()
        {
            if (foundEnemy == false)
            {
                GoToWalk();
                transform.LookAt(new Vector3(0, transform.position.y, 0));
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            else
            {
                GoToIdle();
            }
        }

        private bool PositionToAttack()
        {
            float distance = Vector3.Distance(transform.position, playerCamera.transform.position);
            if(distance <= attackDistance)
            { 
                return true;
            }
            else
            {
                return false;
            }
        }

        //private void AttackPhase()
        //{
        //    attackTimer -= Time.deltaTime;
        //    if(attackTimer <= 0)
        //    {
        //        Attack();
        //        attackTimer = attackSpeed;
        //    }

        //}

        public void Attack()
        {
            DealDamageToPlayer(dmg);
            audi.clip = attackClip;
            audi.Play();
        }

        private void GoToDeathAni()
        {
            ani.SetBool(deathParameter, true);
        }

        private void GoToWalk()
        {
            ani.SetBool(idleParameter, false);
        }
        private void GoToIdle()
        {
            ani.SetBool(idleParameter, true);
        }

        private void GoToAttack()
        {
            ani.SetBool(attackParameter, true);
        }

        protected override void OnDeath()
        {
            GoToDeathAni();
            if(enemyCollider == null)
            {
                enemyCollider = GetComponent<BoxCollider>();
            }
            enemyCollider.enabled = false;
            if (alreadyDead == false)
            {
                audi.clip = deathClip;
            }
            base.OnDeath();
        }

        public static void ScaleMaxHealth(int amount)
        {
            maxHealth += amount;
        }

        public static void ScaleDamage(int amount)
        {
            staticDamage += amount;
        }
    }
}
