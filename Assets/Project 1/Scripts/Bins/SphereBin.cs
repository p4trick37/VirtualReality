using TMPro;
using UnityEngine;

public class SphereBin : MonoBehaviour
{
    [SerializeField] private TMP_Text binText;
    [SerializeField] private int numberOfItems = 0;
    private Sphere sphere;

    private void Update()
    {
        binText.text = "Spheres: " + numberOfItems;
    }
    public void OnTriggerEnter(Collider other)
    {
        sphere = other.GetComponent<Sphere>();
        if (sphere != null)
        {
            Destroy(other.gameObject);
            numberOfItems++;
        }
    }
}
