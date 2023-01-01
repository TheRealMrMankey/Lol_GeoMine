using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LoLSDK;

namespace Tiago_GeoMine
{
    public class GameOver : MonoBehaviour
    {
        private GameManager gameManager;

        public TextMeshProUGUI title;
        public TextMeshProUGUI mainText;
        public TextMeshProUGUI menuBtn;
        public TextMeshProUGUI playBtn;

        public void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

#if UNITY_EDITOR
            title.text = gameManager.langDefs[gameManager.langCode]["GameOver_Title"];
            mainText.text = gameManager.langDefs[gameManager.langCode]["GameOver_Text"];
            menuBtn.text = gameManager.langDefs[gameManager.langCode]["BackToMenu"];
            playBtn.text = gameManager.langDefs[gameManager.langCode]["KeepPlaying"];
#elif UNITY_WEBGL
            title.text = gameManager.langDefs["GameOver_Title"];
            mainText.text = gameManager.langDefs["GameOver_Text"];
            menuBtn.text = gameManager.langDefs["BackToMenu"];
            playBtn.text = gameManager.langDefs["KeepPlaying"];
#endif

            LOLSDK.Instance.SpeakText("GameOver_Text");
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void KeepPlaying()
        {
            this.gameObject.SetActive(false);
        }
    }
}
