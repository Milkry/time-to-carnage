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

    public void PlayGame()
    {
        //Can load the next scene by doing
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Game");
    }

    public void Upgrades()
    {
        //SceneManager.LoadScene("Upgrades");
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Application.Quit();
    }
}
