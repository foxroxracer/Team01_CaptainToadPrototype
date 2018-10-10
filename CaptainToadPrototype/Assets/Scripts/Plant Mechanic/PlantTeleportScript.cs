using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTeleportScript : MonoBehaviour {

    [SerializeField]
    private PlantPortalScript _plantHub;
    public bool IsActiveHead;
    public GameObject Passenger;

    [SerializeField]
    private GameObject _activeHead;
    [SerializeField]
    private GameObject _inactiveHead;
    [SerializeField]
    private GameObject _vine;

    private void Start()
    {
        if(_plantHub == null)
        {
            Debug.Log("Plant head not configured correctly, missing hub");
        }
    }

    private void Update()
    {
        CheckActiveHead();
    }

    void CheckActiveHead()
    {
        if(IsActiveHead)
        {
            _activeHead.SetActive(true);
            _inactiveHead.SetActive(false);

            
        }
        else
        {
            _activeHead.SetActive(false);
            _inactiveHead.SetActive(true);
        }

        if (_vine != null && _vine.GetComponent<VineScript>() != null)
        {
            _vine.GetComponent<VineScript>().Swap(IsActiveHead);
        }
    }


    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Object Detected");
        if (_plantHub.IsFunctional)
        {
            Debug.Log("Planthub Functional Confirmed");
            if (collider.gameObject.GetComponent<TeleportableMarker>() != null)
            {
                Debug.Log("Marker Detected");
                if (collider.gameObject.GetComponent<TeleportableMarker>().Teleportable)
                {
                    Passenger = collider.gameObject.GetComponent<TeleportableMarker>().ParentObject;
                    Debug.Log("Teleportable object confirmed");
                    _plantHub.Teleport();
                }
            }

           
            Clear();
        }
    }

    private void Clear()
    {
        Passenger = null;
    }
}
