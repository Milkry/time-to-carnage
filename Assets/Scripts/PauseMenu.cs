using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void Pause()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //If player has not died yet, they can go "home" and continue later
    public static void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(sceneID);
        Balance.Reset();
        FindObjectOfType<AudioManager>().StopAll();
    }
}
