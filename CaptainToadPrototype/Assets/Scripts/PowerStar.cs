using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStar : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MainCharacter") {
            Destroy(this.gameObject);
            LevelManager.GoToNextLevel();
        }    
    }
}
