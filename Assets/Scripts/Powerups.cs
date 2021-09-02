using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [Space]
    [Header("GENERAL SETTINGS")]
    [Space]
    [SerializeField] private string type;
    [SerializeField] private float degreesPerSecond = 0f;
    [SerializeField] private float amplitude = 0.2f;
    [SerializeField] private float frequency = 1f;

    [Space]
    [Header("HEALTH POWERUP")]
    [Space]
    [SerializeField] private int healAmount;

    [Space]
    [Header("AMMO POWERUP")]
    [Space]
    [SerializeField] private int deagleMagazines;
    [SerializeField] private int mp5Magazines;
    [SerializeField] private int p90Magazines;
    [SerializeField] private int m4Magazines;
    [SerializeField] private int g3Magazines;
    [SerializeField] private int shotgunShells;
    [SerializeField] private int m40Magazines;

    [Space]
    [Header("MONEY POWERUP")]
    [Space]
    [SerializeField] private int moneyDrop;

    [Space]
    [Header("GRENADE POWERUP")]
    [Space]
    [SerializeField] private int grenadeDrop;

    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    private void Start()
    {
        posOffset = transform.position;
    }
    private void Update()
    {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (type.ToUpper())
            {
                case "HEALTH":
                    FindObjectOfType<AudioManager>().PlayOnTop("Powerup_Pickup");
                    FindObjectOfType<PlayerController>().GetComponent<PlayerController>().AddHealth(healAmount);
                    break;

                case "AMMO":
                    FindObjectOfType<AudioManager>().PlayOnTop("Ammo_Pickup");
                    GetAndRefillGuns();
                    break;

                case "MONEY":
                    FindObjectOfType<AudioManager>().PlayOnTop("Purchase");
                    Balance.Deposit(moneyDrop);
                    break;

                case "GRENADES":
                    FindObjectOfType<AudioManager>().PlayOnTop("Powerup_Pickup");
                    SideInventory.AddGrenades(grenadeDrop);
                    break;

                default:
                    Debug.LogWarning($"No such powerup exists. Used: {type.ToUpper()}");
                    break;
            }

            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Gets all inactive guns and gives them magazines
    /// </summary>
    public void GetAndRefillGuns()
    {
        var guns = FindObjectsOfType<Weapon>();
        for (int i = 0; i < guns.Length; i++)
        {
            switch (guns[i].gameObject.name)
            {
                case "Pistol":
                    //nothing
                    break;

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

                case "G3":
                    guns[i].GiveMagazines(g3Magazines);
                    break;

                case "Shotgun":
                    guns[i].GiveMagazines(shotgunShells);
                    break;

                case "M40":
                    guns[i].GiveMagazines(m40Magazines);
                    break;

                default:
                    Debug.LogWarning($"No such weapon name found. Searched for: {guns[i].gameObject.name}");
                    break;
            }
        }
    }
}
