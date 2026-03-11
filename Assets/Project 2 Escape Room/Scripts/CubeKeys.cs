using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CubeKeys : MonoBehaviour
{
    [SerializeField] private GameObject correctCube;
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
        if(args.interactorObject.transform.gameObject == correctCube)
        {
            cubeInPlaced = true;
        }
    }
}
