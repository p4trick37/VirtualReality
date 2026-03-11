using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MorseCode : MonoBehaviour
{
    [SerializeField] private float dotSecondsOn;
    [SerializeField] private float dashSecondsOn;
    [SerializeField] private float dotDashSecondsOff;
    [SerializeField] private float shortPauseSeconds;
    [SerializeField] private float longPauseSeconds;
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;
    [SerializeField] private CombinationLock combinationLock;
    [SerializeField] private CreateDigitCombination createdCombo;

    public bool startMorseCode;
    private bool hasNotStarted = false;

    private string[] morseCodes = new string[]
    {
        "-----", //0
        ".----", //1
        "..---", //2
        "...--", //3
        "....-", //4
        ".....", //5
        "-....", //6
        "--...", //7
        "---..", //8
        "----.", //9
    };
    private string wordMorseCode;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        //wordMorseCode = DetermineMorseCode(combinationLock.NumberCombo, morseCodes);
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        wordMorseCode = DetermineMorseCode(createdCombo.ComboArray, morseCodes);

        if(startMorseCode == true && hasNotStarted == false)
        {
            hasNotStarted = true;
            StartCoroutine(MorseCodeTranslator());
        }
    }


    private IEnumerator MorseCodeTranslator()
    {
        char[] chars = wordMorseCode.ToCharArray();
        meshRenderer.material.color = offColor;
        yield return new WaitForSeconds(2);
        while(true)
        {
            for(int i = 0; i < chars.Length; i++)
            {
                if(chars[i].Equals('.'))
                {
                    // play dot
                    meshRenderer.material.color = onColor;
                    yield return new WaitForSeconds(dotSecondsOn);
                    meshRenderer.material.color = offColor;
                    yield return new WaitForSeconds(dotDashSecondsOff);
                }
                else if(chars[i].Equals('-'))
                {
                    // play dash
                    meshRenderer.material.color = onColor;
                    yield return new WaitForSeconds(dashSecondsOn);
                    meshRenderer.material.color = offColor;
                    yield return new WaitForSeconds(dotDashSecondsOff);
                }
                else if(chars[i].Equals(' '))
                {
                    // play short pause
                    yield return new WaitForSeconds(shortPauseSeconds);
                }
            }
            yield return new WaitForSeconds(longPauseSeconds);
        }
    }

    
    private string DetermineMorseCode(int[] combo, string[] morseCodes)
    {
        string codeInMorse = "";
        for(int i = 0; i < combo.Length; i++)
        {
            for(int j = 0; j < morseCodes.Length; j++)
            {
                if(combo[i] == j)
                {
                    codeInMorse += morseCodes[j] + " ";
                }
            }
        }
        
        return codeInMorse;
    }
    

  
}
