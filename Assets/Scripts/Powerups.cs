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
    [SerializeField] private int heal;

    [Space]
    [Header("AMMO POWERUP")]
    [Space]
    [SerializeField] private int rifleMagazines;
    //[SerializeField] private int deagleMagazines;

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
                    FindObjectOfType<PlayerController>().GetComponent<PlayerController>().AddHealth(heal);
                    FindObjectOfType<AudioManager>().PlayOnTop("Powerup_Pickup");
                    break;

                case "AMMO":
                    GetAndRefillGuns();
                    FindObjectOfType<AudioManager>().PlayOnTop("Ammo_Pickup");
                    break;

                case "SPEED":
                    Debug.Log("Speed buff");
                    //give player speed
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
    private void GetAndRefillGuns()
    {
        var guns = FindObjectsOfType<Weapon>(true);
        for (int i = 0; i < guns.Length; i++)
        {
            switch (guns[i].gameObject.name)
            {
                case "Rifle":
                    guns[i].GiveMagazines(rifleMagazines);
                    break;

                default:
                    Debug.LogWarning($"No such weapon name found. Searched for: {guns[i].gameObject.name}");
                    break;
            }
        }
    }
}
