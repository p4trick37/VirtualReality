using UnityEngine;
using TMPro;

public class CapsleBin : MonoBehaviour
{
    [SerializeField] private TMP_Text binText;
    [SerializeField] private int numberOfItems = 0;
    private Capsule capsule;

    private void Update()
    {
        binText.text = "Capsules: " + numberOfItems;
    }
    public void OnTriggerEnter(Collider other)
    {
        capsule = other.GetComponent<Capsule>();
        if(capsule != null)
        {
            Destroy(other.gameObject);
            numberOfItems++;
        }
    }
}
