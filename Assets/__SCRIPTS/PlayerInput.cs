using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Avatar;

public class PlayerInput : MonoBehaviour {

	public bool button_A_down = false;
	public bool button_B_down = false;
	public bool button_X_down = false;
	public bool button_Y_down = false;

	public bool isGrounded;

	public float left_trigger;
	public float right_trigger;
	public float left_grip;
	public float right_grip;

	public Vector2 leftStick;
	public Vector2 rightStick;

	public float currentSpeed = 12.0f;
	public float forwardSpeed = 0.0f;
	public float gravityStrength = 3.0f;
	public float gravity = 6.0f;
	private float invertGrav;
	private float forceY = 0.0f;
	public float jumpHeight = 18.0f;
	public float airTime = 1.6f;

	private Vector3 moveDirection = Vector3.zero;

	public CharacterController controller;

	// Use this for initialization
	void Start () {
		invertGrav = gravityStrength * airTime;
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		GetTouchInput();
		isGrounded = controller.isGrounded;
	}

	void FixedUpdate(){
		//Controls Player Movement based on Input
		AddMomentum();
	}

	public void GetTouchInput(){
		if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Space)){
			button_A_down = true;
		}
		if (OVRInput.GetUp(OVRInput.Button.One) || Input.GetKeyUp(KeyCode.Space)){
			button_A_down = false;
		}

		if (OVRInput.GetDown(OVRInput.Button.Two)){
			button_B_down = true;
		}
		if (OVRInput.GetUp(OVRInput.Button.Two)){
			button_B_down = false;
		}

		if (OVRInput.GetDown(OVRInput.Button.Three)){
			button_X_down = true;
		}
		if (OVRInput.GetUp(OVRInput.Button.Three)){
			button_X_down = false;
		}

		if (OVRInput.GetDown(OVRInput.Button.Four)){
			button_Y_down = true;
		}
		if (OVRInput.GetUp(OVRInput.Button.Four)){

			button_Y_down = false;
		}

		left_trigger = (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger));
		left_grip = (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger));

		right_trigger = (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger));
		right_grip = (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger));

		leftStick = (OVRInput.Get(OVRInput.RawAxis2D.LThumbstick));
		rightStick = (OVRInput.Get(OVRInput.RawAxis2D.RThumbstick));

		forwardSpeed = leftStick.y;
	}


	public void AddMomentum(){
		//If player is Grounded:
		if(controller.isGrounded){
			//Move in these directions:
			moveDirection = new Vector3(leftStick.x,0,forwardSpeed);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= currentSpeed;

			//Set ForceY to 0 since Grounded
			forceY = 0;

			//invertGrav is also reset based on gravity
			invertGrav = gravity * airTime;

			//If Jump button is pressed while Grounded
			if(button_A_down){
				//make forceY equal to jumpHeight
				forceY = jumpHeight;
			}

		}
		//If player is NOT Grounded:
		else if(!controller.isGrounded){
			moveDirection.x = leftStick.x * forwardSpeed * currentSpeed;
			moveDirection.z = forwardSpeed * currentSpeed;
		}

		//if jump button is down and forceY is not 0, means player is jumping, add invertGrav and decrease so the jump is curved
		if(button_A_down && forceY != 0){
			invertGrav -= Time.fixedDeltaTime;
			forceY += invertGrav*Time.fixedDeltaTime;
		}

		//When not jumping or grounded, apply gravity:
		forceY -= gravity*Time.fixedDeltaTime*gravityStrength;
		moveDirection.y = forceY;

		//Then move player accordingly
		controller.Move(moveDirection * Time.fixedDeltaTime);

		//If Player is running fast for a while, run even faster
	}
}
