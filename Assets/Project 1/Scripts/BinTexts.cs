using UnityEngine;

public class BinTexts : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private RectTransform rectTransform;
    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        Vector3 playerPosition = player.transform.position - rectTransform.position;
        float angle = Mathf.Atan(playerPosition.z / playerPosition.x) * Mathf.Rad2Deg;

        if((playerPosition.x > 0 && playerPosition.z > 0) || (playerPosition.x < 0 && playerPosition.z < 0))
        {
            angle = -(90 + angle); 
            if(playerPosition.x < 0 && playerPosition.z < 0)
            {
                angle += 180;
            }

        }
        else
        {
            angle = 90 - angle;
            if(playerPosition.x > 0 && playerPosition.z < 0)
            {
                angle += 180;
            }
        }
        rectTransform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
