using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic_RotatingQuadPlatform : MonoBehaviour {
    public Transform RotatingQuadPlatform;

    [Header("Settings")]
    public float WaitingTimeInSeconds = 4f;
    public float RotationAmountInDegrees = 45f;
    public float SpinDurationInSeconds = 4f;
    public AnimationCurve RotationCurve;

    void Start() {
        StartCoroutine("RotateQuadPlatform");
    }

    IEnumerator RotateQuadPlatform() {
        yield return new WaitForSeconds(WaitingTimeInSeconds);

        Quaternion startRotation = RotatingQuadPlatform.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, RotationAmountInDegrees, 0f);
        float elapsedTime = 0f;

        while (elapsedTime < SpinDurationInSeconds) {
            float fraction = RotationCurve.Evaluate((elapsedTime / SpinDurationInSeconds));
            Quaternion newRotation = Quaternion.Lerp(startRotation, endRotation, fraction);
            RotatingQuadPlatform.rotation = newRotation;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        RotatingQuadPlatform.rotation = endRotation;

        yield return StartCoroutine("RotateQuadPlatform");
    }
}
