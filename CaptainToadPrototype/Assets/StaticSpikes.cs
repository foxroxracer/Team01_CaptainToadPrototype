using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpikes : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MainCharacter") {
            other.gameObject.GetComponent<MainCharacterDeath>().KillMainCharacter();
        }    
    }
}
