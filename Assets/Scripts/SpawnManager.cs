using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> foodPrefabs;
    public List<GameObject> ballPrefabs;
    public List<GameObject> cratePrefabs;

    public GameObject title;
    private GameManager gameManager;

    public bool isGameActive = false;
    public float startDelay = 0.8f;
    public float spawnInterval = 1.2f;   // starts slow
    public float minInterval = 0.3f;     // fastest speed
    public float difficultyRate = 0.95f; // speed multiplier every 10 seconds

    private List<GameObject> currentPrefabs;

    private void Awake()
    {
        title = GameObject.Find("Title");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void StartFoodMode()
    {
        StartMode(foodPrefabs);
    }

    public void StartBallMode()
    {
        StartMode(ballPrefabs);
    }

    public void StartCrateMode()
    {
        StartMode(cratePrefabs);
    }

    private void StartMode(List<GameObject> prefabs)
    {
        currentPrefabs = prefabs;
        gameManager.StartGame();
        isGameActive = true;
        title.SetActive(false);

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(startDelay);

        while (isGameActive)
        {
            SpawnRandom();
            yield return new WaitForSeconds(spawnInterval);

            // Difficulty scaling
            spawnInterval = Mathf.Max(minInterval, spawnInterval * difficultyRate);
        }
    }

    private void SpawnRandom()
    {
        int index = Random.Range(0, currentPrefabs.Count);

        Vector3 spawnPos = new Vector3(
            Random.Range(-2.3f, 2.3f),
            13,
            0
        );

        Quaternion spawnRot = Random.rotation;

        Instantiate(currentPrefabs[index], spawnPos, spawnRot);
    }
}
