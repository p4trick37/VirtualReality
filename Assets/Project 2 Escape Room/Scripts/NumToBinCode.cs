using TMPro;
using UnityEngine;

public class NumToBinCode : MonoBehaviour
{
    private int numbCode;
    private string binaryCode;
    [SerializeField] private TMP_Text binaryCodeShown;
    [SerializeField] private int binarySize;

    void Start()
    {
        binaryCode = randomBinaryCode(binarySize);
        binaryCodeShown.text = binaryCode;
    }


    private int BinToNum(string binary)
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
        return (int)number;
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

    //start a loop that starts at the last index of array, all the way down to 0
    // check for if the char == 1
}
