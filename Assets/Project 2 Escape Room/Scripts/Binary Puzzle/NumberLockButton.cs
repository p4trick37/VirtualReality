using UnityEngine;
using UnityEngine.UI;

public class NumberLockButton : MonoBehaviour
{
    public string ButtonName => buttonName;
    private Vector3 ogPos;
    private bool beenPressed;
    private NumberLock numberLock;

    [SerializeField] private string buttonName;

    [SerializeField] private AudioSource audi;
    void Start()
    {
        ogPos = transform.position;
        numberLock = GetComponentInParent<NumberLock>();
    }

    void Update()
    {
        if(transform.position.z <= ogPos.z - 0.04 && beenPressed == false)
        {
            ButtonPressed();
            audi.Play();
        }

        if(transform.position == ogPos)
        {
            beenPressed = false;
        }
    }

    private void ButtonPressed()
    {
        beenPressed = true;
        numberLock.button = this;
        //Debug.Log(ButtonName + " has been pushed");
    }
}
