using UnityEngine;

public class TargetX : MonoBehaviour
{
    [Header("Refs")]
    public Rigidbody rb;
    public ParticleSystem targetParticle;
    public GameManagerX gameManager;
    public CameraShake shake;

    [Header("Sfx")]
    public AudioClip tapSound;
    public AudioClip bombSound;

    [Header("Logic")]
    public int scoreIncrease = 5;
    public int scoreDecrease = -15;
    public float force = 2f;
    public float growth = 0.7f;

    [Header("Bonuses")]
    public int timeBonus = 5;
    public int goldBonus = 10;

    [Header("Weight")]
    [Tooltip("Higher value = this object appears more often (SpawnManager must use this).")]
    public int spawnWeight = 1;


    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManagerX>();
        shake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        if (shake == null)
            shake = Camera.main.GetComponent<CameraShake>(); // move shake to main cam


        rb.drag = 0.7f; //this is obsolete apparently
        //rb.linearDamping = growth;

    }

    private void OnMouseDown()
    {
        if (!gameManager.isGameActive)
            return;

        if (HandleGoldBonus()) { DestroyObject(); return; }
        if (HandleHealthPickup()) { DestroyObject(); return; }
        if (HandleTimerPickup()) { DestroyObject(); return; }

        if (gameObject.CompareTag("Bad"))
        {
            gameManager.ReduceLives(1);
            gameManager.AddScore(scoreDecrease);
            gameManager.playAudio.PlayOneShot(bombSound, 1.0f);
        }
        else
        {
            gameManager.AddScore(scoreIncrease);
            gameManager.playAudio.PlayOneShot(tapSound, 1f);
        }

        DestroyObject();

    }

    private void DestroyObject()
    {
        Explode();
        shake.Shake();
        Destroy(gameObject);
    }

    private bool HandleGoldBonus()
    {
        if (!gameObject.CompareTag("Gold"))
            return false;

        gameManager.AddScore(goldBonus);
        return true;
    }

    private bool HandleHealthPickup()
    {
        if (!gameObject.CompareTag("Health"))
            return false;

        if (gameManager.selectedGameplayMode == GameManagerX.GameMode.Endless)
            gameManager.AddLives(1);

        return true;
    }

    private bool HandleTimerPickup()
    {
        if (!gameObject.CompareTag("Timer"))
            return false;

        if (gameManager.selectedGameplayMode == GameManagerX.GameMode.Timed)
            gameManager.AddTime(timeBonus);

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.ReduceLives(1);
            gameManager.playAudio.PlayOneShot(gameManager.loseSound, 1f);
        }

        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        if (!gameManager.isGameActive)
        {
            Destroy(gameObject);
            return;
        }

        // Increase fall speed gradually
        force += growth * Time.deltaTime;
        rb.AddForce(Vector3.down * force, ForceMode.Acceleration);
    }

    void Explode()
    {
        ParticleSystem fx = Instantiate(targetParticle, transform.position, targetParticle.transform.rotation);
        fx.Play();
    }
}