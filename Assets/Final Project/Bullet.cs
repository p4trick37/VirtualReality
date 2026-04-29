using UnityEngine;

namespace FinalProject
{
    public class Bullet : MonoBehaviour
    {
        private Flyer flyer;

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                flyer.DealDamageToPlayer(flyer.Dmg);
            }
        }

        public void FlyerEnemy(Flyer flyerEnemy)
        {
            flyer = flyerEnemy;
        }

    }
}
