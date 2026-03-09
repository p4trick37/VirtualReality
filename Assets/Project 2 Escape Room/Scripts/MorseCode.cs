using System.Collections;
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
    

    private string[] morseCodes = new string[]
    {
        ".--- .- -.- .", //Jake
        "-... --- -- -...", //Bomb
        "-.. .-. ..- --", //Drum
        "-... .- -. -..", //Band
        ".--- --- -... ...", //Jobs
        "- .. -.-. -.-", //Tick
        ".-- --- .-. -..", //Word
        "- ..- .-. -.", //Turn
        ".... ..- -. -", //Hunt
        "-..- .-. .- -.--", //Xray
    };
    private int[] morseCodeToNum = new int[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9,};
    private string wordMorseCode;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        //wordMorseCode = RandomizedWord(morseCodes);
        wordMorseCode = "-... . -..- ..";
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(MorseCodeTranslator());
    }

    private IEnumerator MorseCodeTranslator()
    {
        char[] chars = wordMorseCode.ToCharArray();
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

    private string RandomizedWord(string[] wordList)
    {
        int randomNumber = Random.Range(0, wordList.Length);
        return wordList[randomNumber];
    }
}
