using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CubeKeys : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable correctCube;
    [SerializeField] private XRSocketInteractor socket;
    public bool CubeInPlaced => cubeInPlaced;

    private bool cubeInPlaced = false;


    private void OnEnable()
    {
        socket.selectEntered.AddListener(ObjectPlaced);
    }
    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(ObjectPlaced);
    }

    private void ObjectPlaced(SelectEnterEventArgs args)
    {
        XRGrabInteractable obj = args.interactableObject.transform.GetComponent<XRGrabInteractable>();
        if(obj == null)
        {
            Debug.Log("Error yea");
        }
        if(obj.gameObject.name.Equals(correctCube.gameObject.name))
        {
            cubeInPlaced = true;
            Debug.Log("Trueeeeee");
        }
    }
}
