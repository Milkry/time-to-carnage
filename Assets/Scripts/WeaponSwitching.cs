using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 1;

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

    public void SelectPistol()
    {
        if (!Weapon.isReloading)
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            selectedWeapon = 0;
            SelectWeapon();
        }
    }

    public void SelectRifle()
    {
        if (!Weapon.isReloading)
        {
            FindObjectOfType<AudioManager>().Play("ButtonClick");
            selectedWeapon = 1;
            SelectWeapon();
        }
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
