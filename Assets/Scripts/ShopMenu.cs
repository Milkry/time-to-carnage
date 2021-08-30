using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject shopMenu;

    public void Exit()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        shopMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenShop()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        shopMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
