using UnityEngine;
using TMPro;


public class NumberLock : MonoBehaviour
{
    public NumberLockButton button;
    public string NumberCombo => numberCombo;
    [SerializeField] private TMP_Text keypadPanel;
    [SerializeField] private TMP_Text binaryPanel;
    [SerializeField] private string numbersEntered = "";
    private string numberCombo;
    private string binaryCombo;

    [SerializeField] private GameObject cubePickup;
    [SerializeField] private GameObject cubeDisplay;
    [SerializeField] private GameObject droorerDoor;
    

    void Start()
    {
        binaryCombo = randomBinaryCode(5);
        button = null;
        numberCombo = BinToNum(binaryCombo);
        binaryPanel.text = binaryCombo;
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
                if(CompareCombinationsInOrder(numbersEntered, SpaceNumber(numberCombo)))
                {
                    //Do Job
                    cubePickup.SetActive(true);
                    cubeDisplay.SetActive(false);
                    droorerDoor.SetActive(false);
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
        keypadPanel.text = numbersEntered;
    }

    private bool CompareCombinationsInOrder(string userInput, string acutalCombo)
    {
        if(userInput.Equals(acutalCombo))
        {
            return true;
        }
        
        return false;
    }

    private string BinToNum(string binary)
    {
        float number = 0;
        char[] chars = binary.ToCharArray();
        int j = 0;
        for(int i = chars.Length - 1; i >= 0; i--)
        {
            if(chars[i].Equals('1'))
            {
                number += Mathf.Pow(2f, (float)j);
            }
            j++;
        }
        string numberString = (int)number + "";
        return numberString;
    }

    private string randomBinaryCode(int size)
    {
        string binary = "";
        for(int i = 0; i < size; i++)
        {
            binary += Random.Range(0, 2);
        }
        return binary;
    }

    private string SpaceNumber(string number)
    {
        string spacedNumber = "";
        char[] chars = number.ToCharArray();
        for(int i = 0; i < chars.Length; i++)
        {
            spacedNumber += chars[i] + " ";
        }
        return spacedNumber;
    }

}