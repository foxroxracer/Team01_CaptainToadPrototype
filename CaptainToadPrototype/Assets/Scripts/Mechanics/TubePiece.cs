using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TubePiece : MonoBehaviour {
    private MeshRenderer _tubeMeshRenderer;

    void Awake() {
        GameObject Tube = this.transform.Find("Tube").gameObject;

        Assert.IsNotNull(Tube, "No TUBE found, make sure there is a object named Tube in each of your pipes");

        _tubeMeshRenderer = Tube.GetComponent<MeshRenderer>();
    }

    public void SwitchMaterial(Material newMaterial) {
        _tubeMeshRenderer.material = newMaterial;
    }
}
