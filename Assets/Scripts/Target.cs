using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    public ParticleSystem targetParticle;
    private GameManager gameManager; 
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        Debug.Log("There was a click");
        Destroy(gameObject);
        gameManager.score += 5;
        scoreText.text = ":" + gameManager.score;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
