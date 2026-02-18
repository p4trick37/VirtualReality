using UnityEngine;

public class Sphere : MonoBehaviour
{
    private void Start()
    {
        Collider myCollider = GetComponent<Collider>();
        Player player = FindAnyObjectByType<Player>();
        Collider playerCollider = player.gameObject.GetComponent<Collider>();
        Physics.IgnoreCollision(myCollider, playerCollider);
    }
}
