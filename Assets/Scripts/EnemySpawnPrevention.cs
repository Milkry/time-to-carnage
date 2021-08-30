using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPrevention : MonoBehaviour
{
    private EnemySpawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        spawner = spawner.GetComponent<EnemySpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Stop spawning
            spawner.spawnpoints.Remove(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Continue spawning
            spawner.spawnpoints.Add(transform);
        }
    }
}
