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
        InvokeRepeating("SpawnRandom", 0.5f, Random.Range(20f * Time.deltaTime, 50f * Time.deltaTime));
    }

    // Update is called once per frame
    void SpawnRandom()
    {
        int ballIndex = Random.Range(0, ballPrefabs.Count);
        int foodIndex = Random.Range(0, foodPrefabs.Count);
        int crateIndex = Random.Range(0, cratePrefabs.Count);

        Vector3 spawnPos = new Vector3(Random.Range(-2.3f, 2.3f), 13, 0);

        Instantiate(cratePrefabs[crateIndex], spawnPos, cratePrefabs[crateIndex].transform.localRotation);
    }
}
