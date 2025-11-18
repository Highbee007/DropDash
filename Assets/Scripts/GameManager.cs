using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool isGameActive;
    public int score;
    public TextMeshProUGUI scoreText;
    public AudioClip backgroundAudio;
   
    public void Reset()
    {
        
    }

    public void StartGame()
    {
        isGameActive = true;
    }

    public void Mode()
    {

    }

}
