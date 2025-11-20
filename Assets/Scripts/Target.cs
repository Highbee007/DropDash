using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    public ParticleSystem targetParticle;
    private GameManager gameManager;

    public int count;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rb.drag = 2;
    }

    private void OnMouseDown()
    {
            Destroy(gameObject);
            gameManager.score += 5;
            gameManager.scoreText.text = ":" + gameManager.score;
    }

    // Update is called once per frame
    void Update()
    {
        rb.drag -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Debug.Log(count++);

    }
}
