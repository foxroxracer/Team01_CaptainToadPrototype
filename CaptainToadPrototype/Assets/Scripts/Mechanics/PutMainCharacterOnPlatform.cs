using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PutMainCharacterOnPlatform : MonoBehaviour {
    public Transform Platform;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MainCharacter") {
            other.gameObject.transform.parent = Platform;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "MainCharacter") {
            other.gameObject.transform.parent = null;
        }
    }
}
