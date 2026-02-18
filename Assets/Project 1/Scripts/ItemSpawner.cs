using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] items = new GameObject[4];
    private int sequenceCount = 0;
    [SerializeField] private float spawnDelay;
    private int[] order;
    private float timer;
    [SerializeField] private Vector3 maxPosition;
    private int totalItems;
    [SerializeField] private int maxItems;

    private void Start()
    {
        timer = spawnDelay;
    }

    private void Update()
    {
        if (sequenceCount == 0)
        {
            order = randomSequence();
            //order = new int[]{1, 2, 3, 4};
            sequenceCount++;
        }



        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (totalItems < maxItems)
            {


                float xPos = Random.Range(-maxPosition.x, maxPosition.x);
                float yPos = Random.Range(-maxPosition.y, maxPosition.y);
                float zPos = Random.Range(-maxPosition.z, maxPosition.z);
                float xRot = Random.Range(0, 360);
                float yRot = Random.Range(0, 360);
                float zRot = Random.Range(0, 360);


                switch (order[sequenceCount - 1])
                {
                    case 1:
                        Instantiate(items[0], new Vector3(xPos, yPos, zPos), Quaternion.Euler(xRot, yRot, zRot));
                        break;
                    case 2:
                        Instantiate(items[1], new Vector3(xPos, yPos, zPos), Quaternion.Euler(xRot, yRot, zRot));
                        break;
                    case 3:
                        Instantiate(items[2], new Vector3(xPos, yPos, zPos), Quaternion.Euler(xRot, yRot, zRot));
                        break;
                    case 4:
                        Instantiate(items[3], new Vector3(xPos, yPos, zPos), Quaternion.Euler(xRot, yRot, zRot));
                        break;
                }
                sequenceCount++;

                if (sequenceCount > order.Length)
                {
                    sequenceCount = 0;
                }
                totalItems++;
            }
            timer = spawnDelay;
        }
    }

    private int[] randomSequence()
    {
        int[] sequence = new int[4];
        List<int> numbers = new List<int> { 1, 2, 3, 4 };
        for (int i = 0; i < sequence.Length; i++)
        {
            int randomNumber = Random.Range(0, numbers.Count);
            sequence[i] = numbers[randomNumber];
            numbers.RemoveAt(randomNumber);
        }
        return sequence;
    }
}
