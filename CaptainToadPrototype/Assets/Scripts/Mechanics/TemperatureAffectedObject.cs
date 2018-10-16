using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class TemperatureAffectedObject : MonoBehaviour {
    private Collider _objectCollider;
    private MeshRenderer _meshRenderer;
    public Material MeltedMaterial;
    public Material FreezedMaterial;

    void Awake() {
        _objectCollider = this.GetComponent<Collider>();
        _meshRenderer = this.GetComponent<MeshRenderer>();
    }

    public void Melt() {
        DisableObjectCollider();
        ChangeMaterialToMeltedMaterial();
    }

    public void Freeze() {
        EnableObjectCollider();
        ChangeMaterialToFreezedMaterial();
    }

    private void DisableObjectCollider() {
        _objectCollider.enabled = false;
    }

    private void EnableObjectCollider() {
        _objectCollider.enabled = true;
    }

    private void ChangeMaterialToMeltedMaterial() {
        _meshRenderer.material = MeltedMaterial;
    }

    private void ChangeMaterialToFreezedMaterial() {
        _meshRenderer.material = FreezedMaterial;
    }
}
