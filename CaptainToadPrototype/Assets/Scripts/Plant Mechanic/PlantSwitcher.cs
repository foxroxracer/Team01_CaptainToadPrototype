using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSwitcher : MonoBehaviour {

   // [SerializeField]
   // private 
    

    [Serializable]
    public class States
        {
        public bool[] Values;
        public PlantPortalScript PlantHubScript;
    }

    [SerializeField]
    private States[] _switchStates = new PlantSwitcher.States[0];


    //Changes to the next Switch State. Automatically becomes false by SwitchActive
    //Can later be changed to make Update obsolete
    public bool Activate;

    private int _count = 0;

    private void Update()
    {
        if(Activate)
        {
            SwitchActive();
        }
    }

    //Swaps the states of the targeted heads according to the next Switch State
    void SwitchActive()
    {
        // for (int i = 0; i < _switchStates.Length; i++)
        if (_switchStates[_count].PlantHubScript.IsFunctional)
        {
            _switchStates[_count].PlantHubScript.Swap(_switchStates[_count].Values);
            _count++;
            if (_count == _switchStates.Length)
            { _count = 0; }
        }
            Activate = false;
        
    }
}
