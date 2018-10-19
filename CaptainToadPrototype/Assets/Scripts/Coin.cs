using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private GameObject _toad;

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

        //Make coin move up (?)

        //Remove Coin
        Destroy(this.gameObject);
    }
}
