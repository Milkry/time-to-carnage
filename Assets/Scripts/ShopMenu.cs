using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject shopMenu;
    [Space]
    [SerializeField] private Button[] itemButtons;
    [SerializeField] private TextMeshProUGUI[] itemPrices;
    [SerializeField] private bool[] itemPurchased; //Might need to set all values to false after restart
    [Space]
    [SerializeField] private Button[] inventory;

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
    
    private void UpdateItem(int itemNumber, int amount)
    {
        itemNumber--;
        if (Balance.CanWithdraw(amount))
        {
            FindObjectOfType<AudioManager>().PlayOnTop("Purchase");
            Balance.Withdraw(amount);
            itemPurchased[itemNumber] = true;
            itemButtons[itemNumber].interactable = false;
            itemPrices[itemNumber].text = "SOLD";
            inventory[itemNumber].interactable = true;
        }
        else
        {
            FindObjectOfType<AudioManager>().PlayOnTop("RejectPurchase");
        }
    }

    public void Item01Selected()
    {
        UpdateItem(1, 550);
    }
    public void Item02Selected()
    {
        UpdateItem(2, 500);
    }
    public void Item03Selected()
    {
        UpdateItem(3, 500);
    }
    public void Item04Selected()
    {
        UpdateItem(4, 500);
    }
    public void Item05Selected()
    {
        UpdateItem(5, 500);
    }
    public void Item06Selected()
    {
        UpdateItem(6, 500);
    }
    public void Item07Selected()
    {
        UpdateItem(7, 500);
    }
    public void Item08Selected()
    {
        UpdateItem(8, 500);
    }
    public void Item09Selected()
    {
        UpdateItem(9, 500);
    }
    public void Item10Selected()
    {
        UpdateItem(10, 500);
    }
}
