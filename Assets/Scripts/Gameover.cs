using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    [SerializeField] private GameObject gameover;

    public void GameOver()
    {
        gameover.SetActive(true);
        Time.timeScale = 0f;
    }
}
