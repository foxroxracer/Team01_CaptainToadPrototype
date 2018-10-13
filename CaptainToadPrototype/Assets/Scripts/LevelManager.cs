using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static int currentLevel = 0;
    public static int numberOfLevels = 3;

    public static void SetCurrentLevel(int level) {
        currentLevel = level;
    }

    public static void GoToNextLevel() {
        currentLevel++;

        if (currentLevel > numberOfLevels) {
            GameObject.Find("SceneChanger").GetComponent<SceneChanger>().GoToEndScreen();
        } else {
            GameObject.Find("SceneChanger").GetComponent<SceneChanger>().GoToLevel(currentLevel);
        }
    }
}
