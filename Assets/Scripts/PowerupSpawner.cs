using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [Space]
    [Header("SPAWNER SETTINGS")]
    [Space]
    [SerializeField] private float startDelay;
    [SerializeField] private float spawnRate;
    [SerializeField] private float offsetPosition;
    [Space]
    [SerializeField] public List<Transform> spawnpoints;
    [SerializeField] private List<GameObject> powerups;

    public static bool spawnAllowed;
    private int randomSpawnPoint, randomPowerup;
    private float offsetX, offsetY;
    private Vector3 randomPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnAllowed = true;
        InvokeRepeating("SpawnPowerup", startDelay, spawnRate);
    }

    private void SpawnPowerup()
    {
        if (spawnAllowed)
        {
            randomSpawnPoint = Random.Range(0, spawnpoints.Count);
            randomPowerup = Random.Range(0, powerups.Count);
            offsetX = Random.Range(0, offsetPosition);
            offsetY = Random.Range(0, offsetPosition);
            randomPosition = new Vector3(offsetX, offsetY, 0);
            Instantiate(powerups[randomPowerup], spawnpoints[randomSpawnPoint].position + randomPosition, Quaternion.identity);
        }
    }
}
