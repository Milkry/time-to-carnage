using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public HealthBar healthbar;
    [HideInInspector] public int currentHealth;

    [SerializeField] private float speed;
    [SerializeField] private Joystick joystick;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        //Joystick positioning
        Vector2 joystickInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        moveVelocity = joystickInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Hurt");
        }
    }

    public void AddHealth(int health)
    {
        if (currentHealth + health <= maxHealth)
        {
            currentHealth += health;
            healthbar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth = maxHealth;
            healthbar.SetHealth(maxHealth);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        EnemySpawner.spawnAllowed = false;
        PowerupSpawner.spawnAllowed = false;
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        FindObjectOfType<EnemyController>().target = null;
        FindObjectOfType<Gameover>().GetComponent<Gameover>().GameOver();
    }
}
