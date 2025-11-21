using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private AudioSource playAudio;
    //public AudioClip gameAudio;
    private SpawnManager spawn;
    public GameObject cameraAudio;

    public List<GameObject> foodPrefabs;
    public List<GameObject> ballPrefabs;
    public List<GameObject> cratePrefabs;

    public bool isGameActive;
    public int score;



    private void Awake()
    {
        playAudio = GetComponent<AudioSource>();
        spawn = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        cameraAudio = GetComponent<GameObject>();
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
        //playAudio.PlayOneShot(gameAudio, 1f);
        score = 0;
        scoreText.text = ":" + score;
    }

    public void Mode()
    {

    }

}
