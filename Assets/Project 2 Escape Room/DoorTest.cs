using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DoorTest : MonoBehaviour
{

    [SerializeField] private float targetRotation;
    [SerializeField] private Transform parentObj;
    [SerializeField] private Transform hand;
    public bool isLocked;
    private XRGrabInteractable door;
    private Transform interactor;
    private bool moveDoor = true;
    private Vector3 ogPos;
    private float previousAngle;
    private float angle;
    private bool initialFreeze = true;



    private void Awake()
    {
        door = GetComponent<XRGrabInteractable>();
        ogPos = transform.position;
        initialFreeze = true;
    }


    private void Update()
    {
        
        transform.position = ogPos;
        if(moveDoor == true && isLocked == false)
        {
            initialFreeze = false;
            if(targetRotation < 0)
            {
                previousAngle = AngleToInteractor(hand.position, transform.position) + 90;
                //angle = Mathf.Clamp(previousAngle, targetRotation, 0);
                transform.rotation = Quaternion.Euler(0, previousAngle, 0);
            }
            else
            {
                previousAngle = AngleToInteractor(hand.position, transform.position) - 90;
                //angle = Mathf.Clamp(previousAngle, 0, targetRotation);
                transform.rotation = Quaternion.Euler(0, previousAngle, 0);
            }
        }
        else if(initialFreeze == false)
        {
            transform.rotation = Quaternion.Euler(0, previousAngle, 0);
        }
    }

    private void OpenCloseDoor(SelectEnterEventArgs args)
    {
        interactor = args.interactorObject.transform;
        moveDoor = true;

    }

    private void FreezeDoor(SelectExitEventArgs args)
    {
        moveDoor = false;
    }

    private float AngleToInteractor(Vector3 interactor, Vector3 stationaryObj)
    {
        Vector3 interObj = interactor - stationaryObj;
        float angle = Mathf.Atan(interObj.z / interObj.x) * Mathf.Rad2Deg;

        if((interObj.x > 0 && interObj.z > 0) || (interObj.x < 0 && interObj.z < 0))
        {
            angle = -(90 + angle); 
            if(interObj.x < 0 && interObj.z < 0)
            {
                angle += 180;
            }

        }
        else
        {
            angle = 90 - angle;
            if(interObj.x > 0 && interObj.z < 0)
            {
                angle += 180;
            }
        }
        return angle;
    }
}
