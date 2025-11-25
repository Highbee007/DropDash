using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI livesText;
    public GameObject bestText;
    public Button playAgainButton;
    private AudioSource playAudio;

    private SpawnManager spawn;
    private Target target;

    public List<GameObject> foodPrefabs;
    public List<GameObject> ballPrefabs;
    public List<GameObject> cratePrefabs;

    public bool isGameActive;
    public int score;
    private int best;
    private int lives = 5;


    private void Start()
    {
        playAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        spawn = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        best = PlayerPrefs.GetInt("HighestScore");

        livesText.text = "Lives: " + lives;
        bestScoreText.text = "Highest Score: " + best;
        scoreText.text = ":" + score;


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
        lives = 5;
        livesText.text = "Lives: " + lives;

        score = 0;
        scoreText.text = ":" + score;

        spawn.title.SetActive(true);
        gameOver.SetActive(false);
        bestText.SetActive(false);
        playAgainButton.gameObject.SetActive(false);

        isGameActive = false;
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
        gameOver.SetActive(true);
        bestText.SetActive(true);
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
