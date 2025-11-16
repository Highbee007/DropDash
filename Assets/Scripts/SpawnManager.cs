using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> foodPrefabs;
    public List<GameObject> ballPrefabs;
    public List<GameObject> cratePrefabs;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandom", 0.5f, Random.Range(0.3f, 2.0f));
    }

    // Update is called once per frame
    void SpawnRandom()
    {
        int ballIndex = Random.Range(0, ballPrefabs.Count);
        int foodIndex = Random.Range(0, foodPrefabs.Count);
        int crateIndex = Random.Range(0, cratePrefabs.Count);

        Vector3 spawnPos = new Vector3(Random.Range(-4, 4), 13, 0);

        Instantiate(ballPrefabs[ballIndex], spawnPos, ballPrefabs[ballIndex].transform.rotation);
    }
}
