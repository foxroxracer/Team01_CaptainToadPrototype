using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic_RotatingTile : MonoBehaviour {
    public float IdleTimeInSeconds = 5f;
    public float RotatedTimeInSeconds = 2f;
    public float RotationTimeInSeconds = 1f;
    public MeshCollider TileMeshCollider;
    public Transform RotatableTile;

    void Start() {
        StartCoroutine("DoRotatingTileSequence");        
    }

    IEnumerator DoRotatingTileSequence() {
        yield return new WaitForSeconds(IdleTimeInSeconds);

        Quaternion startRotation = Quaternion.Euler(Vector3.zero);
        Quaternion endRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));

        TileMeshCollider.enabled = false;

        float elapsedTime = 0f;
        while (elapsedTime < RotationTimeInSeconds) {
            RotatableTile.transform.localRotation = Quaternion.Slerp(startRotation, endRotation, (elapsedTime/RotationTimeInSeconds));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        RotatableTile.transform.localRotation = endRotation;        

        yield return new WaitForSeconds(RotatedTimeInSeconds);

        float elapsedTime2 = 0f;
        while (elapsedTime2 < RotationTimeInSeconds) {
            RotatableTile.transform.localRotation = Quaternion.Slerp(endRotation, startRotation, (elapsedTime2 / RotationTimeInSeconds));
            elapsedTime2 += Time.deltaTime;
            yield return null;
        }

        RotatableTile.transform.localRotation = startRotation;

        TileMeshCollider.enabled = true;

        StartCoroutine("DoRotatingTileSequence");
    }
}
