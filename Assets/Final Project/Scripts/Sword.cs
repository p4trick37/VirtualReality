using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace FinalProject
{
    public class Sword : Weapon
    {
        [Header("Sword")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speedThreshold;
        [SerializeField] private float currentSpeed;
        [SerializeField] private Transform attachPoint;
        [Header("Follow Hand")]
        [SerializeField] private float followStrength;
        [SerializeField] private XRGrabInteractable interactable;
        [SerializeField] private Transform hand;
        [Header("HighLight")]
        [SerializeField] private GameObject highlight;
        [Header("Upgrade Area")]
        [SerializeField] private bool activeInGame;
        [SerializeField] private bool beenBought;
        [SerializeField] private bool hasEnoughKills;
        [SerializeField] private UpgradeManager upgradeManager;


        private bool canDealDamage;
        private bool hitEnemy;
        private bool holdSword;
        private void Awake()
        {
            if(rb == null)
            {
                rb = GetComponentInParent<Rigidbody>();
            }
            
        }

        private void Update()
        {
            currentSpeed = rb.linearVelocity.magnitude;
            if(currentSpeed > speedThreshold)
            {
                canDealDamage = true;
                highlight.SetActive(true);
            }
            else
            {
                canDealDamage = false;
                highlight.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            if (holdSword == true)
            {
                MoveSwordToHand();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(enemy != null && canDealDamage == true && hitEnemy == false)
            {
                DealDamage(enemy);
                hitEnemy = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            hitEnemy = false;
        }

        private void MoveSwordToHand()
        {
            Vector3 force = (hand.position - rb.position) * followStrength;
            rb.linearVelocity = force;
        }

        private void OnEnable()
        {
            interactable.hoverEntered.AddListener(OnHover);
            if (hasEnoughKills == true)
            {
                interactable.selectEntered.AddListener(OnSelection);
                interactable.selectExited.AddListener(OnSelectionExit);
            }
        }

        private void OnDisable()
        {
            interactable.hoverEntered.RemoveListener(OnHover);
            interactable.selectEntered.RemoveListener(OnSelection);
            interactable.selectExited.RemoveListener(OnSelectionExit);
        }

        private void OnSelection(SelectEnterEventArgs args)
        {
            if (beenBought == true)
            {
                hand = args.interactorObject.GetAttachTransform(interactable);
                holdSword = true;
            }
            else
            {
                upgradeManager.BuySword(gameObject);
                beenBought = true;
            }
        }

        private void OnSelectionExit(SelectExitEventArgs args)
        {
            holdSword = false;
        }

        private void OnHover(HoverEnterEventArgs args)
        {
            int price = upgradeManager.CurrentPrice;
            if (UpgradeManager.enemiesKilled >= price)
            {
                hasEnoughKills = true;
                interactable.trackRotation = false;
            }
            else
            {
                hasEnoughKills = false;
            }
        }

        public void SwordActivate()
        {
            activeInGame = true;
        }

        public void SwordBought()
        {
            beenBought = true;
        }
    }
}

