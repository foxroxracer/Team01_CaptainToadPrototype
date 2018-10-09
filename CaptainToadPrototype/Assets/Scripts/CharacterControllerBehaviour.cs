using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBehaviour : MonoBehaviour {
    public bool CanFall = true;
    public bool ClimbingLadder = false;
    public bool LeavingLadder = false;

    public Transform CameraTransform;

    public Transform CurrentObjectTransform;


    private CharacterController _charCTRL;


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


	void Start () {
        _charCTRL = GetComponent<CharacterController>();

        _moveSpeed = _baseMoveSpeed;
	}
	

	void Update () {
        MovementCalculation();
        Sprinting();
        ClimbLadder();

        //Debug.Log(_charCTRL.isGrounded);
	}

    private void FixedUpdate()
    {
        if (ClimbingLadder == false)
        {
            WalkDownSlope();
        }
        ApplyGravity();


        //Allow player to move in the X and Z direction
        //Stop player from moving when falling down platform
        if (_downWardsForce.y >=-1.5f)
        {
        _charCTRL.Move(_moveDirection * _moveSpeed * _inputAmount  );
        }

        //Downwards Gravity
        if (CanFall == true)
        { 
        _charCTRL.SimpleMove(_downWardsForce);
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

    public void ClimbLadder()
    {
        if(ClimbingLadder == true)
        {
            //Change toad forward to face the ladder when getting on (Change the Y of the forward
            this.gameObject.transform.forward = Vector3.Lerp(this.gameObject.transform.forward, CurrentObjectTransform.forward * -1,Time.deltaTime*4);
            //Stop toad from falling down when downwards force gets too big
            CanFall = false;


            //Add an upwards force that toad will be using to climb the ladder, this move will only look at the vertical input to climb
            //Because the downwards force is increasing while toad is climbing the ladder, toad won't be able to move left/right/forwards/backwards
            _charCTRL.Move(Vector3.up * _verticalInput*Time.deltaTime*2.5f);

            if (LeavingLadder == true)
            {
                //When leaving the ladder, toad will get pushed forward to land on the ground
                //Push Toad forward according to his forward
                _charCTRL.Move(this.gameObject.transform.forward * _moveSpeed * Time.deltaTime*20);

                //Make sure Toad can fall again
                CanFall = true;

                //When toad is grounded, ladder climbing sequence will be completed
                if (_charCTRL.isGrounded)
                {
                    ClimbingLadder = false;
                    LeavingLadder = false;
                }
            }

        }


    }
}
