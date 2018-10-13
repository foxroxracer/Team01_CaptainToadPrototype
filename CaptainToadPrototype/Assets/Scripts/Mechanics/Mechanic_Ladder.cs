using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic_Ladder : MonoBehaviour {

    private bool _topToBottom = false;
    private bool _allowClimbing = false;

    private float _lerpSpeed = 0.0f;

    private Vector3 _moveDirection;

    [SerializeField]
    private Transform _ladderLeavePos; //Position toad will end up when leaving the ladder
    [SerializeField]
    private Transform _ladderEnterTopPos; //Position toad will end up when entering the ladder from the top

    private bool _leavingLadder = false;

    private GameObject _toad;

    private void Update()
    {
        if(_leavingLadder == true)
        {
            LeaveLadder();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check from which height Toad is entering the ladder

        if(other.gameObject.transform.position.y >= _ladderLeavePos.position.y)
        {
            //Toad is coming from above
            _topToBottom = true;
        }
        else
        {
            _allowClimbing = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "MainCharacter")
        {
                _toad = other.gameObject;

            //If toad is coming from the Top
            if (_topToBottom)
            {
                //Lerp toads position so he is on the ladder and can move up/down from there
                if (LerpPos(_ladderEnterTopPos.position, _lerpSpeed) >= 0.9f)
                {
                    _lerpSpeed = 0;
                    _topToBottom = false;
                    _allowClimbing = true;
                }
            }
           
                //When toad enters Ladder trigger, make sure toad can only move up or down, no longer sideways
                //ClimbingLadder bool when turned to false will make sure toad can no longer move

                _toad.GetComponent<CharacterControllerBehaviour>().ClimbingLadder = true;
                _toad.GetComponent<CharacterControllerBehaviour>().CanMove = false;

                //Replace toad movement

                //Get Left joystick vertical movement
                float verticalInput = Input.GetAxis("Vertical");

                //Give movement to Toad
                _toad.GetComponent<CharacterController>().Move(Vector3.up * verticalInput * Time.deltaTime * 2.5f);
           
                //Make it so that Toad is always facing the ladder
                _toad.transform.forward = Vector3.Lerp(_toad.transform.forward, this.gameObject.transform.forward * -1, Time.deltaTime * 5);

            IsAtBottom();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If toad leaves ladder set bool to true
        //Check in which way toad is leaving the ladder
        if(_toad.transform.position.y > _ladderLeavePos.position.y && _topToBottom ==false)
        {
        _leavingLadder = true;
        }

    }

    private void LeaveLadder()
    {
        
        if(LerpPos(_ladderLeavePos.position,_lerpSpeed) >= 0.9f)
        {
            ResetLadder();
            _lerpSpeed = 0.0f;
        }

    }

    private float LerpPos(Vector3 targetPos, float _lerp)
    {
        _lerpSpeed += Time.deltaTime;
        //Lerp Toads position to position at top of ladder
        _toad.transform.position = Vector3.Lerp(_toad.transform.position, targetPos, _lerpSpeed);

        return _lerp;
    }

    private void IsAtBottom()
    {
        if (_toad.gameObject.GetComponent<CharacterController>().isGrounded && Input.GetAxis("Vertical") <= -0.5)
        {
            //Player wants toad to leave the ladder
            Vector3 newLerpPos = new Vector3(_toad.transform.position.x + transform.forward.x, _toad.transform.position.y, _toad.transform.position.z + transform.forward.z);
            if (LerpPos(newLerpPos, _lerpSpeed) >= 0.9f){
                _lerpSpeed = 0.0f;
            }
            ResetLadder();
        }
    }

    private void ResetLadder()
    {
        //Reset all changes
        _toad.GetComponent<CharacterControllerBehaviour>().ClimbingLadder = false;
        _toad.GetComponent<CharacterControllerBehaviour>().CanMove = true;
        _leavingLadder = false;
        _allowClimbing = false;
        _topToBottom = false;

    }
}
