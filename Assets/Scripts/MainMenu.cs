using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Theme");
    }

    public void PlayClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
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
