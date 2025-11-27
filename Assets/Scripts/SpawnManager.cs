using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Refs")]
    public GameObject title;
    private GameManager gameManager;

    [Header("Spawns")]
    public float startDelay = 0.8f;
    public float spawnInterval = 2.0f;
    public float minInterval = 0.5f;
    public float difficultyRate = 0.95f;
    public float minSpawnX = -5f;
    public float maxSpawnX = -1f;
    public float spawnY = 13f;

    private List<GameObject> currentPrefabs;

    public List<GameObject> specialPrefabs;

    private void Awake()
    {
        title = GameObject.Find("Title");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    public void StartMode(List<GameObject> prefabs)
    {
        if (!gameManager.isGameActive)
        {
            currentPrefabs = prefabs;
            gameManager.StartGame();
            title.SetActive(false);
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(startDelay);

        while (gameManager.isGameActive)
        {
            SpawnRandom();
            yield return new WaitForSeconds(spawnInterval);

            spawnInterval = Mathf.Max(minInterval, spawnInterval * difficultyRate);
        }
    }

    private void SpawnRandom()
    {
        int index = Random.Range(0, currentPrefabs.Count);

        Vector3 spawnPos = new Vector3(
            Random.Range(minSpawnX, maxSpawnX),
            spawnY,
            0
        );

        Quaternion spawnRot = Random.rotation;

        Instantiate(currentPrefabs[index], spawnPos, spawnRot);
    }

    //void SpawnRandomSpecial()
    //{
    //    int index = Random.Range(0, specialPrefabs.Count);
    //}
}
