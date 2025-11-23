using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject title;
    private GameManager gameManager;

    private float startDelay = 0.8f;
    private float spawnInterval = 2.0f;
    private float minInterval = 0.5f; 
    private float difficultyRate = 0.95f;

    private List<GameObject> currentPrefabs;

    public List<GameObject> specialPrefabs;

    private void Awake()
    {
        title = GameObject.Find("Title");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    public void StartMode(List<GameObject> prefabs)
    {
        currentPrefabs = prefabs;
        gameManager.StartGame();
        title.SetActive(false);
        StartCoroutine(Spawn());
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
            Random.Range(-2.1f, 2.1f),
            13,
            0
        );

        Quaternion spawnRot = Random.rotation;

        Instantiate(currentPrefabs[index], spawnPos, spawnRot);
    }

    void SpawnRandomSpecial()
    {
        int index = Random.Range(0, specialPrefabs.Count);
    }
}
