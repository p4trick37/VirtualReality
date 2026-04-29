using UnityEngine;

namespace FinalProject
{
    public class Walker : Enemy
    {
        [SerializeField] private float attackDistance;
        [SerializeField] private float attackSpeed;
        private float attackTimer;
        private void Update()
        {
            if (autoKill == true)
            {
                AutoKill();
            }

            FindEnemy();
            

            if(PositionToAttack())
            {
                AttackPhase();
            }
            else
            {
                MoveTowardPlayer();
            }

            CheckForHealth();
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

        private void AttackPhase()
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                Attack();
                attackTimer = attackSpeed;
            }

        }

        private void Attack()
        {
            player.SubtractHealth(dmg);
        }

    }
}
