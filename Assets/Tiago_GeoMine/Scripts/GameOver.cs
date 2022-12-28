using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void KeepPlaying()
    {
        this.gameObject.SetActive(false);
    }
}
