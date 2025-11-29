using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManagerX : MonoBehaviour
{
    [Header("Settings")]
    public float startDelay = 0.8f;
    public float baseInterval = 2.0f;
    public float minSpawnInterval = 0.5f;
    public float difficultyRate = 0.95f;
    private float _currentInterval;


    [Header("Spawn")]
    public float spawnxMin = -5;
    public float spawnxMax = -1f;
    public float spawnY = 13f;


    [Header("Refs")]
    public GameObject title;
    public GameManagerX gameManager;


    private List<GameObject> _currentPrefabs;
    private bool _chaosMode = false;
    private GameManagerX.GameMode _currentMode;

    private float _originalMax;
    private float _originalMin;
    private float _originalDifficulty;

    private void Start()
    {
        _currentInterval = baseInterval;
    }

    public void ResetIntervals()
    {
        _currentInterval = baseInterval;
    }

    // ------------------------------------------------------
    // ENDLESS + TIMED
    // ------------------------------------------------------

    public void StartMode(List<GameObject> prefabs)
    {
        _chaosMode = false;
        _currentPrefabs = prefabs;
        BeginSpawning();
    }

    // ------------------------------------------------------
    // CHAOS
    // ------------------------------------------------------
    public void StartChaosMode(List<GameObject> mixedList)
    {
        _chaosMode = true;
        _currentPrefabs = mixedList;
        BeginSpawning();
    }

    private void BeginSpawning()
    {
        if (gameManager.isGameActive)
        {
            gameManager.StartGame();
            title.SetActive(false);
            StartCoroutine(SpawnLoop());
        }
    }

    // ------------------------------------------------------
    // SPAWN LOGIC
    // ------------------------------------------------------
    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(startDelay);

        while (gameManager.isGameActive)
        {
            SpawnRandom();
            yield return new WaitForSeconds(_currentInterval);

            if (gameManager.selectedGameplayMode == GameManagerX.GameMode.Endless)
                _currentInterval = Mathf.Max(minSpawnInterval, _currentInterval * difficultyRate);
        }
    }

    private void SpawnRandom()
    {

        if (_currentPrefabs == null || _currentPrefabs.Count == 0) return;
        int index = Random.Range(0, _currentPrefabs.Count);

        Vector3 spawnPos = new Vector3(Random.Range(spawnxMin, spawnxMax), spawnY, 0);
        Quaternion spawnRot = Quaternion.Euler(0, 0, Random.Range(0f, 360f)
     );

        Instantiate(_currentPrefabs[index], spawnPos, spawnRot);
    }
}