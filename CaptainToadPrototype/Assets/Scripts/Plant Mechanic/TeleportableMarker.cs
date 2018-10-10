using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportableMarker : MonoBehaviour {

    //This constitutes whether or not something is teleportable

    public bool Teleportable = true;
    public GameObject ParentObject;
    public bool Teleporting = false;
}
