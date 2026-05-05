using UnityEngine;
using UnityEngine.UI;
namespace FinalProject
{
    public class HealthBar : MonoBehaviour
    {
        private Player player;
        [SerializeField] private Image fillRect;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private Camera mainCamera;
        private void Awake()
        {
            player = FindAnyObjectByType<Player>();
            if(mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        private void Update()
        {
            float xScale = Mathf.InverseLerp(0, player.MaxHealth, player.Health);
            fillRect.rectTransform.localScale = new Vector3(xScale, fillRect.rectTransform.localScale.y, fillRect.rectTransform.localScale.z);
            canvas.LookAt(mainCamera.transform.position);
        }
    }
}
