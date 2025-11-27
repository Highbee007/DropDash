using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public GameObject gameOver;
    //public GameObject bestText;
    public GameObject gameOverObject;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI yourScoreText;
    public Button playAgainButton;
    private AudioSource playAudio;

    public AudioClip buttonTap;

    private SpawnManager spawn;

    public List<GameObject> foodPrefabs;
    public List<GameObject> ballPrefabs;
    public List<GameObject> cratePrefabs;

    public bool isGameActive;
    public int score;
    private int best;
    private int lives;


    private void Start()
    {
        playAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        spawn = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        best = PlayerPrefs.GetInt("HighestScore");

        lives = 5;

        livesText.text = "Lives: " + lives;
        bestScoreText.text = "Highest Score: " + best;
        scoreText.text = "Score: " + score;
        yourScoreText.text = "Your Score: " + score;


    }

    public void StartFoodMode()
    {
        playAudio.PlayOneShot(buttonTap, 1.0f);
        spawn.StartMode(foodPrefabs);
    }

    public void StartBallMode()
    {
        playAudio.PlayOneShot(buttonTap, 1.0f);
        spawn.StartMode(ballPrefabs);
    }

    public void StartCrateMode()
    {
        playAudio.PlayOneShot(buttonTap, 1.0f);
        spawn.StartMode(cratePrefabs);
    }

    public void Reset()
    {
        lives = 5;
        livesText.text = "Lives: " + lives;

        score = 0;
        scoreText.text = "Score: " + score;

        spawn.title.SetActive(true);
        //gameOver.SetActive(false);
        //bestText.SetActive(false);
        gameOverObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);

        isGameActive = false;

        spawn.spawnInterval = 2.0f;
        spawn.startDelay = 0.8f;
        spawn.minInterval = 0.5f;
        spawn.difficultyRate = 0.95f;

        //target.force = 2f;
        //target.force = 0.7f;
    }

    public void StartGame()
    {
        isGameActive = true;
        playAudio.Play();
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
    }

    public void AddScore(int point)
    {
        score += point;
        scoreText.text = "Score: " + score;
        yourScoreText.text = "Your Score: " + score;
        bestScoreText.text = "Highest Score: " + best;


        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("HighestScore", best);
        }
    }

    public void ReduceLives(int life)
    {
        lives -= life;
        livesText.text = "Lives: " + lives;

        if (lives == 0)
            return;
    }

    public void GameOver()
    {
        isGameActive = false;
        //gameOver.SetActive(true);
        //bestText.SetActive(true);
        gameOverObject.SetActive(true);
        playAgainButton.GameObject().SetActive(true);
    }

    private void Update()
    {
        if (lives < 1 && isGameActive)
        {
            GameOver();
        }
    }

}
