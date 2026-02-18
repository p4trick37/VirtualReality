using TMPro;
using UnityEngine;

public class CylinderBin : MonoBehaviour
{
    [SerializeField] private TMP_Text binText;
    [SerializeField] private int numberOfItems = 0;
    private Cylinder cylinder;

    private void Update()
    {
        binText.text = "Cylinders: " + numberOfItems;
    }
    public void OnTriggerEnter(Collider other)
    {
        cylinder = other.GetComponent<Cylinder>();
        if (cylinder != null)
        {
            Destroy(other.gameObject);
            numberOfItems++;
        }
    }
}
