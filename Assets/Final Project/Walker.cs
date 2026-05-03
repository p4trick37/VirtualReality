using UnityEngine;

namespace FinalProject
{
    public class Walker : Enemy
    {
        [Header("Walker")]
        [SerializeField] private float attackDistance;
        [SerializeField] private float attackSpeed;
        private float attackTimer;
        [Header("Animations")]
        [SerializeField] private Animator ani;
        [SerializeField] private string deathParameter;
        [SerializeField] private string attackParameter;
        [SerializeField] private string idleParameter;
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

            FindEnemy();
            

            if(PositionToAttack())
            {
                GoToAttack();
            }
            else
            {
                MoveTowardPlayer();
            }

            CheckForHealth();

            if(dead == true)
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
            base.OnDeath();
        }
    }
}
