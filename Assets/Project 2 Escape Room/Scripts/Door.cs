using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void OpenDoor()
    {
        Destroy(gameObject);
    }

    public void ChangeColorGreen()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    public void ChangeColorGray()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.gray;
    }
}
