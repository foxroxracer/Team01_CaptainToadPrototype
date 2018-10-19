using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluckPatch : MonoBehaviour {
    [SerializeField]
    private int _possiblePulls; //Pulls player can do before Pluck patch is empty

    [SerializeField]
    private GameObject _droppedItem; //What can the player receive from the pluck patch

    public Material DeactivatedMaterial;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "MainCharacter")
        {
            Debug.Log("Colliding with Toad");
            //If toad is in this trigger, allow the player to press X | B | A to pluck the turnip
            //Has priority over moving around ! 
            if (Input.GetButtonDown("X") || Input.GetButtonDown("A") || Input.GetButtonDown("B"))
            {
                //lower possible pulls of this pluckPatch by one
                _possiblePulls--;

                if (_possiblePulls >= 0)
                {
                    //Make something happen i.e Coin comes out
                    Instantiate(_droppedItem,other.gameObject.transform.position,Quaternion.Euler(-90,0,0));

                    

                    if (_possiblePulls == 0)
                    {
                        //If player can not pull from this pluck patch anymore:

                        //Change Material of Model
                        GameObject modelGameobject = transform.GetChild(0).gameObject;
                        int numOfChildren = modelGameobject.transform.childCount;
                        Debug.Log(modelGameobject.transform.childCount);
                        for (int i = 0; i < numOfChildren-1; i++)
                        {
                            GameObject child = modelGameobject.transform.GetChild(i).gameObject;
                            child.GetComponent<Renderer>().material = DeactivatedMaterial;
                        }
                        GameObject PlantChild = modelGameobject.transform.GetChild(modelGameobject.transform.childCount-1).gameObject;
                        PlantChild.SetActive(false);

                        //Deactivate Script
                        this.gameObject.GetComponent<PluckPatch>().enabled = false;
                    }                  
                }
            }
        }
    }


}
