using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Space]
    [Header("TARGET")]
    [Space]
    public GameObject target = null;

    [Space]
    [Header("ENEMY TYPE")]
    [Space]
    [SerializeField] private int enemyType;
    [SerializeField] private bool NoAI;

    [Space]
    [Header("GENERAL SETTINGS")]
    [Space]
    [SerializeField] public int damage;
    [SerializeField] private float attackRate;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private int killReward;

    [Space]
    [Header("ENEMY PISTOL SETTINGS")]
    [Space]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float distanceToShoot;

    [Space]
    [Header("OTHER")]
    [Space]
    [SerializeField] private Transform firePoint = null;
    [SerializeField] private Transform muzzleFlashPoint = null;
    [SerializeField] private Transform muzzleFlashLightPoint = null;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private GameObject muzzleFlash = null;
    [SerializeField] private GameObject muzzleFlashLight = null;
    [SerializeField] private ParticleSystem gunParticles = null;
    [SerializeField] private Canvas healthbar;

    private Vector2 Direction;
    private float Angle;
    private float Distance;
    private Rigidbody2D rb;
    private PlayerController player;
    private float nextAttack;
    private float nextPistolAttack;
    private float currentHealth;
    private Canvas hpbar;
    private Slider hpslider;
    private ParticleSystem bloodParticles;

    // Start is called before the first frame update
    void Start()
    {
        //Other
        target = GameObject.FindGameObjectWithTag("Player");
        player = target.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        bloodParticles = FindObjectOfType<ItemHandler>().GetComponent<ItemHandler>().bloodParticles;

        //Cooldowns
        nextPistolAttack = Time.time + 1f;
        nextAttack = Time.time + 1f;

        //HP
        currentHealth = maxHealth;
        hpbar = Instantiate(healthbar, transform.position, Quaternion.identity);
        hpslider = hpbar.GetComponentInChildren<Slider>();
        hpslider.maxValue = maxHealth;
        hpslider.value = maxHealth;
        hpslider.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.transform.position = transform.position;

        if (currentHealth <= 0)
        {
            Die();
        }

        if (target != null && !NoAI)
        {
            Direction = (target.transform.position - transform.position).normalized;
            Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            Distance = Vector2.Distance(target.transform.position, transform.position);

            switch (enemyType)
            {
                case 1:
                    //nothing
                    break;

                case 2:
                    if (Distance < distanceToShoot && Time.time > nextPistolAttack)
                    {
                        nextPistolAttack = Time.time + attackRate;
                        AttackPistol();
                    }
                    break;

                case 3:
                    AttackSniper();
                    break;

                case 4:
                    AttackGrenade();
                    break;

                default:
                    Debug.LogError("No such enemy exists.");
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (target != null && !NoAI)
        {
            switch (enemyType)
            {
                case 1:
                    moveEnemyKnife();
                    break;

                case 2:
                    moveEnemyPistol();
                    break;

                case 3:
                    moveEnemySniper();
                    break;

                case 4:
                    moveEnemyGrenade();
                    break;

                default:
                    Debug.LogError("No such enemy exists.");
                    break;
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Destroy(hpbar.gameObject);
        Balance.Deposit(killReward);
        Balance.CalculateGems(1, 5, 5); //5% chance to give the player 1-5 gems upon enemy death
        string[] sounds = { "Enemy_death_grunt_1", "Enemy_death_grunt_2", "Enemy_death_grunt_3" };
        FindObjectOfType<AudioManager>().PlayRandomOnTop(sounds);
    }

    private void moveEnemyKnife()
    {
        rb.MovePosition((Vector2)transform.position + (Direction * moveSpeed * Time.deltaTime));
        rb.rotation = Angle;
    }

    private void moveEnemyPistol()
    {
        rb.rotation = Angle;
        if (Distance > distanceToShoot)
        {
            rb.MovePosition((Vector2)transform.position + (Direction * moveSpeed * Time.deltaTime));
        }
    }

    private void moveEnemySniper()
    {
        //code
    }

    private void moveEnemyGrenade()
    {
        //code
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hpslider.value = currentHealth;
    }

    //This method is for melee use
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Helper.FindChildWithTag(gameObject, "EnemyKnife") && collision.gameObject.CompareTag("Player") && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            AttackMelee(collision);
        }
    }

    private void AttackMelee(Collider2D collision)
    {
        //Play animation of melee attack
        FindObjectOfType<AudioManager>().PlayOnTop("Stab");
        ParticleSystem blood = Instantiate(bloodParticles, collision.transform.position, Quaternion.identity);
        blood.Play();
        player.TakeDamage(damage);
    }

    private void AttackPistol()
    {
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

    private void AttackSniper()
    {
        Debug.Log("Enemy Snipes!");
    }

    private void AttackGrenade()
    {
        Debug.Log("Enemy throws Grenade!");
    }
}
