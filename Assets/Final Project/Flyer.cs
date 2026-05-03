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

        private void Start()
        {
            timer = dropRate;
            upDownOffset = Mathf.Sin(Time.time * upDownSpeed);
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
            transform.position = new Vector3(transform.position.x, ChangeYValue() * heightSpread + startingHeight, transform.position.z);
        }

        private float ChangeYValue()
        {
            float yValue = Mathf.Sin((Time.time * upDownSpeed) - upDownOffset);
            return yValue;
        }


        private void DropBullet()
        {
            if(ChangeYValue() < -0.75f && positionToDrop == true)
            {
                dropBullet = true;
                positionToDrop = false;
            }

            if(ChangeYValue() > 0.5f)
            {
                positionToDrop = true;
            }

            if(dropBullet == true)
            {
                GameObject bulletSpawned = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
                bulletSpawned.GetComponent<Bullet>().FlyerEnemy(gameObject.GetComponent<Flyer>());
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

        
        //protected override void FindEnemy()
        //{
        //    if (Physics.SphereCast(transform.position, raycastRadius, transform.forward, out RaycastHit hit, raycastDistance))
        //    {
        //        Enemy enemyFound = hit.transform.GetComponent<Enemy>();
        //        if (enemyFound != null && enemyFound != gameObject)
        //        {
        //            foundEnemy = true;
        //        }
        //    }
        //    else
        //    {
        //        foundEnemy = false;
        //    }
        //}
    }
}
