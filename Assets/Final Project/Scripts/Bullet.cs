using UnityEngine;

namespace FinalProject
{
    public class Bullet : MonoBehaviour
    {
        private Flyer flyer;

        private void OnTriggerEnter(Collider other)
        {
            
            
            Flyer flyerTemp = other.GetComponent<Flyer>();
            if(flyerTemp != null)
            {
                return;
            }

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                flyer.DealDamageToPlayer(flyer.Dmg);
            }

            Destroy(gameObject);
        }

        public void FlyerEnemy(Flyer flyerEnemy)
        {
            flyer = flyerEnemy;
        }

    }
}
