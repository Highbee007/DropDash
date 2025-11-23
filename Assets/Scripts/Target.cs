using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    public ParticleSystem targetParticle;
    private GameManager gameManager;

    public AudioClip tapSound;
    public AudioClip bombSound;

    private float force = 2f;
    private float growth = 0.7f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        rb.drag = 0.7f;

    }

    private void OnMouseDown()
    {
        if (!gameManager.isGameActive)
            return;

        Destroy(gameObject);
        gameManager.AddScore(5);
        Explode();
        //gameManager.sfxAudio.PlayOneShot(tapSound);
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

    }

    private void FixedUpdate()
    {
        // Adjusting fall speed
        if (gameManager.isGameActive)
        {
            force += growth * Time.deltaTime;
            rb.AddForce(Vector3.down * force, ForceMode.Acceleration);
        }
    }

    void Explode()
    {
        Instantiate(targetParticle, transform.position, targetParticle.transform.rotation).Play();
    }
}
