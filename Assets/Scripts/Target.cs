using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    public ParticleSystem targetParticle;
    public TextMeshProUGUI scoreText;

    public int score;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        Debug.Log("There was a click");
        Destroy(gameObject);
        score += 5;
        scoreText.text = ":" + score;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
