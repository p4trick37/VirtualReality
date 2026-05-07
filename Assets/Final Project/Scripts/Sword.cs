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
        public bool BeenPickedUp => beenPickedUp;
        [SerializeField] private bool beenPickedUp;
        [SerializeField] private bool canBeBought;
        [SerializeField] private bool beenBought;
        [SerializeField] private bool hasEnoughKills;
        [SerializeField] private UpgradeManager upgradeManager;
        [SerializeField] private bool startingSword;
        [Header("RespawnPoint")]
        [SerializeField] private Transform respawnPoint1;
        [SerializeField] private Transform respawnPoint2;
        [SerializeField] private float distanceToRespawn;
        [SerializeField] private bool rightHand;
        private Player player;
        [SerializeField] private AudioSource audi;

        private bool canDealDamage;
        private bool hitEnemy;
        private bool holdSword;
        private void Awake()
        {
            if (rb == null)
            {
                rb = GetComponentInParent<Rigidbody>();
            }
            upgradeManager = FindAnyObjectByType<UpgradeManager>();
            rb.linearVelocity = Vector3.zero;
            player = FindAnyObjectByType<Player>();
            respawnPoint1 = GameObject.Find("RespawnPoint1").transform;
            respawnPoint2 = GameObject.Find("RespawnPoint2").transform;
            if (startingSword == true)
            {
                RespawnSword();
            }
        }

        private void Update()
        {
            CheckingSpeed();

            if (CheckForEnoughKills())
            {
                canBeBought = true;
            }
            else
            {
                canBeBought = false;
            }

            if (canBeBought == true || beenBought == true || startingSword == true)
            {
                interactable.trackRotation = true;
            }
            else
            {
                interactable.trackRotation = false;
            }

            if (ShouldRespawn() == true)
            {
                RespawnSword();
            }
        }

        private void CheckingSpeed()
        {
            currentSpeed = rb.linearVelocity.magnitude;
            if (currentSpeed > speedThreshold)
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
            if (enemy != null && canDealDamage == true && hitEnemy == false)
            {
                DealDamage(enemy);
                hitEnemy = true;
                audi.Play();
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
            interactable.selectEntered.AddListener(OnSelection);
            interactable.selectExited.AddListener(OnSelectionExit);
        }

        private void OnDisable()
        {
            interactable.selectEntered.RemoveListener(OnSelection);
            interactable.selectExited.RemoveListener(OnSelectionExit);
        }

        private void OnSelection(SelectEnterEventArgs args)
        {
            if (canBeBought == true || beenBought == true)
            {
                ChangeAllLayerMasks();
                hand = args.interactorObject.GetAttachTransform(interactable);
                holdSword = true;
                UnFreezeSword();
                beenPickedUp = true;
                if (beenBought == false && startingSword == false)
                {
                    upgradeManager.BuySword();
                }
            }
        }



        private void OnSelectionExit(SelectExitEventArgs args)
        {
            holdSword = false;
        }



        private bool CheckForEnoughKills()
        {
            int price = upgradeManager.CurrentPrice;
            if (UpgradeManager.enemiesKilled >= price || startingSword == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ShouldRespawn()
        {
            float distance = 0;
            if (rightHand == true)
            {
                distance = Vector3.Distance(transform.position, respawnPoint1.position);
            }
            else
            {
                distance = Vector3.Distance(transform.position, respawnPoint2.position);
            }

            if (distance > distanceToRespawn && holdSword == false && ((beenBought == true && beenPickedUp == true) || startingSword == true))
            {
                return true;
            }

            return false;
        }
        private void RespawnSword()
        {
            if (rightHand == true)
            {
                transform.position = respawnPoint1.position;
            }
            else
            {
                transform.position = respawnPoint2.position;
            }
            FreezeSword();
        }

        private void FreezeSword()
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("Spin me right round");
        }

        private void UnFreezeSword()
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        private void ChangeAllLayerMasks()
        {
            gameObject.layer = 12;
            Transform[] objectsInChildren = gameObject.GetComponentsInChildren<Transform>(); 
            for (int i = 0; i < objectsInChildren.Length; i++)
            {
                objectsInChildren[i].gameObject.layer = 12;
            }
        }

        public void SetHand(bool shouldBeRight)
        {
            if (shouldBeRight == true)
            {
                rightHand = true;
            }
            else
            {
                rightHand = false;
            }
        }

        public void SwordBeenBought()
        {
            beenBought = true;
        }
    
    }
}

