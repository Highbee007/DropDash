using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Camera to Shake")]
    public Transform cameraTransform;   // Drag your camera here

    [Header("Shake Settings")]
    public float defaultDuration = 0.2f;
    public float defaultMagnitude = 0.05f;

    private Vector3 _originalPos;
    private Coroutine shakeRoutine;

    void Awake()
    {
        cameraTransform = GetComponent<Transform>();
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        _originalPos = cameraTransform.localPosition;
    }

    public void Shake()
    {
        Shake(defaultDuration, defaultMagnitude);
    }

    public void Shake(float duration, float magnitude)
    {
        // If shaking already, stop and reset before starting new shake
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
            cameraTransform.localPosition = _originalPos;
        }

        shakeRoutine = StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        float timer = 0f;

        while (timer < duration)
        {
            cameraTransform.localPosition = _originalPos + Random.insideUnitSphere * magnitude;
            timer += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = _originalPos;
        shakeRoutine = null;

    }
}
