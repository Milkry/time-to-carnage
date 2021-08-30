using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    private GameObject bulletImpactEffect;
    private PlayerController player;
    private EnemyController enemy;

    private void Start()
    {
        bulletImpactEffect = FindObjectOfType<ItemHandler>().GetComponent<ItemHandler>().bulletImpactEffect;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemy = GameObject.FindGameObjectWithTag("EnemyPistol").GetComponentInParent<EnemyController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            FindObjectOfType<AudioManager>().PlayOnTop("BulletImpact_Wall");
            Destroy(gameObject);
            GameObject impactEffect = Instantiate(bulletImpactEffect, transform.position, Quaternion.identity);
            Destroy(impactEffect, 0.35f);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().PlayOnTop("BulletImpact_Enemy");
            player.TakeDamage(enemy.damage);
            Destroy(gameObject);
            //play blood particles
            GameObject impactEffect = Instantiate(bulletImpactEffect, transform.position, Quaternion.identity);
            Destroy(impactEffect, 0.35f);
        }
    }
}
