using UnityEngine;

namespace FinalProject
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int dmg;

        protected void DealDamage(Enemy enemy)
        {
            enemy.SubtractHealth(dmg);
        }
    }
}
