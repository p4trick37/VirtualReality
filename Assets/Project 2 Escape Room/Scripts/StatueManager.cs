using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class StatueManager : MonoBehaviour
{
    [SerializeField] private Statue statue1;
    [SerializeField] private Statue statue2;
    [SerializeField] private float checkTime;
    private float timer;
    private bool facingEachOther;
    [SerializeField] private Door door;

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
            door.ChangeColorGreen();
        }
        else
        {
            door.ChangeColorGray();
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
