using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic_SpikeTrap : MonoBehaviour {
    public float SpikeOutTimeInSeconds = 2f;
    public float SpikeInTimeInSeconds = 4f;
    public float TimeToRaiseSpikesInSeconds = 0.3f;
    public float TimeToSubtractSpikesInSeconds = 3f;
    public Transform StartPosition;
    public Transform EndPosition;
    public Transform Spikes;

    private BoxCollider _boxTrigger;

    void Start() {
        _boxTrigger = this.GetComponent<BoxCollider>();
        StartCoroutine("DoSpikeTrapSequence");
    }

    IEnumerator DoSpikeTrapSequence() {
        yield return new WaitForSeconds(SpikeInTimeInSeconds);

        _boxTrigger.enabled = true;

        Vector3 startPositionSpikes = StartPosition.transform.localPosition;
        Vector3 endPositionSpikes = EndPosition.transform.localPosition;

        float elapsedTimeRaisingSpikes = 0f;
        while (elapsedTimeRaisingSpikes < TimeToRaiseSpikesInSeconds) {
            Spikes.transform.localPosition = Vector3.Lerp(startPositionSpikes, endPositionSpikes, (elapsedTimeRaisingSpikes/TimeToRaiseSpikesInSeconds));
            elapsedTimeRaisingSpikes += Time.deltaTime;
            yield return null;
        }
        Spikes.transform.localPosition = endPositionSpikes;

        yield return new WaitForSeconds(SpikeOutTimeInSeconds);

        _boxTrigger.enabled = false;

        float elapsedTimeSubtractingSpikes = 0f;
        while (elapsedTimeSubtractingSpikes < TimeToSubtractSpikesInSeconds) {
            Spikes.transform.localPosition = Vector3.Lerp(endPositionSpikes, startPositionSpikes, (elapsedTimeSubtractingSpikes / TimeToSubtractSpikesInSeconds));
            elapsedTimeSubtractingSpikes += Time.deltaTime;
            yield return null;
        }
        Spikes.transform.localPosition = startPositionSpikes;

        StartCoroutine("DoSpikeTrapSequence");
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MainCharacter") {
            other.gameObject.GetComponent<MainCharacterDeath>().KillMainCharacter();
        }    
    }
}
