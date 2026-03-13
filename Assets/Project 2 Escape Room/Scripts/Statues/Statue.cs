using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class Statue : MonoBehaviour
{
    [SerializeField] private StatueManager manager;
    [SerializeField] private Rigidbody rb;
    public bool IsFacing => isFacing;
    private bool isFacing;
    private bool statueGrabbed;
    private XRGrabInteractable item;
    private Transform interactor; 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        item = GetComponent<XRGrabInteractable>();
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

        if(statueGrabbed == true)
        {
            Debug.Log("In update, the statuegrabbed bool ran the code");
            transform.rotation = Quaternion.Euler(0, AngleToInteractor(interactor.position, transform.position), 0);
        }
    }

    public void OnRelease()
    {
        rb.freezeRotation = true;
        statueGrabbed = false;
        Debug.Log("Hand has been released");
    }

    public void OnGrab()
    { 
        rb.freezeRotation = false;
        statueGrabbed = true;
        Debug.Log("Hand as been grabbed");
    }

    private void OnEnable()
    {
        item.selectEntered.AddListener(OnInteraction);
    }
    private void OnDisable()
    {
        item.selectEntered.RemoveListener(OnInteraction);
    }

    private void OnInteraction(SelectEnterEventArgs args)
    {
        interactor = args.interactorObject.transform;
        Debug.Log("I think interactor has been set I think");
        Debug.Log("This is the interactor: " + interactor.name);
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
