using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraMode : MonoBehaviour {
    [Range(0f, 100f)]
    public float DistanceFromPivotPoint = 10f;
    public float SmoothingTime = 0.2f;
    public float CameraTransitionTimeInSeconds = 0.5f;

    [Header("FOV")]
    public float CameraFOV = 27f;

    public AnimationCurve CameraTransitionCurve;

    protected Transform _cameraPivotPoint;
    protected Transform _mainCharacter;
    protected Vector3 _cameraMovingVelocity = Vector3.zero;
    private Transform _mainCameraTransform;
    private Camera _mainCamera;

    void Awake() {
        _mainCharacter = GameObject.Find("MainCharacter").transform;
        _cameraPivotPoint = this.transform.Find("CameraPivot");
        _mainCameraTransform = GameObject.Find("MainCamera").transform;
        _mainCamera = _mainCameraTransform.GetComponent<Camera>();
    }

    void OnEnable() {
        StartCoroutine(TransitionToNewCameraMode());
    }

    void OnDisable() {
        StopAllCoroutines();    
    }

    void Update() {
        MovePivotPoint();
    }

    private IEnumerator TransitionToNewCameraMode() {
        Vector3 startPositionPivotPoint = _cameraPivotPoint.position;
        Vector3 endPositionPivotPoint = GetStartingCameraPivotPoint();

        Vector3 startCameraPosition = _mainCameraTransform.localPosition;
        Vector3 endCameraPosition = new Vector3(_mainCameraTransform.localPosition.x, _mainCameraTransform.localPosition.y, -DistanceFromPivotPoint);

        float startCamereFOV = _mainCamera.fieldOfView;
        float endCameraFOV = CameraFOV;
        
        float elapsedTime = 0f;

        while (elapsedTime < CameraTransitionTimeInSeconds) {
            float fraction = (elapsedTime / CameraTransitionTimeInSeconds);

            float transitionFraction = CameraTransitionCurve.Evaluate(fraction);
            _cameraPivotPoint.position = Vector3.Lerp(startPositionPivotPoint, GetStartingCameraPivotPoint(), transitionFraction);
            _mainCameraTransform.localPosition = Vector3.Lerp(startCameraPosition, endCameraPosition, transitionFraction);
            _mainCamera.fieldOfView = Mathf.Lerp(startCamereFOV, endCameraFOV, transitionFraction);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public virtual Vector3 GetStartingCameraPivotPoint() {
        return _mainCharacter.position;
    }

    private void SetCameraDistance() {
        Vector3 cameraPosition = _mainCameraTransform.localPosition;
        cameraPosition.z = -DistanceFromPivotPoint;
        _mainCameraTransform.localPosition = cameraPosition;
    }

    public virtual void MovePivotPoint() {
        _cameraPivotPoint.position = Vector3.SmoothDamp(_cameraPivotPoint.position, _mainCharacter.position, ref _cameraMovingVelocity, SmoothingTime);
    }
}
