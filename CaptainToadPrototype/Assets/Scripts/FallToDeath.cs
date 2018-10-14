using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallToDeath : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MainCharacter") {
            other.gameObject.GetComponent<MainCharacterDeath>().KillMainCharacter();
        }    
    }
}
