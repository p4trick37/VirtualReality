using UnityEngine;

namespace FinalProject
{
    public class Flyer : Enemy
    {
        [Header("Movement")]
        [SerializeField] private float heightSpread;
        [SerializeField] private float startingHeight;
        [SerializeField] private float upDownSpeed;
        private float counter = 0;
        [Header("Attack")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float dropRate;
        private float timer;
        private bool dropBullet;
        private bool positionToDrop;
        private float upDownOffset;
        private bool beenKilled = false;
        [Header("Physics")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float forceMagnitude;
        [Header("Audio")]
        [SerializeField] private AudioSource audi;
        [SerializeField] private AudioClip bulletDropClip;
        [SerializeField] private AudioClip deathClip;
        [SerializeField] private float bulletDropLength;
        private float bulletClipTimer;
        private bool startBulletClip;

        private EnemySpawner spawner;

        public static int maxHealth;
        public static int staticDamage;

        private void Start()
        {
            timer = dropRate;
            upDownOffset = Mathf.Sin(Time.time * upDownSpeed);
            health += maxHealth;
            dmg += staticDamage;
            spawner = FindAnyObjectByType<EnemySpawner>();
            bulletClipTimer = bulletDropLength;
        }
        private void Update()
        {
            if (autoKill == true)
            {
                AutoKill();
            }
            if(autoTakeDamage == true)
            {
                AutoTakeDamage();
            }

            MoveTowardPlayer();
            MoveUpDown();
            DropBullet();
            FindEnemy();

            CheckForHealth();

            if(dead == true)
            {
                OnDeath();
                if(beenKilled == false)
                {
                    rb = GetComponent<Rigidbody>();
                    if(rb != null)
                    {
                        rb.useGravity = true;
                        rb.AddForce(-transform.forward * forceMagnitude);
                    }
                    beenKilled = true;
                }

            }

            if(startBulletClip == true)
            {
                CutBulletDropClip();
            }
        }


        protected override void MoveTowardPlayer()
        {
            if (foundEnemy == false && InPosition() == false)
            {
                transform.LookAt(new Vector3(0, transform.position.y, 0));
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
        private void MoveUpDown()
        {
            if (dead == false)
            {
                transform.position = new Vector3(transform.position.x, ChangeYValue() * heightSpread + startingHeight, transform.position.z);
            }
        }

        private float ChangeYValue()
        {
            float yValue = Mathf.Sin((Time.time * upDownSpeed) - upDownOffset) + spawner.FlyerSpawnHeight;
            return yValue;
        }


        private void DropBullet()
        {
            if(ChangeYValue() < -0.75f + spawner.FlyerSpawnHeight && positionToDrop == true)
            {
                dropBullet = true;
                positionToDrop = false;
            }

            if (ChangeYValue() > 0.5f + spawner.FlyerSpawnHeight)
            {
                positionToDrop = true;
            }

            if(dropBullet == true)
            {
                GameObject bulletSpawned = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
                bulletSpawned.GetComponent<Bullet>().FlyerEnemy(gameObject.GetComponent<Flyer>());
                audi.clip = bulletDropClip;
                audi.Play();
                startBulletClip = true;
                bulletClipTimer = bulletDropLength;
                dropBullet = false;
            }
        }

        private bool InPosition()
        {
            float distance = Vector2.Distance(Vector2.zero, new Vector2(transform.position.x, transform.position.z));
            if(distance < 0.1f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void FindEnemy()
        {
            RaycastHit[] hitsDown = Physics.SphereCastAll(transform.position + transform.forward * raycastDistance, raycastRadius, Vector3.down);
            bool foundFlyer = false;
            foreach(RaycastHit hit in hitsDown)
            {
                if(hit.transform.gameObject.GetComponent<Flyer>() && hit.transform.gameObject != gameObject)
                {
                    foundFlyer = true;
                }
            }

            RaycastHit[] hitsUp = Physics.SphereCastAll(transform.position + transform.forward * raycastDistance, raycastRadius, Vector3.up);
            foreach (RaycastHit hit in hitsUp)
            {
                if(hit.transform.gameObject.GetComponent<Flyer>() && hit.transform.gameObject != gameObject)
                {
                    foundFlyer = true;
                }
            }


            if(foundFlyer == true)
            {
                foundEnemy = true;
            }
            else
            {
                foundEnemy = false;
            }
        }

        protected override void OnDeath()
        {
            if(alreadyDead == false)
            {
                audi.clip = deathClip;
                audi.Play();
            }
            base.OnDeath();
        }

        public static void ScaleMaxHealth(int amount)
        {
            maxHealth += amount;
        }

        public static void ScaleDamage(int amount)
        {
            staticDamage += amount;
        }

        private void CutBulletDropClip()
        {
            bulletClipTimer -= Time.deltaTime;
            if(bulletClipTimer <= 0 )
            {
                audi.clip = null;
                startBulletClip = false;
            }
        }
    }
}
