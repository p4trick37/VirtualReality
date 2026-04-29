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

        private void Start()
        {
            timer = dropRate;
        }
        private void Update()
        {
            if (autoKill == true)
            {
                AutoKill();
            }
            MoveTowardPlayer();
            DropBullet();

            CheckForHealth();
        }

        protected override void MoveTowardPlayer()
        {
            if (foundEnemy == false)
            {
                transform.LookAt(new Vector3(0, transform.position.y, 0));
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, ChangeYValue() * heightSpread + startingHeight, transform.position.z);
            }
        }

        private float ChangeYValue()
        {
            float yValue = Mathf.Sin(Time.time * upDownSpeed);
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
    }
}
