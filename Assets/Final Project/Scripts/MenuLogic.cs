using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FinalProject
{
    public class MenuLogic : MonoBehaviour
    {
        [SerializeField] private TMP_Text waveText;
        public void GoToGameplay()
        {
            SceneManager.LoadScene(1);
        }

        public void Awake()
        {
            waveText.text = WaveManager.staticWave.ToString();
        }


    }
}
