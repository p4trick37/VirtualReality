using Unity.VisualScripting;
using UnityEngine;

namespace FinalProject
{
    public class Sword : Weapon
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speedThreshold;
        [SerializeField] private float currentSpeed;
        private bool canDealDamage;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            currentSpeed = rb.linearVelocity.magnitude;
            if(currentSpeed > speedThreshold)
            {
                canDealDamage = true;
            }
            else
            {
                canDealDamage = false;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(enemy != null && canDealDamage == true)
            {
                Debug.Log("Was able to deal damage");
                DealDamage(enemy);
            }
        }
    }
}
