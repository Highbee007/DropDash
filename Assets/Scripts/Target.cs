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
    private AudioSource sfxAudio;


    public AudioClip tapSound;
    public AudioClip bombSound;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        sfxAudio = GetComponent<AudioSource>();
        //targetParticle = GetComponent<ParticleSystem>();

        rb.drag = 1.6f;
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Instantiate(targetParticle, transform.position, targetParticle.transform.rotation);
            sfxAudio.PlayOneShot(tapSound, 1.0f);
            gameManager.AddScore(5);
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

    }
}
