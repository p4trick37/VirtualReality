using UnityEngine;
using UnityEngine.Video;

public class LetterLockManager : MonoBehaviour
{
    [SerializeField] private LetterLock letter1;
    [SerializeField] private LetterLock letter2;
    [SerializeField] private LetterLock letter3;
    [SerializeField] private LetterLock letter4;
    [SerializeField] private LetterLock letter5;
    [SerializeField] private Door door;

    private string answer = "acdaf";
    public bool submittedAnswer = false;
    private bool update = true;
    //Answer is acdaf

    void Update()
    {
        if(submittedAnswer == true && update == true)
        {
            if(CheckForAnswer() == true)
            {
                //Do something on activation
                door.ChangeColorGreen();
                update = false;
            }
            else
            {
                //Do something on fail
                door.ChangeColorRed();
                submittedAnswer = false;
            }
        }
    }


    private bool CheckForAnswer()
    {
        if(letter1.Index == 0 && letter2.Index == 2 && letter3.Index == 3 && letter4.Index == 0 && letter5.Index == 5)
        {
            return true;
        }
        return false;
    }

}
