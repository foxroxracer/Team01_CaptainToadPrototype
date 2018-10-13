using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBehaviour : MonoBehaviour {
    public bool CanFall = true;
    public bool ClimbingLadder = false;
    public bool LeavingLadder = false;

    public bool CanMove = true;
    public Transform CameraTransform;

    public Transform CurrentObjectTransform;


    public CharacterController _charCTRL;


    private float _verticalInput;
    private float _horizontalInput;

    private Vector3 _moveDirection;

    private float _inputAmount;

    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _baseMoveSpeed;
    [SerializeField]
    private float _maxMoveSpeed;

    private float _moveSpeed;

    private Vector3 _downWardsForce = Vector3.zero;
    private bool _isPaused = false;


	void Start () {
        _charCTRL = GetComponent<CharacterController>();

        _moveSpeed = _baseMoveSpeed;
	}

    void OnEnable() {
        PauseManager.OnGameContinue += ContinueCharacterController;
        PauseManager.OnGamePause += PauseCharacterController;
    }

    void OnDisable() {
        PauseManager.OnGameContinue -= ContinueCharacterController;
        PauseManager.OnGamePause -= PauseCharacterController;
    }


    void Update () {
        if (!_isPaused) {
            MovementCalculation();
            Sprinting();


            if (CanFall == true && ClimbingLadder == false)
            {
                WalkDownSlope();
                _charCTRL.SimpleMove(_downWardsForce);
            }
          

            ApplyGravity();

            Vector3 finalMovement = _moveDirection + _downWardsForce;
            if (CanMove == true)
            {
            _charCTRL.Move(finalMovement * _moveSpeed * Time.deltaTime);
            }
        }
    }

    private void WalkDownSlope()
    {
        float snapDistance = 0.5f;
        if (_charCTRL.isGrounded == false)
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hitInfo, snapDistance))
                _charCTRL.Move(hitInfo.point - transform.position);
        }
    }

    private void ApplyGravity()
    {
        //Base gravity that will always affect Toad
         _downWardsForce.y += Physics.gravity.y*Time.deltaTime;
        
        if (_charCTRL.isGrounded)
        {
                _downWardsForce -= Vector3.Project(_downWardsForce, Physics.gravity); //Reset downwards Gravity
        }
    }

    private void MovementCalculation()
    {
        //Get Left joystick input from Controller to move around
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        //Multiply with Characters forward and right so movement is based on camera position
        Vector3 correctedHorizontal = _horizontalInput * CameraTransform.right;
        Vector3 correctedVertical = _verticalInput * CameraTransform.forward;

        //Combine and Normalise so toad will not go faster when moving diagonal
        //Also make y=0 so character cant go into the ground

        Vector3 finalInput = correctedHorizontal + correctedVertical;


        _moveDirection = new Vector3(finalInput.normalized.x,0, finalInput.normalized.z);
        

        //Make sure input doesn't go above 1 or negative for safety
        //Also makes sure movemement scales up and stops increasing

        float inputMag = Mathf.Abs(_horizontalInput) + Mathf.Abs(_verticalInput);
        _inputAmount = Mathf.Clamp01(inputMag);
        
        if(ClimbingLadder == false)
        {
            Quaternion rot = new Quaternion();
            // rotate Toad according to Camera rotation
            if (_moveDirection != Vector3.zero) {
                rot = Quaternion.LookRotation(_moveDirection);
            }
        
            //Slerp to decide how fast toad rotates match camera forward
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * _inputAmount * _rotationSpeed);
            transform.rotation = targetRotation;
        }
    }

    private void Sprinting()
    {

        //Make toad able to sprint, slighty increasing his movement speed when a set button is being held down (B | A | X)
        if(_inputAmount> 0 && ( Input.GetButton("X")|| Input.GetButton("A") || Input.GetButton("B")))
        {
            //Slightly increase movement speed
            _moveSpeed = Mathf.Lerp(_moveSpeed, _maxMoveSpeed, Time.deltaTime*2);
        }
        else if(!Input.GetButton("X") || !Input.GetButton("A") || !Input.GetButton("B"))
        {
            //Set back to normal speed
            _moveSpeed = Mathf.Lerp(_moveSpeed, _baseMoveSpeed, Time.deltaTime*3);
        }
    }
   

    private void PauseCharacterController() {
        _isPaused = true;
    }

    private void ContinueCharacterController() {
        _isPaused = false;
    }
}
