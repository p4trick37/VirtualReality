using UnityEngine;

namespace FinalProject
{
    public class ZombieAnimation : MonoBehaviour
    {
        private Walker walker;

        private void Awake()
        {
            walker = GetComponentInParent<Walker>();
        }

        public void AttackPlayer()
        {
            if (walker != null)
            {
                walker.Attack();
            }
        }
    }
}
