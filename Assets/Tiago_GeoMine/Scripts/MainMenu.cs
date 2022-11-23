using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Tiago_GeoMine
{
    public class MainMenu : MonoBehaviour
    {
        private GameManager gameManager;

        public TextMeshProUGUI newGameBtn;
        public TextMeshProUGUI continueBtn;

        void Awake()
        {
            
        }

        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            // Set text based on current selected language
            newGameBtn.text = gameManager.currentLanguage.NewGame;
            continueBtn.text = gameManager.currentLanguage.Continue;

            Debug.Log("Button Text: " + gameManager.currentLanguage.NewGame);
        }

        public void NewGame()
        {
            SceneManager.LoadScene(1);

            gameManager.ResetGameData();
            
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
 
        }
    }
}
