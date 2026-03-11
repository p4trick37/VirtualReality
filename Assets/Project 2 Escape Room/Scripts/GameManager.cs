using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CubeKeys key1;
    [SerializeField] private CubeKeys key2;
    [SerializeField] private CubeKeys key3;

    [SerializeField] private MorseCode morseCode;
    [SerializeField] private CombinationLock comboLock;


    void Update()
    {
        if(KeysAreCorrect())
        {
            StartMorseCode();
        }
    }

    private bool KeysAreCorrect()
    {
        if(key1.CubeInPlaced == true && key2.CubeInPlaced == true && key3.CubeInPlaced == true)
        {
            return true;
        }
        return false;
    }

    private void StartMorseCode()
    {
        morseCode.startMorseCode = true;
    }


}
