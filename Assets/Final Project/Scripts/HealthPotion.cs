using System.Runtime.CompilerServices;
using UnityEngine;

namespace FinalProject
{
    public class HealthPotion : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private float maxAmount;
        [SerializeField] private float currentAmount;
        [SerializeField] private SphereCollider playerHead;
        [SerializeField] private float loseAmountRate;
        [SerializeField] private float addHealthRate;
        [SerializeField] private float raycastRadius;
        private Player player;

        [Header("Characteristics")]
        [SerializeField] private GameObject belt;
        [SerializeField] private bool inBelt;
        [Header("Liquid")]
        [SerializeField] private GameObject liquid;
        

        private void Awake()
        {
            currentAmount = maxAmount;
            player = FindAnyObjectByType<Player>();
        }
        private void Update()
        {
            //Debug.Log(transform.up);
            if (currentAmount > 0 && CheckForAngle() && inBelt == false)
            {
                Pour();
            }
            SetLiquid();
        }

        private void Pour()
        {
            currentAmount -= Time.deltaTime * loseAmountRate;
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, raycastRadius, transform.up);
            foreach(RaycastHit hit in hits)
            {
                if(hit.collider == playerHead && player.Health < player.MaxHealth)
                {
                    player.AddHealth(Time.deltaTime * addHealthRate);
                }
            }
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

        public void InBeltState()
        {
            inBelt = true;
        }

        public void OutBeltState()
        {
            inBelt = false;
        }

        private void SetLiquid()
        {
            float amountValue = Mathf.InverseLerp(0, maxAmount, currentAmount);
            // At full, position = 0, 0, 0
            // At full, scaling = 0.075, 0.15, 0.075
            // both x and z for both position and scaling stays the same
            //Just y values change
            float yPosition = Mathf.Lerp(-0.075f, 0, amountValue);
            float yScaling = Mathf.Lerp(0, 0.15f, amountValue);

            liquid.transform.localPosition = new Vector3(liquid.transform.localPosition.x, yPosition, liquid.transform.localPosition.z);
            liquid.transform.localScale = new Vector3(liquid.transform.localScale.x, yScaling, liquid.transform.localScale.z);
        }
    }
}
