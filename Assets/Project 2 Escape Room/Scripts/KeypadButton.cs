using UnityEngine;
using UnityEngine.UI;

public class KeypadButton : MonoBehaviour
{
    public string ButtonName => buttonName;
    private Vector3 ogPos;
    private bool beenPressed;
    private CombinationLock comboLock;

    [SerializeField] private string buttonName;
    void Start()
    {
        ogPos = transform.position;
        comboLock = GetComponentInParent<CombinationLock>();
    }

    void Update()
    {
        if(transform.position.z <= ogPos.z - 0.04 && beenPressed == false)
        {
            ButtonPressed();
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
        Debug.Log(ButtonName + " has been pushed");
    }
}
