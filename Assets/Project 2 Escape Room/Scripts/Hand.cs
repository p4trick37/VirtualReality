using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PagePickup : MonoBehaviour
{
    private XRGrabInteractable page;
    private GameObject hand;

    void Awake()
    {
        page = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        page.selectEntered.AddListener(SetTransform);
        
    }
    private void OnDisable()
    {
        page.selectEntered.RemoveListener(SetTransform);
    }

    private void SetTransform(SelectEnterEventArgs args)
    {
        hand = args.interactorObject.transform.gameObject;
    }
}
