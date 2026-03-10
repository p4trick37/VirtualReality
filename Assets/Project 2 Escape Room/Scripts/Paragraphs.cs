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
        paragraphGrab.selectExited.AddListener(OffGrab);
    }
    void OnDisable()
    {
        paragraphGrab.selectEntered.RemoveListener(OnGrab);
        paragraphGrab.selectExited.RemoveListener(OffGrab);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.CompareTag("Left Controller"))
        {
            paragraphGrab.attachTransform = leftAttach;
            paragraphGrab.secondaryAttachTransform = rightAttach;
        }
        else
        {
            paragraphGrab.attachTransform = rightAttach;
            paragraphGrab.secondaryAttachTransform = leftAttach;
        }
    }

    void OffGrab(SelectExitEventArgs args)
    {
        paragraphGrab.attachTransform = null;
        paragraphGrab.secondaryAttachTransform = null;
    }

}
