using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGem : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MainCharacter")
        {
        //Add one to Collected Power Gems (3 Total)
        
        //Remove PowerGem from Level
        Destroy(this.gameObject);
        }
    }
}
