using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPortalScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] _plantHeads = new GameObject[0];
    private PlantTeleportScript _scriptUnderScrutiny;

    private GameObject _passenger;

    //Cooldownsystems
    [SerializeField]
    private float _coolDownTimerMax = 2;
    [SerializeField]
    private float _coolDownTimer;
    private bool _timerActive;
   public bool IsFunctional = true;

    private void FixedUpdate()
    {
        if (_timerActive)
        {
            IsFunctional = false;
            Timer();

        }
    }

    //teleports the passenger from one active head to the next
    public void Teleport()
    {
        
        for (int i = 0; i < _plantHeads.Length; i++)
        {
            _scriptUnderScrutiny = _plantHeads[i].GetComponent<PlantTeleportScript>();

            if (_passenger != null)
            {
                _passenger.transform.position = _plantHeads[i].transform.GetChild(0).transform.position;
                _passenger = null;
            }

            if (_scriptUnderScrutiny.Passenger != null)
            {
                _passenger = _scriptUnderScrutiny.Passenger;

                if(i == _plantHeads.Length -1)
                {
                    _passenger.transform.position = _plantHeads[0].transform.GetChild(0).transform.position;
                    _passenger = null;
                }
            }
            
        }
        _timerActive = true;
    }

    //Changes the state of a Head
    public void Swap(bool[] ChangedValues)
    {
        
        if(_plantHeads.Length == ChangedValues.Length)
        for(int i = 0; i< _plantHeads.Length; i++)
        {
                _scriptUnderScrutiny = _plantHeads[i].GetComponent<PlantTeleportScript>();
                if (ChangedValues[i])
            {
                    _scriptUnderScrutiny.IsActiveHead = true;
            }
                if(!ChangedValues[i])
                {
                    _scriptUnderScrutiny.IsActiveHead = false;
                }
        }
        else
        {
            Debug.Log("Switch requires the wrong amount of heads");
        }
        _timerActive = true;
    }

    void Timer()
    {
        _coolDownTimer += Time.fixedDeltaTime;

        if(_coolDownTimer >= _coolDownTimerMax)
        {
            _coolDownTimer = 0;
            _timerActive = false;
            IsFunctional = true;
        }
    }
}
