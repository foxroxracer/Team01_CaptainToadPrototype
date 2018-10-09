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
        SceneManager.LoadScene("Level_01");
    }

    public void GoToLevelTwo() {
        SceneManager.LoadScene("Level_02");
    }

    public void GoToLevelThree() {
        SceneManager.LoadScene("Level_03");
    }

    public void GoToTestLevel() {
        SceneManager.LoadScene("TestLevel");
    }

    public void GoToEndScreen() {
        SceneManager.LoadScene("EndScreen");
    }
}
