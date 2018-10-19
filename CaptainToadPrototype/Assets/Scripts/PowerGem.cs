using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGem : MonoBehaviour {
    private GameObject _uiUpdater;
    private void Start()
    {
        _uiUpdater = GameObject.Find("UI_Update");
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "MainCharacter")
        {
            //Add one to Collected Power Gems (3 Total)
            _uiUpdater.GetComponent<InGameUI>().UpdatePowerGems();
            //Remove PowerGem from Level
            Destroy(this.gameObject);
        }
    }
}
