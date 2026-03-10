using UnityEngine;
using TMPro;
using UnityEditor.ShortcutManagement;
using System.Linq;

public class CombinationLock : MonoBehaviour
{
    public KeypadButton button;
    [SerializeField] private TMP_Text textPanel;
    [SerializeField] private string numbersEntered = "";
    [SerializeField] private bool needInOrder;
    [SerializeField] private int combinationLength;
    [SerializeField] private Door door;
    private string numberCombo;

    void Start()
    {
        //combination = Combination(combinationLength);
        numberCombo = "1 2 3 4 ";
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


    private string Combination(int length)
    {
        string combination = "";
        for(int i = 0; i < length; i++)
        {
            combination += Random.Range(0, 10).ToString() + " ";
        }
        return combination;
    }
}
