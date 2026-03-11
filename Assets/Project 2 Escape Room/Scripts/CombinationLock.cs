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

    public void ButtonPressed(string name)
    {
       
        if(name.Equals("Clear"))
        {
            numbersEntered = "";
        }
        else if(name.Equals("Enter"))
        {
            if(CompareCombinationsInOrder(numbersEntered, numberCombo))
            {
                //Do Job
                door.OpenDoor();
                Debug.Log("Answer Right");
            }
            else
            {
                //Wrong Answer message or sound
                numbersEntered = "";
                Debug.Log("Answer Wrong");
            }

        }
        else if(numbersEntered.Length < numberCombo.Length)
        {
            numbersEntered += name + " ";
            Debug.Log("Button has been Pressed: " + name);
        }
        textPanel.text = numbersEntered;
        Debug.Log(numbersEntered);
    }

    private bool CompareCombinationsInOrder(string userInput, string acutalCombo)
    {
        if(userInput.Equals(acutalCombo))
        {
            return true;
        }
        
        return false;
    }

/*
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
    */
}
