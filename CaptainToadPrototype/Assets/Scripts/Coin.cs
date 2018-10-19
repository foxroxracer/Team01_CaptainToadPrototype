using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private GameObject _uiUpdater;
    private GameObject _toad;

    private void Start()
    {
        _uiUpdater = GameObject.Find("UI_Update");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MainCharacter")
        {
            _toad = other.gameObject;
        }
    }

    private void Update()
    {
        if (_toad != null)
        {
            CoinAction();
        }
    }

    private void CoinAction()
    {
        //From the moment The coin Spawns

        //Add one to current coins
        _uiUpdater.GetComponent<InGameUI>().CurrentCoins =_uiUpdater.GetComponent<InGameUI>().CurrentCoins + 1;
        
        //Remove Coin
        Destroy(this.gameObject);
    }
}
