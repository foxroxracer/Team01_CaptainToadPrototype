using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic_SlidingDown : MonoBehaviour {
 


    private Vector3 CombinedRaycast = new Vector3();
    private Vector3 raycastFloorPos = new Vector3();

    private Vector3 SlopeMovement = new Vector3();

    private CharacterController _charCTRL = null;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "MainCharacter")
        {
            //Stop player from moving normally and make him slide down the platform

        _charCTRL = other.gameObject.GetComponent<CharacterController>();

        SlopeMovement = new Vector3(_charCTRL.transform.position.x, FindFloorAverage().y, _charCTRL.transform.position.z);

        other.gameObject.transform.position = SlopeMovement;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "MainCharacter")
        {
            other.gameObject.GetComponent<CharacterControllerBehaviour>().CanMove = true;
        }
    }
    private Vector3 FindFloorAverage()
    {
        //Multiple Raycasts pointed to the ground
        //When going downhill the average will always be lower than the current player position

        // width of raycasts around the centre of your character
        float raycastWidth = 0.25f;
        // check floor on 5 raycasts   , get the average when not Vector3.zero  
        int floorAverage = 1;

        CombinedRaycast = FloorRaycasts(0, 0, 1.6f);
        floorAverage += (getFloorAverage(raycastWidth, 0) + getFloorAverage(-raycastWidth, 0) + getFloorAverage(0, raycastWidth) + getFloorAverage(0, -raycastWidth));

        return CombinedRaycast / floorAverage;
    }



    private int getFloorAverage(float offsetX, float offsetZ)
    {

        if (FloorRaycasts(offsetX, offsetZ, 1.6f) != Vector3.zero)
        {
            CombinedRaycast += FloorRaycasts(offsetX, offsetZ, 1.6f);
            return 1;
        }
        else { return 0; }
    }

    private Vector3 FloorRaycasts(float offsetX, float offsetZ, float raycastLength)
    {
        RaycastHit hit;
        // move raycast
        
        raycastFloorPos = _charCTRL.gameObject.transform.TransformPoint(0 + offsetX, 0 + 0.5f, 0 + offsetZ);

        Debug.DrawRay(raycastFloorPos, Vector3.down, Color.magenta);
        if (Physics.Raycast(raycastFloorPos, -Vector3.up, out hit, raycastLength))
        {
            return hit.point;
        }
        else return Vector3.zero;
    }
}
