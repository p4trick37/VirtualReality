using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace FinalProject
{
    public class Belt : MonoBehaviour
    {
        [Header("Player attributes")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform rotatePoint;
        [Header("Sockets")]
        [SerializeField] private XRSocketInteractor[] sockets;
        [SerializeField] private GameObject[] healthPotions;
        [SerializeField] private int socketSize;
        private Vector3 beltOffset;

        private void Awake()
        {
            beltOffset = CalculateOffset();
            SetPotionPositions();
        }
        private void SetPotionPositions()
        {
            for(int i = 0; i < healthPotions.Length; i++)
            {
                healthPotions[i].transform.position = sockets[i].transform.position;
            }
        }

        private void Update()
        {
            FollowCameraRotation();
            TrackCameraPosition();
        }

        private void OnEnable()
        {
            for(int i = 0; i < sockets.Length; i++)
            {
                sockets[i].selectEntered.AddListener(InSocketLogic);
                sockets[i].selectExited.AddListener(OutSocketLogic);
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < sockets.Length; i++)
            {
                sockets[i].selectEntered.RemoveListener(InSocketLogic);
                sockets[i].selectExited.RemoveListener(OutSocketLogic);
            }
        }

        private void InSocketLogic(SelectEnterEventArgs args)
        {
            HealthPotion potionFound= args.interactableObject.transform.gameObject.GetComponent<HealthPotion>();
            if(potionFound != null)
            {
                potionFound.InBeltState();
            }
        }

        private void OutSocketLogic(SelectExitEventArgs args)
        {
            HealthPotion potionFound = args.interactableObject.transform.gameObject.GetComponent<HealthPotion>();
            if(potionFound != null)
            {
                potionFound.OutBeltState();
            }
        }

        private void FollowCameraRotation()
        {
            //rotatePoint.rotation = Camera.main.transform.rotation;
            rotatePoint.localRotation = Quaternion.Euler(rotatePoint.localRotation.x, mainCamera.transform.localRotation.eulerAngles.y + 90, rotatePoint.localRotation.z);
        }

        private void TrackCameraPosition()
        {
            transform.position = mainCamera.transform.position - beltOffset;
        }

        private Vector3 CalculateOffset()
        {
            Vector3 offset = mainCamera.transform.position - transform.position;
            return offset;
        }  
    }
}
