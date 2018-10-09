using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    Animator _movingPlatformAnimator;
    public GameObject PressAButtonCanvas;

    void Start() {
        _movingPlatformAnimator = this.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MainCharacter") {
            other.gameObject.transform.parent = this.transform;
            PressAButtonCanvas.SetActive(true);
        }    
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "MainCharacter") {
            other.gameObject.transform.parent = null;
            other.gameObject.transform.localScale = Vector3.one * 0.5f;
            PressAButtonCanvas.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "MainCharacter" && Input.GetButtonDown("MovePlatform")) {
            _movingPlatformAnimator.SetTrigger("MovePlatformTrigger");
        }            
    }
}
