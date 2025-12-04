using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerX : MonoBehaviour
{
    [Header("Mode")]
    public PrefabMode selectedPrefabMode;
    public GameMode selectedGameplayMode;
    public float timedDuration = 60f;
    private float _timeRemaining;

    public enum PrefabMode { Food, Ball, Crate }
    public enum GameMode { Endless, Chaos, Timed }


    [Header("Refs")]
    public GameObject gameOverObject;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI yourScoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI muteText;
    public GameObject timerUI;
    public GameObject playAgainButton;
    public GameObject returnToMenuButton; // add a new button for going back
    public GameObject gameMode;
    public GameObject prefabMode;
    public SpawnManagerX spawn;


    [Header("Logic")]
    public int maxLives = 5;
    public float startDelay = 0.8f;

    [Header("Audio")]
    public AudioClip buttonTap;
    public AudioClip loseSound;
    public AudioSource playAudio;
    public AudioSource musicAudio;
    private bool isMuted = false;

    [Header("Prefabs")]
    public List<GameObject> foodPrefabs;
    public List<GameObject> ballPrefabs;
    public List<GameObject> cratePrefabs;

    [Header("Special Prefabs")]
    public bool enableHealthItems;       // only in Endless
    public bool enableTimerItems;        // only in Timed
    public bool enableGoldItems;         // any mode
    public GameObject healthItemPrefab;
    public GameObject timerItemPrefab;
    public GameObject goldItemPrefab;

    [Header("Game State")]
    public bool isGameActive;
    public int score;
    private int _bestScore;
    public int lives;



    private void Start()
    {
        _bestScore = PlayerPrefs.GetInt("HighestScore");
        lives = maxLives;

        livesText.text = "Lives: " + lives;
        bestScoreText.text = "Highest Score: " + _bestScore;
        scoreText.text = "Score: " + score;
        yourScoreText.text = "Your Score: " + score;
        muteText.text = "Mute";

        timerUI.SetActive(false);
        returnToMenuButton.SetActive(false);
    }


    // ------------------------------------------------------
    // FALLING PREFAB SELECTION
    // ------------------------------------------------------
    public void SelectFood()
    {
        PlayButtonAudio();
        selectedPrefabMode = PrefabMode.Food;
        BeginGame();
    }

    public void SelectBall()
    {
        PlayButtonAudio();
        selectedPrefabMode = PrefabMode.Ball;
        BeginGame();
    }

    public void SelectCrate()
    {
        PlayButtonAudio();
        selectedPrefabMode = PrefabMode.Crate;
        BeginGame();
    }

    // Optional combined version
    public void SelectFallingItem(PrefabMode chosenMode)
    {
        PlayButtonAudio();
        selectedPrefabMode = chosenMode;
        prefabMode.SetActive(true);
    }


    // ------------------------------------------------------
    // GAMEPLAY MODES
    // ------------------------------------------------------
    public void StartEndlessMode()
    {
        PlayButtonAudio();
        selectedGameplayMode = GameMode.Endless;
        gameMode.SetActive(false);
        SelectFallingItem(selectedPrefabMode);
    }

    public void StartChaosMode()
    {
        PlayButtonAudio();
        selectedGameplayMode = GameMode.Chaos;
        BeginGame();
    }

    public void StartTimedMode()
    {
        PlayButtonAudio();
        selectedGameplayMode = GameMode.Timed;
        _timeRemaining = timedDuration;
        timerUI.SetActive(true);
        BeginGame();
    }


    // ------------------------------------------------------
    // BEGIN GAME woop woop
    // ------------------------------------------------------
    private void BeginGame()
    {
        StartGame();

        List<GameObject> listToUse = new List<GameObject>();

        // Base prefabs (normal mode or chaos override)
        if (selectedGameplayMode == GameMode.Chaos)
        {
            listToUse.AddRange(foodPrefabs);
            listToUse.AddRange(ballPrefabs);
            listToUse.AddRange(cratePrefabs);
        }
        else
        {
            switch (selectedPrefabMode)
            {
                case PrefabMode.Food: listToUse = foodPrefabs; break;
                case PrefabMode.Ball: listToUse = ballPrefabs; break;
                case PrefabMode.Crate: listToUse = cratePrefabs; break;
            }
        }

        CheckForSpecials(listToUse);
        spawn.StartMode(listToUse);
    }

    public void CheckForSpecials(List<GameObject> listToUse)
    {
        if (enableHealthItems && selectedGameplayMode == GameMode.Endless)
            listToUse.Add(healthItemPrefab);

        if (enableTimerItems && selectedGameplayMode == GameMode.Timed)
            listToUse.Add(timerItemPrefab);

        if (enableGoldItems)
            listToUse.Add(goldItemPrefab);
    }


    // ------------------------------------------------------
    // TIMED MODE TICK
    // ------------------------------------------------------
    private void Update()
    {
        if (!isGameActive)
            return;

        if (lives < 1)
        {
            GameOver();
            return;
        }

        if (selectedGameplayMode == GameMode.Timed)
        {
            _timeRemaining -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(_timeRemaining).ToString();

            if (_timeRemaining <= 0)
            {
                timerText.text = "0";
                timerUI.SetActive(false);
                GameOver();
            }
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        playAudio.Play();
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
    }

    public void PlayAgain() => Reset(); // call by button

    public void ReturnToMenu() // call by button
    {
        Reset();
        prefabMode.SetActive(false);
        gameMode.SetActive(true);
        playAudio.Stop();
        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);

        //TODO: add logic to go back to start screen
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        musicAudio.mute = isMuted;

        if (isMuted)
            muteText.text = "Unmute";
        else if (!isMuted)
            muteText.text = "Mute";
        //TODO: add button for this
    }


    public void Reset()
    {
        lives = maxLives;
        livesText.text = "Lives: " + lives;

        score = 0;
        scoreText.text = "Score: " + score;

        spawn.title.SetActive(true);
        gameOverObject.SetActive(false);
        playAgainButton.SetActive(false);
        returnToMenuButton.SetActive(false);
        timerUI.SetActive(false);


        isGameActive = false;
        spawn.startDelay = startDelay;
        spawn.ResetIntervals();
    }

    public void AddScore(int point)
    {
        score += point;

        if (score > _bestScore)
        {
            _bestScore = score;
            PlayerPrefs.SetInt("HighestScore", _bestScore);
            bestScoreText.text = "Highest Score: " + _bestScore;
            yourScoreText.text = "Your Score: " + score + " - NEW RECORD"; // I hope this is the right one
        }
        else
            yourScoreText.text = "Your Score: " + score;

        scoreText.text = "Score: " + score;
    }

    public void ReduceLives(int life)
    {
        lives -= Mathf.Max(life, 0);
        livesText.text = "Lives: " + lives;
    }

    public void AddLives(int life)
    {
        if (lives >= maxLives) return;

        lives += life;
        if (lives > maxLives)
            lives = maxLives;

        livesText.text = "Lives: " + lives;
    }

    public void AddTime(int value)
    {
        _timeRemaining += value;
        _timeRemaining = Mathf.Min(_timeRemaining, timedDuration);
    }

    public void GameOver()
    {
        isGameActive = false;
        playAudio.Stop();
        gameOverObject.SetActive(true);
        playAgainButton.SetActive(true);
        returnToMenuButton.SetActive(true);
        timerUI.SetActive(false);
    }


    // ------------------------------------------------------
    // BUTTON AUDIO
    // ------------------------------------------------------
    public void PlayButtonAudio() => playAudio.PlayOneShot(buttonTap, 1.0f);
}
