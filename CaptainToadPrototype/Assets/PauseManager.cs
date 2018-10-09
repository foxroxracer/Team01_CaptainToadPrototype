using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    public delegate void PausingAction();
    public static event PausingAction OnGamePause;

    public delegate void ContinueAction();
    public static event ContinueAction OnGameContinue;

    public static void PauseGame() {
        if (ThereAreRecieversForOnPauseEvent()) {
            OnGamePause();
        }
    }

    public static void ContinueGame() {
        if (ThereAreRecieversForOnContinueEvent()) {
            OnGameContinue();
        }
    }

    private static bool ThereAreRecieversForOnPauseEvent() {
        return OnGamePause != null;
    }

    private static bool ThereAreRecieversForOnContinueEvent() {
        return OnGameContinue != null;
    }
}
