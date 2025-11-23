using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI livesText;
    private AudioSource playAudio;
    public AudioSource sfxAudio;
    public GameObject gameOver;

    private SpawnManager spawn;
    private Target target;

    public List<GameObject> foodPrefabs;
    public List<GameObject> ballPrefabs;
    public List<GameObject> cratePrefabs;

    public bool isGameActive;
    public int score;
    private int best;
    private int lives = 5;



    private void Awake()
    {
        playAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        sfxAudio = GetComponent<AudioSource>();
        spawn = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        gameOver = GetComponent<GameObject>();

        livesText.text = "Lives: " + lives;
        best = PlayerPrefs.GetInt("Highscore");

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
    }

    public void AddScore(int point)
    {
        score += point;
        scoreText.text = ":" + score;
        bestScoreText.text = "Highest Score: " + best;

        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("Highscore", best);
        }
    }

    public void ReduceLives(int life)
    {
        lives -= 1;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOver.SetActive(true);
        target.rb.useGravity = false;
    }

    private void Update()
    {
        if (lives < 1)
        {
            GameOver();
        }
    }

}
