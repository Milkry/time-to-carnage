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
    [Space]
    [SerializeField] private Button[] inventory;
    [SerializeField] private GameObject inventoryLayout;
    [SerializeField] private GameObject sideInventory;

    private int[] prices = { 650, 1150, 1800, 2250, 2500, 0, 5000, 250, 400, 0 }; // shotgun was [1600]

    private void Start()
    {
        for (int i = 0; i < itemPrices.Length; i++)
        {
            itemPrices[i].text = "$" + prices[i].ToString();
        }
    }

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

            if (itemNumber >= 7 && itemNumber <= 9) //Items 7 to 9 are packs
            {
                //sideInventory.SetActive(true);

                switch (itemNumber)
                {
                    case 7:
                        SideInventory.AddHealthpacks(1);
                        break;

                    case 8:
                        SideInventory.AddAmmopacks(1);
                        break;

                    case 9:
                        SideInventory.AddGrenades(1);
                        break;

                    default:
                        Debug.LogWarning($"No such itemNumber exists. Searched for {itemNumber}");
                        break;
                }
            }
            else
            {
                itemButtons[itemNumber].interactable = false;
                itemPrices[itemNumber].text = "SOLD";
                inventoryLayout.SetActive(true);
                inventory[itemNumber].interactable = true;
                inventory[itemNumber].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().PlayOnTop("RejectPurchase");
        }
    }

    public void Item01Selected()
    {
        UpdateItem(1, prices[0]);
    }
    public void Item02Selected()
    {
        UpdateItem(2, prices[1]);
    }
    public void Item03Selected()
    {
        UpdateItem(3, prices[2]);
    }
    public void Item04Selected()
    {
        UpdateItem(4, prices[3]);
    }
    public void Item05Selected()
    {
        UpdateItem(5, prices[4]);
    }
    public void Item06Selected()
    {
        UpdateItem(6, prices[5]);
    }
    public void Item07Selected()
    {
        UpdateItem(7, prices[6]);
    }
    public void Item08Selected()
    {
        UpdateItem(8, prices[7]);
    }
    public void Item09Selected()
    {
        UpdateItem(9, prices[8]);
    }
    public void Item10Selected()
    {
        UpdateItem(10, prices[9]);
    }
}
