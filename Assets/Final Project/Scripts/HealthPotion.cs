using UnityEngine;

namespace FinalProject
{
    public class HealthPotion : MonoBehaviour
    {
        [SerializeField] private float maxAmount;
        [SerializeField] private float currentAmount;
        [SerializeField] private SphereCollider playerHead;
        [SerializeField] private float loseAmountRate;
        [SerializeField] private float addHealthRate;
        [SerializeField] private float raycastRadius;
        private Player player;

        private void Awake()
        {
            currentAmount = maxAmount;
            player = FindAnyObjectByType<Player>();
        }
        private void Update()
        {
            //Debug.Log(transform.up);
            if (currentAmount > 0 && CheckForAngle())
            {
                Pour();
            }
            
        }

        private void Pour()
        {
            currentAmount -= Time.deltaTime * loseAmountRate;
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, raycastRadius, transform.up);
            foreach(RaycastHit hit in hits)
            {
                if(hit.collider == playerHead)
                {
                    player.AddHealth(Time.deltaTime * addHealthRate);
                    Debug.Log("hitting player collider");
                }
            }
            Debug.Log("Is pouring");
        }

        private bool CheckForAngle()
        {
            Vector3 upVector = transform.up;
            if(upVector.y < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
