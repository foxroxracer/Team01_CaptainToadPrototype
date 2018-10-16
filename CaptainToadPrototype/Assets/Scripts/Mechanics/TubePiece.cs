using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubePiece : MonoBehaviour {
    private MeshRenderer _tubeMeshRenderer;

    void Awake() {
        _tubeMeshRenderer = this.transform.Find("Tube").GetComponent<MeshRenderer>();
    }

    public void SwitchMaterial(Material newMaterial) {
        _tubeMeshRenderer.material = newMaterial;
    }
}
