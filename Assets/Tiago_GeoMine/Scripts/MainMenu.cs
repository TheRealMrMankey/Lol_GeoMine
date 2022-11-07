using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tiago_GeoMine
{
    public class MainMenu : MonoBehaviour
    {
        private GameManager gameManager;

        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
