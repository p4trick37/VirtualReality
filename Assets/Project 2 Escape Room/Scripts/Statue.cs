using SteamAudio;
using Unity.Collections;
using UnityEngine;

public class Statue : MonoBehaviour
{
    [SerializeField] private StatueManager manager;
    public bool IsFacing => isFacing;
    private bool isFacing;
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
}
