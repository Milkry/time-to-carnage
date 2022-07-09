using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Space]
    [Header("SPAWNER SETTINGS")]
    [Space]
    [SerializeField] private float startDelay;
    [SerializeField] private float spawnRate;
    [Tooltip("In how many seconds should the difficulty go up everytime")]
    [SerializeField] private float difficultyRate;
    [Space]
    [SerializeField] public List<Transform> spawnpoints;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<GameObject> newEnemies;
    [Space]
    [Header("OTHER")]
    [Space]
    [SerializeField] private GameObject difficultyWarning;
    [Range(0.5f, 3.0f)]
    [SerializeField] private float flashDuration;
    [SerializeField] private int flashTimes;

    public static bool spawnAllowed;
    private int randomSpawnPoint, randomEnemy;
    private float timeUntilDifficultyIncrease;
    private float nextSpawn;
    private bool lockDifficultyIncreaser = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnAllowed = true;
        timeUntilDifficultyIncrease = difficultyRate;
        nextSpawn = Time.time + startDelay;
    }

    private void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnEnemy();
        }

        //Difficulty Increaser
        if (Time.timeSinceLevelLoad >= timeUntilDifficultyIncrease && !lockDifficultyIncreaser)
        {
            if (spawnRate >= 2f)
            {
                spawnRate--;
                difficultyRate *= 2;
                timeUntilDifficultyIncrease += difficultyRate;
                AddNewEnemy();
                StartCoroutine(Warning());
                //Debug.Log($"Difficulty Increased! Now spawning enemies every: {spawnRate} seconds...\nNext difficulty increase will be in {difficultyRate} seconds.");
            }
            else
            {
                lockDifficultyIncreaser = true;
                //max difficulty reached popup text on screen
                //Debug.LogWarning("Max difficulty reached!");
            }
        }
    }

    private IEnumerator Warning()
    {
        for (int i = 0; i < flashTimes; i++)
        {
            FindObjectOfType<AudioManager>().Play("Warning");
            difficultyWarning.SetActive(true);
            yield return new WaitForSeconds(flashDuration);
            difficultyWarning.SetActive(false);
            yield return new WaitForSeconds(flashDuration);
        }
    }

    private void SpawnEnemy()
    {
        if (spawnAllowed)
        {
            randomSpawnPoint = Random.Range(0, spawnpoints.Count);
            randomEnemy = Random.Range(0, enemies.Count);
            Instantiate(enemies[randomEnemy], spawnpoints[randomSpawnPoint].position, Quaternion.identity);
        }
    }

    //Moves the 2 first gameobjects from the newEnemies list into the enemies list
    private void AddNewEnemy()
    {
        if (newEnemies.Count != 0)
        {
            GameObject[] enemy = { newEnemies[0], newEnemies[1] };

            enemies.AddRange(enemy);
            newEnemies.RemoveRange(0, 2);
        }
    }
}
