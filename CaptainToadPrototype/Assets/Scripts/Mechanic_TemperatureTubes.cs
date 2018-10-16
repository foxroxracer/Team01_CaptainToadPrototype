using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic_TemperatureTubes : MonoBehaviour {
    public Material HotTubeMaterial;
    public Material ColdTubeMaterial;

    public Transform LeftSideTubesContainer;
    public Transform RightSideTubesContainer;

    public Transform LeftSideTemperatureAffectedObjectsContainer;
    public Transform RightSideTemperatureAffectedObjectsContainer;

    private List<TemperatureAffectedObject> _leftSideTemperatureAffectedObjects = new List<TemperatureAffectedObject>();
    private List<TemperatureAffectedObject> _rightSideTemperatureAffectedObjects = new List<TemperatureAffectedObject>();

    private List<TubePiece> _leftSideTubes = new List<TubePiece>();
    private List<TubePiece> _rightSideTubes = new List<TubePiece>();

    public enum Temperature {
        Hot,
        Cold
    }

    private Temperature _currentLeftTubesTemperature = Temperature.Cold;
    private Temperature _currentRightTubesTemperature = Temperature.Hot;

    void Start() {
        FillTubeListFromContainers();
        FillTemperatureAffectedObjectsFromContainer();
        UpdateTubeMaterials();
        UpdateTemperatureAffectedObjects();
    }

    public void SwitchTubesTemperature() {
        _currentLeftTubesTemperature = (_currentLeftTubesTemperature == Temperature.Cold) ? Temperature.Hot : Temperature.Cold;
        _currentRightTubesTemperature = (_currentRightTubesTemperature == Temperature.Cold) ? Temperature.Hot : Temperature.Cold;

        UpdateTubeMaterials();
        UpdateTemperatureAffectedObjects();
    }

    private void FillTubeListFromContainers() {
        foreach (Transform child in LeftSideTubesContainer) {
            _leftSideTubes.Add(child.GetComponent<TubePiece>());
        }

        foreach (Transform child in RightSideTubesContainer) {
            _rightSideTubes.Add(child.GetComponent<TubePiece>());
        }
    }

    private void FillTemperatureAffectedObjectsFromContainer() {
        foreach (Transform child in LeftSideTemperatureAffectedObjectsContainer) {
            _leftSideTemperatureAffectedObjects.Add(child.GetComponent<TemperatureAffectedObject>());
        }

        foreach (Transform child in RightSideTemperatureAffectedObjectsContainer) {
            _rightSideTemperatureAffectedObjects.Add(child.GetComponent<TemperatureAffectedObject>());
        }
    }

    private void UpdateTubeMaterials() {
        Material leftSideTubeMaterial = GetMaterialByTemperature(_currentLeftTubesTemperature);
        Material rightSideTubeMaterial = GetMaterialByTemperature(_currentRightTubesTemperature);

        foreach (TubePiece tube in _leftSideTubes) {
            tube.SwitchMaterial(leftSideTubeMaterial);
        }

        foreach (TubePiece tube in _rightSideTubes) {
            tube.SwitchMaterial(rightSideTubeMaterial);
        }
    }

    private void UpdateTemperatureAffectedObjects() {
        bool leftSideNeedsToMelt = (_currentLeftTubesTemperature == Temperature.Hot);
        bool rightSideNeedsToMelt = (_currentRightTubesTemperature == Temperature.Hot);

        foreach (TemperatureAffectedObject obj in _leftSideTemperatureAffectedObjects) {
            if (leftSideNeedsToMelt) {
                obj.GetComponent<TemperatureAffectedObject>().Melt();
            } else {
                obj.GetComponent<TemperatureAffectedObject>().Freeze();
            }
        }

        foreach (TemperatureAffectedObject obj in _rightSideTemperatureAffectedObjects) {
            if (rightSideNeedsToMelt) {
                obj.GetComponent<TemperatureAffectedObject>().Melt();
            } else {
                obj.GetComponent<TemperatureAffectedObject>().Freeze();
            }
        }
    }

    private Material GetMaterialByTemperature(Temperature temp) {
        return (temp == Temperature.Cold) ? ColdTubeMaterial : HotTubeMaterial;
    }
}
