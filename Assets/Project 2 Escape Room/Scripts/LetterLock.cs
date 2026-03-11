using UnityEngine;

public class LetterLock : MonoBehaviour
{
    public int Index => index;

    [SerializeField] private LetterLockManager manager;
    private char[] letterOptions = new char[]{'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'};
    private int index;
    private bool shouldRotate;
    [SerializeField] private float rotationSpeed;
    private float currentRotation;
    private float targetRotation;

    void Start()
    {
        currentRotation = 0;
        targetRotation = -45;
    }

    void Update()
    {
        if(shouldRotate == true)
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
            if(transform.rotation.z < targetRotation)
            {
                transform.rotation = Quaternion.Euler(0, 0, targetRotation);   
                currentRotation = targetRotation;
                targetRotation -= 45;
                shouldRotate = false;
            }
        }
    }


    public void Rotate45()
    {
        if(manager.submittedAnswer == false)
        {
            shouldRotate = true;
            index++;
            if(index == letterOptions.Length)
            {
                index = 0;
            }
        }
        Debug.Log("Rotate be atch");
    }

    public void Submit()
    {
        manager.submittedAnswer = true;
    }
}
