using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CubeKeys key1;
    [SerializeField] private CubeKeys key2;
    [SerializeField] private CubeKeys key3;

    [SerializeField] private MorseCode morseCode;
    [SerializeField] private CombinationLock comboLock;

    [SerializeField] private XRSocketInteractor keyInsert;
    [SerializeField] private GameObject door;

    [SerializeField] private AudioSource audiExplode;
    [SerializeField] private AudioSource audiKeyTable;



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
            audiKeyTable.Play();
            return true;
        }
        return false;
    }

    private void StartMorseCode()
    {
        morseCode.startMorseCode = true;
    }

    private void OnEnable()
    {
        keyInsert.selectEntered.AddListener(OnKeyInDoor);
        
    }
    private void OnDisable()
    {
        keyInsert.selectEntered.RemoveListener(OnKeyInDoor);
    }

    private void OnKeyInDoor(SelectEnterEventArgs args)
    {
        audiExplode.Play();
        door.SetActive(false);
    }


}
