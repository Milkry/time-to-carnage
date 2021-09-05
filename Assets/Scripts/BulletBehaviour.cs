using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private GameObject bulletImpactEffect;
    private ParticleSystem bloodParticles;

    private void Start()
    {
        bulletImpactEffect = FindObjectOfType<ItemHandler>().GetComponent<ItemHandler>().bulletImpactEffect;
        bloodParticles = FindObjectOfType<ItemHandler>().GetComponent<ItemHandler>().bloodParticles;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().PlayOnTop("BulletImpact_Enemy");
            ParticleSystem blood = Instantiate(bloodParticles, collision.transform.position, Quaternion.identity);
            blood.Play();
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(FindObjectOfType<Weapon>().GetComponent<Weapon>().damage);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            FindObjectOfType<AudioManager>().PlayOnTop("BulletImpact_Wall");
            GameObject impactEffect = Instantiate(bulletImpactEffect, transform.position, Quaternion.identity);
            Destroy(impactEffect, 0.35f);
        }
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            FindObjectOfType<AudioManager>().PlayOnTop("BulletSpark");
            GameObject impactEffect = Instantiate(bulletImpactEffect, transform.position, Quaternion.identity);
            Destroy(impactEffect, 0.35f);
        }

        Destroy(gameObject);
    }
}
