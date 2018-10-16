using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureValveTrigger : MonoBehaviour {
    public GameObject AButtonPopup;

    private Mechanic_TemperatureTubes _temperatureTubeMechanic;
    private bool _mainCharacterInTriggerArea = false;

    void Start() {
        _temperatureTubeMechanic = this.GetComponent<Mechanic_TemperatureTubes>();     
    }

    void Update() {
        if (_mainCharacterInTriggerArea && AButtonPressed()) {
            _temperatureTubeMechanic.SwitchTubesTemperature();
        }    
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MainCharacter") {
            _mainCharacterInTriggerArea = true;
            AButtonPopup.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "MainCharacter") {
            _mainCharacterInTriggerArea = false;
            AButtonPopup.SetActive(false);
        }
    }

    private bool AButtonPressed() {
        return Input.GetButtonDown("A");
    }
}
