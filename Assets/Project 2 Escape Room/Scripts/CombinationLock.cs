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

    private CreateDigitCombination createCombo;

    void Start()
    {
        createCombo = GetComponent<CreateDigitCombination>();
        numberCombo = createCombo.SpacedCombo;
        button = null;
    }

    void Update()
    {
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
                    door.OpenDoor();
                }
                else if(CompareCombinationsOutOrder(numbersEntered, numberCombo) && !needInOrder)
                {
                    //Do Job
                    door.OpenDoor();
                }
                else
                {
                    //Wrong Answer message or sound
                    numbersEntered = "";
                }

            }
            else if(numbersEntered.Length < numberCombo.Length)
            {
                numbersEntered += button.ButtonName + " ";
                Debug.Log("Button has been Pressed: " + button.name);
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

    private bool CompareCombinationsOutOrder(string userInput, string acutalCombo)
    {
        char[] charUserInput = userInput.ToCharArray();
        char[] charCombination = acutalCombo.ToCharArray();
        for(int i = 0; i < charUserInput.Length; i++)
        {
            bool isInArray = false;
            for(int j = 0; j < charCombination.Length; j++)
            {
                if(charUserInput[i].Equals(charCombination[j]))
                {
                    isInArray = true;
                }
            }

            if(isInArray == false)
            {
                return false;
            }
        }
        return true;
    }
}
