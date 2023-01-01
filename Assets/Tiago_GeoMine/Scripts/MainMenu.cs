using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LoLSDK;

namespace Tiago_GeoMine
{
    public class MainMenu : MonoBehaviour
    {
        private GameManager gameManager;

        public GameObject continueButton;

        public TextMeshProUGUI newGameBtn;
        public TextMeshProUGUI continueBtn;

        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            // Set text based on current selected language
            newGameBtn.text = gameManager.currentLanguage.NewGame;
            continueBtn.text = gameManager.currentLanguage.Continue;

            LoadLastSave<CurrentSaveData>(GetLastSave);
        }

        public void GetLastSave(CurrentSaveData lastSave)
        {
            if (lastSave == gameManager.saveData)
                continueButton.SetActive(false);
            else
                continueButton.SetActive(true);
        }

        public static void LoadLastSave<T>(System.Action<T> callback)
        {
            LOLSDK.Instance.LoadState<T>(state =>
            {
                callback(state.data);
            });
        }

        public void NewGame()
        {
            SceneManager.LoadScene(1);

            gameManager.ResetGameData();
            gameManager.isNewGame = true;
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);

            gameManager.StartLoading();
            gameManager.isNewGame = false;
        }
    }
}
