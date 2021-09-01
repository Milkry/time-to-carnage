using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
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
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            selectedWeapon = slot;
            SelectWeapon();
        }
    }

    public void SelectPistol()
    {
        SelectItem(0);
    }
    public void SelectDeagle()
    {
        SelectItem(1);
    }
    public void SelectMP5()
    {
        SelectItem(2);
    }
    public void SelectP90()
    {
        SelectItem(3);
    }
    public void SelectM4()
    {
        SelectItem(4);
    }
    public void SelectWeaponName1()
    {
        SelectItem(5);
    }
    public void SelectWeaponName2()
    {
        SelectItem(6);
    }
    public void SelectWeaponName3()
    {
        SelectItem(7);
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
