using UnityEngine;

namespace FinalProject
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speedThreshHold;
        [SerializeField] private int dmg;
        private float weaponSpeed;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();    
        }

        private void Update()
        {
            weaponSpeed = rb.linearVelocity.magnitude;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if(enemy != null)
            {
                if(weaponSpeed > speedThreshHold)
                {
                    enemy.SubtractHealth(dmg);
                }
            }
        }
    }
}
