using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Drawer : MonoBehaviour
{
    [SerializeField] private string direction; //move to direction
    [SerializeField] private float handleDistance;
    private XRGrabInteractable drawer;
    private Transform interactor;
    private bool shouldMove;

    private void Awake()
    {
        drawer = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        drawer.selectEntered.AddListener(MoveDrawer);
        drawer.selectExited.AddListener(FreezeDrawer);
    }

    private void OnDisable()
    {
        drawer.selectEntered.RemoveListener(MoveDrawer);
        drawer.selectExited.RemoveListener(FreezeDrawer);
    }

    private void Update()
    {
        if(shouldMove == true)
        {
            if(direction.Equals("PosX"))
            {
                float transformation = interactor.position.x - handleDistance;
                transform.position = new Vector3(transformation, transform.position.y, transform.position.z);
            }
            else if(direction.Equals("NegX"))
            {
                float transformation = interactor.position.x + handleDistance;
                transform.position = new Vector3(transformation, transform.position.y, transform.position.z);
            }
            else if(direction.Equals("PosZ"))
            {
                float transformation = interactor.position.z - handleDistance;
                transform.position = new Vector3(transform.position.x, transform.position.y, transformation);
            }
            else
            {
                float transformation = interactor.position.z + handleDistance;
                transform.position = new Vector3(transform.position.x, transform.position.y, transformation);
            }
        }
    }

    private void MoveDrawer(SelectEnterEventArgs args)
    {
        interactor = args.interactorObject.transform;   
        shouldMove = true;
    }

    private void FreezeDrawer(SelectExitEventArgs args)
    {
        shouldMove = false;
    }
}
