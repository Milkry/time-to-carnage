using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string PPTutorial;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Theme");
        PPTutorial = FindObjectOfType<SettingsMenu>().PPTutorial;
    }

    public void PlayClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt(PPTutorial, 1);
        //play a tutorial
    }

    public void Upgrades()
    {
        PlayClick();
        //SceneManager.LoadSceneAsync("Upgrades");
    }

    public void QuitGame()
    {
        PlayClick();
        Application.Quit();
    }
}
