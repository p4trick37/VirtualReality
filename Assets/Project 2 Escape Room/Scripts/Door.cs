using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Door : MonoBehaviour
{

    [SerializeField] private float targetRotation;
    [SerializeField] private Transform parentObj;
    public bool isLocked;
    private XRGrabInteractable door;
    private Transform interactor;
    private bool moveDoor = false;
    private Vector3 ogPos;
    private float previousAngle;
    private float angle;

    private void Awake()
    {
        door = GetComponent<XRGrabInteractable>();
        ogPos = transform.position;
    }

    private void OnEnable()
    {
        door.selectEntered.AddListener(OpenCloseDoor);
        door.selectExited.AddListener(FreezeDoor);
    }
    private void OnDisable()
    {
        door.selectEntered.RemoveListener(OpenCloseDoor);
        door.selectExited.RemoveListener(FreezeDoor);
    }

    private void Update()
    {
        transform.position = ogPos;
        if(moveDoor == true && isLocked == false)
        {
            //if(parentObj.rotation.y == 0)
            //{
                if(targetRotation < 0)
                {
                    previousAngle = AngleToInteractor(interactor.position, transform.position) + 90;
                    angle = Mathf.Clamp(previousAngle, targetRotation, 0);
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                }
                else
                {
                    previousAngle = AngleToInteractor(interactor.position, transform.position) - 90;
                    angle = Mathf.Clamp(previousAngle, 0, targetRotation);
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                }
            //}
           // else if(parentObj.rotation.y == 90)
           // {
               // if(targetRotation < 0)
               // {
                //    previousAngle = AngleToInteractor(interactor.position, transform.position);
               //     angle = Mathf.Clamp(previousAngle, targetRotation, 0);
               //     transform.localRotation = Quaternion.Euler(0, angle, 0);
              //  }
              //  else
              //  {
               //     previousAngle = AngleToInteractor(interactor.position, transform.position) - 180;
               //     angle = Mathf.Clamp(previousAngle, 0, targetRotation);
               //     transform.localRotation = Quaternion.Euler(0, angle, 0);
               // }
        //    }
            
            
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        Debug.Log(angle);
        Debug.Log(transform.rotation.y);
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
        Debug.Log("This is the angle where it is: " + angle);
        return angle;
    }
}
