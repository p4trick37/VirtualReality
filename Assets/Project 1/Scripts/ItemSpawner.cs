using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] items = new GameObject[4];
    private bool[] usedItems = new bool[4];
    [SerializeField] private float spawnDelay;

    private float timer;

    private void Start()
    {
        timer = spawnDelay;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            
        }
    }

}
