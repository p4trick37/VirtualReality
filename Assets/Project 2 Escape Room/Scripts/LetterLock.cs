using UnityEngine;

public class LetterLock : MonoBehaviour
{
    public int Index => index;

    [SerializeField] private LetterLockManager manager;
    private char[] letterOptions = new char[]{'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'};
    private int index;


    public void Rotate45()
    {
        if(manager.submittedAnswer == false)
        {
            transform.Rotate(0, 0, -45);
            index++;
            if(index == letterOptions.Length)
            {
                index = 0;
            }
        }
    }

    public void Submit()
    {
        manager.submittedAnswer = true;
    }
}
