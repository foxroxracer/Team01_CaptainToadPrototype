using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class SmallZoomCameraMode : CameraMode {
    public Transform CameraDollyRightPoint;
    public Transform CameraDollyLeftPoint;

#if DEBUG
    void Start() {
        Assert.IsNotNull(CameraDollyRightPoint, "No dolly track found, please put the SmallZoomCameraDollyTrack (inside prefabs) in the scene and assign it.");
        Assert.IsNotNull(CameraDollyLeftPoint, "No dolly track found, please put the SmallZoomCameraDollyTrack (inside prefabs) in the scene and assign it.");
    }
#endif

    public override void MovePivotPoint() {
        Vector3 newPivotPointPosition = GetCameraPivotPointOnCameraDolly();
        _cameraPivotPoint.position = Vector3.SmoothDamp(_cameraPivotPoint.position, newPivotPointPosition, ref _cameraMovingVelocity, SmoothingTime);
    }

    public override Vector3 GetStartingCameraPivotPoint() {
        return GetCameraPivotPointOnCameraDolly();
    }

    private Vector3 GetCameraPivotPointOnCameraDolly() {
        float mainCharacterXposition = _mainCharacter.position.x;
        float fraction = CalculateFractionBetweenTwoValues(mainCharacterXposition, CameraDollyLeftPoint.position.x, CameraDollyRightPoint.position.x);
        Vector3 newPivotPointPosition = Vector3.Lerp(CameraDollyLeftPoint.localPosition, CameraDollyRightPoint.localPosition, fraction);

        return newPivotPointPosition;
    }

    private float CalculateFractionBetweenTwoValues(float currentValue, float minValue, float maxValue) {
        float fraction = 0f;
        float positiveMaximumValue = maxValue + Mathf.Abs(minValue);
        float positiveCurrentValue = currentValue + Mathf.Abs(minValue);

        fraction = (positiveCurrentValue / positiveMaximumValue);

        return fraction;
    }
}
