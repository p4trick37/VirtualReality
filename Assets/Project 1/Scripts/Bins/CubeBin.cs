using TMPro;
using UnityEngine;

public class CubeBin : MonoBehaviour
{
    [SerializeField] private TMP_Text binText;
    [SerializeField] private int numberOfItems = 0;
    private Cube cube;

    private void Update()
    {
        binText.text = "Cubes: " + numberOfItems;
    }
    public void OnTriggerEnter(Collider other)
    {
        cube = other.GetComponent<Cube>();
        if (cube != null)
        {
            Destroy(other.gameObject);
            numberOfItems++;
        }
    }
}
