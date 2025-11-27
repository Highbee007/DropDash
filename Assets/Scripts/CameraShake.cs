using UnityEngine;
using System.Collections.Generic;
using System.Collections;
//using com.cyborgAssets.inspectorButtonPro;


public class CameraShake : MonoBehaviour
{
    [Header("Camera to Shake")]
    public Transform cameraTransform;
    [SerializeField] public Camera mainCam;

    [Header("Shake Settings")]
    public float defaultDuration = 0.2f;
    public float defaultMagnitude = 0.05f;

    private Vector3 _originalPos;
    private Coroutine _shakeRoutine;

    void Awake()
    {
        if (cameraTransform == null)
            cameraTransform = mainCam.transform;

        _originalPos = cameraTransform.localPosition;
    }

    //[ProButton]
    public void Shake()
    {
        Shake(defaultDuration, defaultMagnitude);
    }

    public void Shake(float duration, float magnitude)
    {
        // If shaking already, stop and reset before starting new shake
        if (_shakeRoutine != null)
        {
            StopCoroutine(_shakeRoutine);
            cameraTransform.localPosition = _originalPos;
        }

        _shakeRoutine = StartCoroutine(DoShake(duration, magnitude));
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
        _shakeRoutine = null;
    }
}