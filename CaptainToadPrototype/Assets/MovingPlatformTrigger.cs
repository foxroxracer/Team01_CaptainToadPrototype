using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformTrigger : MonoBehaviour {

    public Mechanic_MovingPlatform MovingPlatformMechanic;
    private bool _mainCharacterOnPlatform = false;

    void Update() {
        if (_mainCharacterOnPlatform && AButtonPressed()) {
            MovingPlatformMechanic.TogglePlatformMovement();
        }    
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MainCharacter") {
            PutMainCharacterOnPlatform(other.gameObject.transform);
            _mainCharacterOnPlatform = true;
        }        
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "MainCharacter") {
            RemoveMainCharacterFromPlatform(other.gameObject.transform);
            _mainCharacterOnPlatform = false;
        }
    }

    private void PutMainCharacterOnPlatform(Transform mainCharacter) {
        mainCharacter.transform.parent = MovingPlatformMechanic.Platform;
    }

    private void RemoveMainCharacterFromPlatform(Transform mainCharacter) {
        mainCharacter.transform.parent = null;
    }

    private bool AButtonPressed() {
        return Input.GetButtonDown("A");
    }
}
