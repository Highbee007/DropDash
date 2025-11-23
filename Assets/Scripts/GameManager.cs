using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private AudioSource playAudio;
    public AudioSource sfxAudio;
    private SpawnManager spawn;

    public List<GameObject> foodPrefabs;
    public List<GameObject> ballPrefabs;
    public List<GameObject> cratePrefabs;

    public bool isGameActive;
    public int score;



    private void Awake()
    {
        playAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        sfxAudio = GetComponent<AudioSource>();
        spawn = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    public void StartFoodMode()
    {
        spawn.StartMode(foodPrefabs);
    }

    public void StartBallMode()
    {
        spawn.StartMode(ballPrefabs);
    }

    public void StartCrateMode()
    {
        spawn.StartMode(cratePrefabs);
    }

    public void Reset()
    {
        
    }

    public void StartGame()
    {
        isGameActive = true;
        playAudio.Play();
        score = 0;
    }

    public void AddScore(int point)
    {
        score += point;
        scoreText.text = ":" + score;
    }

    public void Mode()
    {

    }

}
