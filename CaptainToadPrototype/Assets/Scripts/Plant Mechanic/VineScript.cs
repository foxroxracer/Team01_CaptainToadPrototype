using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineScript : MonoBehaviour {

    [SerializeField]
    private GameObject _activeVine;
    [SerializeField]
    private GameObject _inactiveVine;

    public void Swap(bool isActive)
    {
        if(!isActive)
        {
            _activeVine.SetActive(false);
            _inactiveVine.SetActive(true);
        }
        if(isActive)
        {
            _activeVine.SetActive(true);
            _inactiveVine.SetActive(false);
        }
    }
}
