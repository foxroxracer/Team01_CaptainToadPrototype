using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic_MovingPlatform : MonoBehaviour {
    [Header("Setup")]
    public Transform StartPoint;
    public Transform EndPoint;
    public Transform Platform;

    [Header("Settings")]
    public float TravelTimeinSeconds = 3f;
    public AnimationCurve MovementCurve;

    private enum PlatformState {
        Up,
        Down,
    }
    private PlatformState _currentPlatformState = PlatformState.Down;
    private bool _platformIsMoving = false;
    
    void Start() {
        MovePlatformToStartPoint();        
    }

    public void TogglePlatformMovement() {
        if (!_platformIsMoving) {
            if (_currentPlatformState == PlatformState.Up) {
                MovePlatformDown();
                _currentPlatformState = PlatformState.Down;
            } else {
                MovePlatformUp();
                _currentPlatformState = PlatformState.Up;
            }
        }
    }

    private void MovePlatformUp() {
        StartCoroutine(MovePlatform(PlatformState.Up));
    }

    private void MovePlatformDown() {
        StartCoroutine(MovePlatform(PlatformState.Down));
    }

    IEnumerator MovePlatform(PlatformState direction) {
        _platformIsMoving = true;
        float elapsedTime = 0f;

        Vector3 startPoint = this.StartPoint.position;
        Vector3 endPoint = this.EndPoint.position;

        if (direction == PlatformState.Down) {
            startPoint = this.EndPoint.position;
            endPoint = this.StartPoint.position;
        }

        while (elapsedTime < TravelTimeinSeconds) {
            float fractionFromCurve = MovementCurve.Evaluate((elapsedTime / TravelTimeinSeconds));
            Platform.transform.position = Vector3.Lerp(startPoint, endPoint, fractionFromCurve);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Platform.transform.position = endPoint;

        _platformIsMoving = false;
    }

    private void MovePlatformToStartPoint() {
        Platform.position = StartPoint.position;
    }
}
