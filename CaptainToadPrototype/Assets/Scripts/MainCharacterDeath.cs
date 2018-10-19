using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterDeath : MonoBehaviour {

    public GameObject MainCharacterDeathFX;
    private GameObject _mainCharacterModel;
    private CharacterControllerBehaviour _charController;

    void Start() {
        _mainCharacterModel = this.transform.Find("Model_Toad").gameObject;
        _charController = this.GetComponent<CharacterControllerBehaviour>();
    }

    public void KillMainCharacter() {
        HideMainCharacter();
        StopMainCharacterMovement();
        DoMainCharacterDeathFX();
        Invoke("RestartLevel", 2f);


        GameObject uiUpdate = GameObject.Find("UI_Update");
        uiUpdate.GetComponent<InGameUI>().CurrentLives = uiUpdate.GetComponent<InGameUI>().CurrentLives - 1;
    }

    private void HideMainCharacter() {
        _mainCharacterModel.SetActive(false);
    }

    private void StopMainCharacterMovement() {
        _charController.enabled = false;
    }

    private void DoMainCharacterDeathFX() {
        Instantiate(MainCharacterDeathFX, this.transform.position, Quaternion.identity);
    }

    private void RestartLevel() {
        LevelManager.RestartCurrentLevel();
    }
}
