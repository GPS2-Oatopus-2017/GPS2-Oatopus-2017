using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public enum PlayerControlState
{
	Idle,
	Run,
	Jump,

	Total
}

public class PlayerControlScript : MonoBehaviour {

	public PlayerScript self;

	[Header("Settings")]
	public float forwardSpeed;
	public float backwardSpeed;
	public float acceleration;
	public float sensitivity;
	public float jumpingForce;

	//Private variables
	private bool grounded = false;
	private float radius;
	private float landingDistance;

	public PlayerControlState curState;

	void Start () 
	{
		
	}
	

	void Update () 
	{
		if (curState == PlayerControlState.Idle || curState == PlayerControlState.Run || curState == PlayerControlState.Jump)
		{
			//Calculate new angle when turned left or right
			float newRot = transform.rotation.eulerAngles.y + (CrossPlatformInputManager.GetAxis("Horizontal") * sensitivity * Time.deltaTime);

			//Apply new angle to the gameobject
			transform.rotation = Quaternion.Euler(0, newRot, 0);
		}

		//Raycasting
		RaycastHit hit;
		Ray landingRay = new Ray(transform.position, Vector3.down);

		if(Physics.SphereCast(landingRay, radius, out hit, landingDistance))
		{
			//If gameobjects with tag "Environment" is hit, it's grounded.
			if(hit.collider.tag == "Environment")
			{
				grounded = true;
			}
			else
			{
				grounded = false;
			}
		}
		else
		{
			grounded = false;
		}

		if(CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			if(grounded)
			{
				//Add force to boost the player upwards
				self.rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);

				//Lock the state here (reset in the code above ^^^^)
				curState = PlayerControlState.Jump;
			}
			grounded = false;
		}
	}


	void FixedUpdate()
	{
		//Get direaction of where the player is moving
		float moveDir = CrossPlatformInputManager.GetAxis("Vertical");
		float moveSpeed = 0;

		if(moveDir > 0)
		{
			moveSpeed = moveDir * forwardSpeed;

			if(curState == PlayerControlState.Idle)
				curState = PlayerControlState.Run;
		}
		else if(moveDir < 0)
		{
			moveSpeed = moveDir * backwardSpeed;

			if(curState == PlayerControlState.Idle)
				curState = PlayerControlState.Run;
		}



		//Let the player move at NORMAL, JUMPING & WALLRUNNING, but not LOCKCONTROL
//		if (curState == PlayerControlState.Run)
//		{
//			//Use different speed for moving forward or moving backwards
//			if(moveDir > 0)
//			{
//				moveSpeed = moveDir * forwardSpeed;
//			}
//			else if(moveDir < 0)
//			{
//				moveSpeed = moveDir * backwardSpeed;
//			}
//
//			//Move the player according to the speed calculated
//			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
//
//			//Debug for looking the value of both axes
//			Debug.Log(string.Format("{0}, {1}", CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")));
//		}
	}
}
