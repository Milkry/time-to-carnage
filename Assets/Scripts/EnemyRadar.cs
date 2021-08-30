using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadar : MonoBehaviour
{
    List<Transform> nearbyEnemies = new List<Transform>();
    private Transform closestEnemy;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float angle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            nearbyEnemies.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            nearbyEnemies.Remove(other.transform);
        }
    }

    private void Update()
    {
        closestEnemy = GetClosestEnemy(nearbyEnemies, transform);

        //Look at the closest enemy
        if (nearbyEnemies.Count != 0)
        {
            direction = (closestEnemy.position - transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    Transform GetClosestEnemy(List<Transform> enemies, Transform source)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = source.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
}
