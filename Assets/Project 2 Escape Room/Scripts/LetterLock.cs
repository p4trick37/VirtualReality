using UnityEngine;

public class LetterLock : MonoBehaviour
{
    public int Index => index;

    [SerializeField] private LetterLockManager manager;
    private char[] letterOptions = new char[]{'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'};
    private int index;
    private bool shouldRotate = false;
    [SerializeField] private float rotationSpeed;
    private float currentRotation;
    private float targetRotation;

    void Start()
    {
        currentRotation = 0;
        targetRotation = 315;
    }

    void Update()
    {
        if(shouldRotate == true)
        {
            if(targetRotation < 0)
            {
                targetRotation = 315; 
            }
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
            if(transform.eulerAngles.z < targetRotation || (targetRotation == 0 && transform.eulerAngles.z > 315))
            {
                transform.rotation = Quaternion.Euler(0, 0, targetRotation);   
                currentRotation = targetRotation;
                targetRotation -= 45;
                index++;
                if(index == letterOptions.Length)
                {
                    index = 0;
                }
                shouldRotate = false;
            }
        }
    }


    public void Rotate45()
    {
        if(manager.submittedAnswer == false)
        {
            shouldRotate = true;
        }
    }

    public void Submit()
    {
        manager.submittedAnswer = true;
    }
}
