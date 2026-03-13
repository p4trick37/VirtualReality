using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class StatueManager : MonoBehaviour
{
    [SerializeField] private Statue statue1;
    [SerializeField] private Statue statue2;
    [SerializeField] private float checkTime;
    private float timer;
    private bool facingEachOther;
    
    [SerializeField] private GameObject cubePickup;
    [SerializeField] private GameObject cubeDisplay;
    [SerializeField] private GameObject pagePickup;
    [SerializeField] private GameObject pageDisplay;
    [SerializeField] private GameObject droorerDoor;

    void Start()
    {
        timer = checkTime;
    }

    void Update()
    {
        if(IsFacingEachOther() == true)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                facingEachOther = true;
            }
        }
        else
        {
            timer = checkTime;
        }

        if(facingEachOther == true)
        {
            cubePickup.SetActive(true);
            cubeDisplay.SetActive(false);
            pagePickup.SetActive(true);
            pageDisplay.SetActive(false);
            droorerDoor.SetActive(false);
        }
    }

    private bool IsFacingEachOther()
    {
        if(statue1.IsFacing == true && statue2.IsFacing == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
