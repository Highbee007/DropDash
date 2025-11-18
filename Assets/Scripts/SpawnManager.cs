using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> foodPrefabs;
    public List<GameObject> ballPrefabs;
    public List<GameObject> cratePrefabs;
    private GameManager gameManager;



    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void SpawnCrate()
    {
        gameManager.StartGame();
        InvokeRepeating("SpawnRandomCrate", 0.5f, Random.Range(20f * Time.deltaTime, 50f * Time.deltaTime));
    }

    void SpawnBall()
    {
        gameManager.StartGame();
        InvokeRepeating("SpawnRandomBall", 0.5f, Random.Range(20f * Time.deltaTime, 50f * Time.deltaTime));
    }

    void SpawnFood()
    {
        gameManager.StartGame();
        InvokeRepeating("SpawnRandomFood", 0.5f, Random.Range(20f * Time.deltaTime, 50f * Time.deltaTime));
    }

    // Update is called once per frame
    private void SpawnRandomCrate()
    {
        int crateIndex = Random.Range(0, cratePrefabs.Count);

        Vector3 spawnPos = new Vector3(Random.Range(-2.3f, 2.3f), 13, 0);
        Quaternion spawnRot = new Quaternion(Random.Range(0, 178), Random.Range(0, 178), Random.Range(0, 178), Random.Range(0, 178));

        Instantiate(cratePrefabs[crateIndex], spawnPos, spawnRot);
    }

    private void SpawnRandomBall()
    {
        int ballIndex = Random.Range(0, ballPrefabs.Count);

        Vector3 spawnPos = new Vector3(Random.Range(-2.3f, 2.3f), 13, 0);
        Quaternion spawnRot = new Quaternion(Random.Range(0, 178), Random.Range(0, 178), Random.Range(0, 178), Random.Range(0, 178));

        Instantiate(ballPrefabs[ballIndex], spawnPos, spawnRot);
    }

    private void SpawnRandomFood()
    {
        int foodIndex = Random.Range(0, foodPrefabs.Count);

        Vector3 spawnPos = new Vector3(Random.Range(-2.3f, 2.3f), 13, 0);
        Quaternion spawnRot = new Quaternion(Random.Range(0, 178), Random.Range(0, 178), Random.Range(0, 178), Random.Range(0, 178));

        Instantiate(foodPrefabs[foodIndex], spawnPos, spawnRot);
    }
}
