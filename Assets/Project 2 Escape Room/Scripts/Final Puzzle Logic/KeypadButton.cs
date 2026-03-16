using UnityEngine;
using UnityEngine.UI;

public class KeypadButton : MonoBehaviour
{
    public string ButtonName => buttonName;
    private Vector3 ogPos;
    private bool beenPressed;
    private CombinationLock comboLock;

    [SerializeField] private string buttonName;

    [SerializeField] private AudioSource audi;
    void Start()
    {
        ogPos = transform.position;
        comboLock = GetComponentInParent<CombinationLock>();
    }

    void Update()
    {
        if(transform.position.x >= ogPos.x + 0.04 && beenPressed == false)
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
        comboLock.button = this;
        //Debug.Log(ButtonName + " has been pushed");
    }
}
