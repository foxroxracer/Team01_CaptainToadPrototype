using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenOntoObject : MonoBehaviour {
    public BoxCollider FreezedObjectCollider;
    private Rigidbody _rb;

    void Start() {
        _rb = this.GetComponent<Rigidbody>();    
    }

    private void Update() {
        if (!FreezedObjectCollider.enabled) {
            _rb.isKinematic = false;
            _rb.useGravity = true;
        }
    }

}
