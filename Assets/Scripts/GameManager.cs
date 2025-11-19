using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool isGameActive;
    public int score;
   
    public void Reset()
    {
        
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
    }

    public void Mode()
    {

    }

}
