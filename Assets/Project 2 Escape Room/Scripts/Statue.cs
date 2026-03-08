using SteamAudio;
using Unity.Collections;
using UnityEngine;

public class Statue : MonoBehaviour
{
    [SerializeField] private StatueManager manager;
    [SerializeField] private Rigidbody rb;
    public bool IsFacing => isFacing;
    private bool isFacing;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Statue otherStatue = hit.transform.gameObject.GetComponent<Statue>();
            if(otherStatue != null)
            {
                isFacing = true;
            }
            else
            {
                isFacing = false;
            }
        }
    }

    public void FreezeRotation()
    {
        rb.freezeRotation = true;
    }

    public void UnFreezeRotation()
    {
        rb.freezeRotation = false;
    }
}
