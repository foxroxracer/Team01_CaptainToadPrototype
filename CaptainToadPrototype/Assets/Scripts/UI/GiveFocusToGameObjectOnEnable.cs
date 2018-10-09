using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GiveFocusToGameObjectOnEnable : MonoBehaviour {
    public GameObject ObjectToGiveFocus;

    private GameObject _originalGameObject;

    void OnEnable() {
        SaveCurrentSelectedObject();
        Invoke("HighLightGameObject", 0.1f);
    }

    void OnDisable() {
        HighLightPreviousGameObject();
    }

    private void SaveCurrentSelectedObject() {
        if (EventSystem.current.currentSelectedGameObject != null) {
            _originalGameObject = EventSystem.current.currentSelectedGameObject;
        }
    }

    private void HighLightGameObject() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ObjectToGiveFocus);
        //ObjectToGiveFocus.GetComponent<Button>().Select();
    }

    private void HighLightPreviousGameObject() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_originalGameObject);
    }
}
