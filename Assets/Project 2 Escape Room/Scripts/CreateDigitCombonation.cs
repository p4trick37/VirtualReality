using Unity.VisualScripting;
using UnityEngine;

public class CreateDigitCombination : MonoBehaviour
{
    public string SpacedCombo => spacedCombo;
    public string NotSpacedCombo => notSpacedCombo;
    public int[] ComboArray => comboArray;

    private string spacedCombo;
    private string notSpacedCombo;
    private int[] comboArray;
    [SerializeField] private int comboLength;

    void Start()
    {
        spacedCombo = SpacedCombination(comboLength);
        notSpacedCombo = NotSpacedCombination(spacedCombo);
        comboArray = CodeInArray(notSpacedCombo);
    }


    public string SpacedCombination(int length)
    {
        string combination = "";
        for(int i = 0; i < length; i++)
        {
            combination += Random.Range(0, 10) + " ";
        }
        return combination;
    }

    public string NotSpacedCombination(string combo)
    {
        string combination = "";
        char[] chars = combo.ToCharArray();
        for(int i = 0; i < chars.Length; i++)
        {
            if(!chars[i].Equals(' '))
            {
                combination += chars[i];
            }
        }
        return combination;
    }

    public int[] CodeInArray(string code)
    {
        char[] chars = code.ToCharArray();
        int[] ints = new int[chars.Length];
        for(int i = 0; i < ints.Length; i++)
        {
            ints[i] = chars[i] - '0';
        }
        return ints;
    }
}
