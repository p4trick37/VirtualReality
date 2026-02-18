using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Player : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable gun;
    [SerializeField] private Transform head;
    public float movementSpeed;
    [SerializeField] private float maxFlySpeed;
    [Header("Hands")]
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    private Vector3 direction;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider gunCollider;
    private void Start()
    {
        Collider playerCollider = GetComponent<Collider>();
        Physics.IgnoreCollision(playerCollider, gunCollider);
    }

    private void Update()
    {
        rb.linearVelocity = direction * movementSpeed;
    }

    private void OnEnable()
    {
        gun.activated.AddListener(OnActivate);
        gun.deactivated.AddListener(OnDeactivate);
    }

    private void OnDisable()
    {
        gun.activated.RemoveListener(OnActivate);
        gun.deactivated.RemoveListener(OnDeactivate);
    }

    private void OnActivate(ActivateEventArgs args)
    {
        direction = gun.transform.forward;
        movementSpeed = maxFlySpeed;
    }

    private void OnDeactivate(DeactivateEventArgs args)
    {

    }

    public void OnLeftPickup()
    {
        leftHand.SetActive(false);
        
    }

    public void OnRightPickup()
    {
        rightHand.SetActive(false);
    }

    public void OnLeftDrop()
    {
        leftHand.SetActive(true);
    }

    public void OnRightDrop()
    {
        rightHand.SetActive(true);
    }


}
