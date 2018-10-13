using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public void GoToStartScreen() {
        SceneManager.LoadScene("StartScreen");
    }

    public void GoToLevelSelectScreen() {
        SceneManager.LoadScene("LevelSelectScreen");
    }

    public void GoToLevelOne() {
        SceneManager.LoadScene("Level_1");
        LevelManager.SetCurrentLevel(1);
    }

    public void GoToLevelTwo() {
        SceneManager.LoadScene("Level_2");
        LevelManager.SetCurrentLevel(2);
    }

    public void GoToLevelThree() {
        SceneManager.LoadScene("Level_3");
        LevelManager.SetCurrentLevel(3);
    }

    public void GoToTestLevel() {
        SceneManager.LoadScene("TestLevel");
    }

    public void GoToEndScreen() {
        SceneManager.LoadScene("EndScreen");
    }

    public void GoToLevel(int level) {
        switch (level) {
            case 1:
                GoToLevelOne();
                break;
            case 2:
                GoToLevelTwo();
                break;
            case 3:
                GoToLevelThree();
                break;
        }
    }
}
