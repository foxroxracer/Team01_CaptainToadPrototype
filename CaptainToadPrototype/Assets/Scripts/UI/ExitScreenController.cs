using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScreenController : MonoBehaviour {
    public GameObject ExitScreen;
    private bool _exitScreenIsVisible = false;

    void Update() {
        if (Input.GetButtonDown("Cancel")) {
            if (_exitScreenIsVisible) {
                HideExitScreen();
            } else {
                ShowExitScreen();
            }
        }    
    }

    public void QuitApplication() {
        Application.Quit();
    }

    public void ContinueApplication() {
        HideExitScreen();
    }

    private void ShowExitScreen() {
        PauseManager.PauseGame();
        ExitScreen.SetActive(true);
        _exitScreenIsVisible = true;
    }

    private void HideExitScreen() {
        PauseManager.ContinueGame();
        ExitScreen.SetActive(false);
        _exitScreenIsVisible = false;
    }
}
