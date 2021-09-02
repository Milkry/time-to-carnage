using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] private float range;
    [SerializeField] private float fireRate;
    [SerializeField] private float spread;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int maxMagazines;
    [SerializeField] private float reloadTime;
    [Space]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform muzzleFlashPoint;
    [SerializeField] private Transform muzzleFlashLightPoint;
    [Space]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject muzzleFlashLight;
    [SerializeField] private ParticleSystem gunParticles;
    [Space]
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI magazineText;

    public static bool isReloading = false;
    private float nextFire = 0f;
    private int currentAmmo;
    private int currentMagazines;
    private bool outofMagazines = false;
    private bool isShootButtonPressed = false;

    private void Start()
    {
        currentAmmo = maxAmmo;
        currentMagazines = maxMagazines;
    }

    private void Update()
    {
        if (!isReloading)
        {
            ammoText.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();

            switch (gameObject.name)
            {
                case "Pistol":
                    magazineText.text = "\u221E";
                    currentMagazines = 999;
                    break;

                case "Deagle":
                    magazineText.text = currentMagazines.ToString();
                    break;

                case "MP5":
                    magazineText.text = currentMagazines.ToString();
                    break;

                case "P90":
                    magazineText.text = currentMagazines.ToString();
                    break;

                case "M4":
                    magazineText.text = currentMagazines.ToString();
                    break;

                case "G3":
                    magazineText.text = currentMagazines.ToString();
                    break;

                case "Shotgun":
                    magazineText.text = currentMagazines.ToString();
                    break;

                case "M40":
                    magazineText.text = currentMagazines.ToString();
                    break;

                default:
                    magazineText.text = "N/A";
                    break;
            }
        }

        if (currentMagazines <= 0)
        {
            outofMagazines = true;
        }

        if (isShootButtonPressed)
        {
            if (currentAmmo > 0)
            {
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    currentAmmo--;

                    switch (gameObject.name)
                    {
                        case "Pistol":
                            Pistol();
                            break;

                        case "Deagle":
                            Deagle();
                            break;

                        case "MP5":
                            MP5();
                            break;

                        case "P90":
                            P90();
                            break;

                        case "M4":
                            M4();
                            break;

                        case "G3":
                            G3();
                            break;

                        case "Shotgun":
                            Shotgun();
                            break;

                        case "M40":
                            M40();
                            break;

                        default:
                            Debug.LogWarning($"No such gun name found. Searched for: {gameObject.name}");
                            break;
                    }
                }
            }
            else
            {
                //Out of ammo sound effect
                if (!isReloading && !outofMagazines)
                {
                    StartCoroutine(Reload());
                }
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        ammoText.text = "Reloading...";
        switch (gameObject.name)
        {
            case "Pistol":
                FindObjectOfType<AudioManager>().Play("PistolReload");
                break;

            case "Deagle":
                FindObjectOfType<AudioManager>().Play("PistolReload");
                break;

            case "MP5":
                FindObjectOfType<AudioManager>().Play("RifleReload");
                break;

            case "P90":
                FindObjectOfType<AudioManager>().Play("RifleReload");
                break;

            case "M4":
                FindObjectOfType<AudioManager>().Play("RifleReload");
                break;

            case "G3":
                FindObjectOfType<AudioManager>().Play("RifleReload");
                break;

            case "Shotgun":
                FindObjectOfType<AudioManager>().Play("RifleReload");
                break;

            case "M40":
                FindObjectOfType<AudioManager>().Play("RifleReload");
                break;

            default:
                Debug.LogWarning($"No such gun name found. Searched for: {gameObject.name}");
                break;
        }
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        currentMagazines--;
        isReloading = false;
    }

    public void GiveMagazines(int magazines)
    {
        currentMagazines += magazines;
        outofMagazines = false;
    }

    public void UpdateShoot(bool buttonValue)
    {
        isShootButtonPressed = buttonValue;
    }

    public void Pistol()
    {
        //float rotation = Random.Range(-spread, spread);
        FindObjectOfType<AudioManager>().PlayOnTop("Pistol");
        GameObject muzzFlash = Instantiate(muzzleFlash, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
        GameObject muzzFlashLight = Instantiate(muzzleFlashLight, muzzleFlashLightPoint.position, muzzleFlashLightPoint.rotation);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        gunParticles.Play();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(muzzFlash, 0.1f);
        Destroy(muzzFlashLight, 0.1f);
    }

    public void Deagle()
    {
        FindObjectOfType<AudioManager>().PlayOnTop("Deagle");
        GameObject muzzFlash = Instantiate(muzzleFlash, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
        GameObject muzzFlashLight = Instantiate(muzzleFlashLight, muzzleFlashLightPoint.position, muzzleFlashLightPoint.rotation);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        gunParticles.Play();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(muzzFlash, 0.1f);
        Destroy(muzzFlashLight, 0.1f);
    }

    public void MP5()
    {
        FindObjectOfType<AudioManager>().PlayOnTop("MP5");
        GameObject muzzFlash = Instantiate(muzzleFlash, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
        GameObject muzzFlashLight = Instantiate(muzzleFlashLight, muzzleFlashLightPoint.position, muzzleFlashLightPoint.rotation);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        gunParticles.Play();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(muzzFlash, 0.1f);
        Destroy(muzzFlashLight, 0.1f);
    }

    public void P90()
    {
        FindObjectOfType<AudioManager>().PlayOnTop("P90");
        GameObject muzzFlash = Instantiate(muzzleFlash, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
        GameObject muzzFlashLight = Instantiate(muzzleFlashLight, muzzleFlashLightPoint.position, muzzleFlashLightPoint.rotation);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        gunParticles.Play();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(muzzFlash, 0.1f);
        Destroy(muzzFlashLight, 0.1f);
    }

    public void M4()
    {
        FindObjectOfType<AudioManager>().PlayOnTop("M4");
        GameObject muzzFlash = Instantiate(muzzleFlash, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
        GameObject muzzFlashLight = Instantiate(muzzleFlashLight, muzzleFlashLightPoint.position, muzzleFlashLightPoint.rotation);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        gunParticles.Play();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(muzzFlash, 0.1f);
        Destroy(muzzFlashLight, 0.1f);
    }

    public void G3()
    {
        FindObjectOfType<AudioManager>().PlayOnTop("G3");
        GameObject muzzFlash = Instantiate(muzzleFlash, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
        GameObject muzzFlashLight = Instantiate(muzzleFlashLight, muzzleFlashLightPoint.position, muzzleFlashLightPoint.rotation);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        gunParticles.Play();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(muzzFlash, 0.1f);
        Destroy(muzzFlashLight, 0.1f);
    }

    public void Shotgun()
    {
        Debug.LogWarning("Not Implemented yet...");
        //During shotgun cooldown play the "ShotgunPumpIt" sound effect
    }

    public void M40()
    {
        FindObjectOfType<AudioManager>().PlayOnTop("M40");
        GameObject muzzFlash = Instantiate(muzzleFlash, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
        GameObject muzzFlashLight = Instantiate(muzzleFlashLight, muzzleFlashLightPoint.position, muzzleFlashLightPoint.rotation);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        gunParticles.Play();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(muzzFlash, 0.1f);
        Destroy(muzzFlashLight, 0.1f);
    }
}
