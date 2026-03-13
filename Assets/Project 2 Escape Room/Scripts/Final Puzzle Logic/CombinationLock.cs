using UnityEngine;
using TMPro;


public class CombinationLock : MonoBehaviour
{
    public KeypadButton button;
    public string NumberCombo => numberCombo;
    [SerializeField] private TMP_Text textPanel;
    [SerializeField] private string numbersEntered = "";
    [SerializeField] private bool needInOrder;
    [SerializeField] private Door door;
    private string numberCombo;

    [SerializeField] private CreateDigitCombination createCombo;
    [SerializeField] private GameObject spawnKey;

    void Start()
    {
        button = null;
    }

    void Update()
    {
        numberCombo = createCombo.SpacedCombo;
        if(button != null)
        {
            if(button.ButtonName.Equals("Clear"))
            {
                numbersEntered = "";
            }
            else if(button.ButtonName.Equals("Enter"))
            {
                if(CompareCombinationsInOrder(numbersEntered, numberCombo))
                {
                    //Do Job
                    spawnKey.SetActive(true);
                    Debug.Log("Answer Right");
                }
                else
                {
                    //Wrong Answer message or sound
                    numbersEntered = "";
                    Debug.Log("Answer Wrong");
                }

            }
            else 
            {
                numbersEntered += button.ButtonName + " ";
                Debug.Log("Button has been Pressed: " + button.ButtonName);
            }
        }

        button = null;
        textPanel.text = numbersEntered;
    }

    private bool CompareCombinationsInOrder(string userInput, string acutalCombo)
    {
        if(userInput.Equals(acutalCombo))
        {
            return true;
        }
        
        return false;
    }
}
