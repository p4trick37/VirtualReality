using UnityEngine;

public class StopMotion : MonoBehaviour
{
    private Player player;
    

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Freeze"))
        {
            player.movementSpeed = 0;
            Debug.Log("This has ran");
        }
    }
}
