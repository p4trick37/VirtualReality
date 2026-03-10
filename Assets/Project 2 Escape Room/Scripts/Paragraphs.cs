using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Paragraphs : MonoBehaviour
{
    private XRGrabInteractable paragraphGrab;
    [SerializeField] private Transform leftAttach;
    [SerializeField] private Transform rightAttach;

    void OnEnable()
    {
        paragraphGrab = GetComponent<XRGrabInteractable>();
        paragraphGrab.selectEntered.AddListener(OnGrab);
    }
    void OnDisable()
    {
        paragraphGrab.selectEntered.RemoveListener(OnGrab);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.CompareTag("Left Controller"))
        {
            paragraphGrab.attachTransform = leftAttach;
        }
        else
        {
            paragraphGrab.attachTransform = rightAttach;
        }
    }

}
