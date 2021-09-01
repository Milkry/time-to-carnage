using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SideInventory : MonoBehaviour
{
    [Space]
    [Header("STARTING AMOUNT OF PACKS")]
    [Space]
    [SerializeField] private int healthpackStartingAmount = 0;
    [SerializeField] private int ammopackStartingAmount = 0;
    [SerializeField] private int grenadeStartingAmount = 0;

    [Space]
    [Header("HEALTHPACK")]
    [Space]
    [SerializeField] private int healAmount;

    [Space]
    [Header("AMMOPACK")]
    [Space]
    [SerializeField] private int deagleMagazines;
    [SerializeField] private int mp5Magazines;
    [SerializeField] private int p90Magazines;
    [SerializeField] private int m4Magazines;

    //Add something for grenades

    [Space]
    [SerializeField] private TextMeshProUGUI healthpacks;
    [SerializeField] private TextMeshProUGUI ammopacks;
    [SerializeField] private TextMeshProUGUI grenades;

    private float nextTick = 0f;
    private float updateRate = 0.2f;
    public static int currentHealthpacks;
    public static int currentAmmopacks;
    public static int currentGrenades;
    private PlayerController player;

    private void Start()
    {
        currentHealthpacks = healthpackStartingAmount;
        currentAmmopacks = ammopackStartingAmount;
        currentGrenades = grenadeStartingAmount;
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Time.time > nextTick)
        {
            nextTick = Time.time + updateRate;
            healthpacks.text = currentHealthpacks.ToString();
            ammopacks.text = currentAmmopacks.ToString();
            grenades.text = currentGrenades.ToString();
        }
    }

    public static void AddHealthpacks(int amount)
    {
        currentHealthpacks += amount;
    }

    public static void AddAmmopacks(int amount)
    {
        currentAmmopacks += amount;
    }

    public static void AddGrenades(int amount)
    {
        currentGrenades += amount;
    }

    public void UseHealthpack()
    {
        if (currentHealthpacks > 0 && player.currentHealth < player.maxHealth)
        {
            FindObjectOfType<PlayerController>().GetComponent<PlayerController>().AddHealth(healAmount);
            currentHealthpacks--;
        }
    }

    public void UseAmmopack()
    {
        if (currentAmmopacks > 0)
        {
            var guns = FindObjectsOfType<Weapon>(true);
            currentAmmopacks--;
            for (int i = 1; i < guns.Length; i++) //i = 1 so it would ignore the default pistol
            {
                switch (guns[i].gameObject.name)
                {
                    case "Deagle":
                        guns[i].GiveMagazines(deagleMagazines);
                        break;

                    case "MP5":
                        guns[i].GiveMagazines(mp5Magazines);
                        break;

                    case "P90":
                        guns[i].GiveMagazines(p90Magazines);
                        break;

                    case "M4":
                        guns[i].GiveMagazines(m4Magazines);
                        break;

                    default:
                        Debug.LogWarning($"No such weapon name found. Searched for: {guns[i].gameObject.name}");
                        break;
                }
            }
        }
    }

    public void UseGrenade()
    {
        if (currentGrenades > 0)
        {
            Debug.Log("BOOM!");
            currentGrenades--;
        }
    }
}
