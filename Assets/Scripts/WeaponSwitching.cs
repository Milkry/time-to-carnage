using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    private ItemHandler itemhandler;
    private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
        itemhandler = FindObjectOfType<ItemHandler>().GetComponent<ItemHandler>();
        playerSprite = gameObject.GetComponentInParent<SpriteRenderer>();
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    private void SelectItem(int slot)
    {
        if (!Weapon.isReloading)
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick"); //Replace this sound with something else
            selectedWeapon = slot;
            SelectWeapon();
        }
    }

    public void SelectPistol()
    {
        SelectItem(0);
        playerSprite.sprite = itemhandler.player_pistol;
    }
    public void SelectDeagle()
    {
        SelectItem(1);
        playerSprite.sprite = itemhandler.player_deagle;
    }
    public void SelectMP5()
    {
        SelectItem(2);
        playerSprite.sprite = itemhandler.player_mp5;
    }
    public void SelectP90()
    {
        SelectItem(3);
        playerSprite.sprite = itemhandler.player_p90;
    }
    public void SelectM4()
    {
        SelectItem(4);
        playerSprite.sprite = itemhandler.player_m4;
    }
    public void SelectG3()
    {
        SelectItem(5);
        playerSprite.sprite = itemhandler.player_g3;
    }
    public void SelectShotgun()
    {
        SelectItem(6);
        playerSprite.sprite = itemhandler.player_shotgun;
    }
    public void SelectM40()
    {
        SelectItem(7);
        playerSprite.sprite = itemhandler.player_m40;
    }

    public void OnButtonDown()
    {
        FindObjectOfType<Weapon>().UpdateShoot(true);
    }

    public void OnButtonUp()
    {
        FindObjectOfType<Weapon>().UpdateShoot(false);
    }
}
