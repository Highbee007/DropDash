using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Rigidbody rb;
    public ParticleSystem targetParticle;
    private GameManager gameManager;
    private CameraShake shake;

    public AudioClip tapSound;
    public AudioClip bombSound;

    public float force = 2f;
    public float growth = 0.7f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        shake = GetComponent<CameraShake>();

        rb.drag = 0.7f;

    }

    private void OnMouseDown()
    {
        if (!gameManager.isGameActive)
            return;
        Destroy(gameObject);
        gameManager.AddScore(5);
        Explode();
        //shake.Shake();

        if (gameObject.CompareTag("Bad"))
        {
            gameManager.ReduceLives(1);
            gameManager.AddScore(-15);
        }
        //gameManager.sfxAudio.PlayOneShot(tapSound);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Bad"))
        {
            Destroy(gameObject);
            gameManager.ReduceLives(1);
        } else
        {
            Destroy(gameObject);
        }

    }

    private void LateUpdate()
    {
        // Adjusting fall speed
        if (gameManager.isGameActive)
        {
            force += growth * Time.deltaTime;
            rb.AddForce(Vector3.down * force, ForceMode.Acceleration);
        }

        if (!gameManager.isGameActive)
        {
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        Instantiate(targetParticle, transform.position, targetParticle.transform.rotation).Play();
    }
}
