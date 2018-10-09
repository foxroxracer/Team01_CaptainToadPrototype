using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System.Collections;

public class CameraController : MonoBehaviour {
    [Header("Horizontal rotation")]
    [Range(0f, 500f)]
    public float HorizontalRotationSpeed = 100f;

    [Header("Vertical rotation")]
    [Range(0f, 500f)]
    public float VerticalRotationSpeed = 100f;

    [Range(0f, 180f)]
    public float MinimumVerticalRotationInDegrees = 5f;

    [Range(0f, 180f)]
    public float MaximumVerticalRotationInDegrees = 170f;

    [Header("Recenter camera")]
    [Range(0f, 1f)]
    public float RecenterCameraTimeInSeconds = 0.3f;

    [Header("Camera Modes")]
    public List<CameraMode> CameraModes = new List<CameraMode>();
    private int _currentCameraModeIndex = 0;

    private Transform _cameraPivotPoint;
    private Transform _cameraVerticalRotationPivotPoint;
    private Transform _mainCharacter;
    private bool _isLeftTriggerHeldDown = false;
    private bool _isPaused = false;

    void Awake() {
        _cameraPivotPoint = this.transform.Find("CameraPivot");
        _cameraVerticalRotationPivotPoint = _cameraPivotPoint.transform.Find("CameraVerticalRotationPivot");
        _mainCharacter = GameObject.Find("MainCharacter").transform;
    }

    void OnEnable() {
        PauseManager.OnGameContinue += ContinueCameraController;
        PauseManager.OnGamePause += PauseCameraController;
    }

    void OnDisable() {
        PauseManager.OnGameContinue -= ContinueCameraController;
        PauseManager.OnGamePause -= PauseCameraController;
    }

    private void PauseCameraController() {
        _isPaused = true;
    }

    private void ContinueCameraController() {
        _isPaused = false;
    }

    void Update() {
        if (!_isPaused) {
            RotateCameraHorizontally();
            RotateCameraVertically();

            if (Input.GetButtonDown("CameraSwitch")) {
                SwitchCameraMode();
            }

            if (Input.GetButtonDown("CameraRecenter")) {
                RecenterCamera();
            }
        }
    }

    private void RotateCameraHorizontally() {
        float rotationAmount = HorizontalRotationSpeed * Input.GetAxis("CameraHorizontal") * Time.deltaTime;
        _cameraPivotPoint.Rotate(Vector3.up * rotationAmount);
    }

    private void RotateCameraVertically() {
        float rotationAmount = VerticalRotationSpeed * Input.GetAxis("CameraVertical") * Time.deltaTime;
        _cameraVerticalRotationPivotPoint.Rotate(Vector3.left * rotationAmount);
        ClampVerticalRotation(_cameraVerticalRotationPivotPoint, MinimumVerticalRotationInDegrees, MaximumVerticalRotationInDegrees);
    }

    private void SwitchCameraMode() {
        CameraModes[GetCurrentCameraModeIndex()].enabled = false;
        CameraModes[GetNextCameraModeIndex()].enabled = true;
        _currentCameraModeIndex++;
    }

    private void RecenterCamera() {
        StopAllCoroutines();
        StartCoroutine(SmoothRecenterCamera());
    }

    private IEnumerator SmoothRecenterCamera() {
        Quaternion startRotation = _cameraPivotPoint.rotation;
        Quaternion endRotation = _mainCharacter.rotation;
        float elapsedTime = 0f;

        while (elapsedTime <= RecenterCameraTimeInSeconds) {
            float fraction = elapsedTime / RecenterCameraTimeInSeconds;
            _cameraPivotPoint.rotation = Quaternion.Slerp(startRotation, endRotation, fraction);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _cameraPivotPoint.rotation = endRotation;
    }

    private void ClampVerticalRotation(Transform ObjectToRotate, float MinimumAngle, float MaximumAngle) {
        Vector3 rotation = ObjectToRotate.localRotation.eulerAngles;
        rotation.x = Mathf.Clamp(rotation.x, MinimumAngle, MaximumAngle);
        ObjectToRotate.localRotation = Quaternion.Euler(rotation);
    }

    private int GetCurrentCameraModeIndex() {
        return _currentCameraModeIndex % CameraModes.Count;
    }

    private int GetNextCameraModeIndex() {
        return (_currentCameraModeIndex + 1) % CameraModes.Count;
    }
}
