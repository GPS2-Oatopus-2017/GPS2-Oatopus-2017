using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public enum ControlState
{
	NORMAL,
	JUMPING,
	WALLRUNNING
}

//Summary: Take control of the player's movement using the input data.
public class PlayerMovementScript : MonoBehaviour
{
	//Public variables
	[Header("Components")]
	public Rigidbody rb;
	public Collider col;

	[Header("Settings")]
	public float sensitivity;
	public float forwardSpeed;
	public float backwardSpeed;
	public float jumpingForce;

	//Private variables
	private bool grounded = false;
	private bool stickLeft = false;
	private bool stickRight = false;
	private float landingDistance;
	private float wallRunDistance;

	//emum States
	public ControlState curState;

	void Start ()
	{
		//Get components required in script
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();

		//Setup
		landingDistance = col.bounds.extents.y;
		wallRunDistance = 1.0f;

		//current State
		curState = ControlState.NORMAL;
	}

	void Update()
	{
		//Get direaction of where the player is moving
		float moveDir = CrossPlatformInputManager.GetAxis("Vertical");
		float moveSpeed = 0;

		//Let the player move at NORMAL & JUMPING, but not WALLRUNNING
		if (curState == ControlState.NORMAL || curState == ControlState.JUMPING)
		{
			//Use different speed for moving forward or moving backwards
			if(moveDir > 0)
			{
				moveSpeed = moveDir * forwardSpeed;
			}
			else if(moveDir < 0)
			{
				moveSpeed = moveDir * backwardSpeed;
			}
			//Move the player according to the speed calculated
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);

			//Debug for looking the value of both axes
			//		Debug.Log(string.Format("{0}, {1}", CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")));

			//Calculate new angle when turned left or right
			float newRot = transform.rotation.eulerAngles.y + (CrossPlatformInputManager.GetAxis("Horizontal") * sensitivity * Time.deltaTime);
			//Apply new angle to the gameobject
			transform.rotation = Quaternion.Euler(0, newRot, 0);
		}

		//Raycasting
		RaycastHit hit;
		Ray landingRay = new Ray(transform.position, Vector3.down);

		if(Physics.Raycast(landingRay, out hit, landingDistance))
		{
			//If gameobjects with tag "Environment" is hit, it's grounded.
			if(hit.collider.tag == "Environment")
			{
				grounded = true;

				//reset here
				curState = ControlState.NORMAL;
			}
			else
			{
				grounded = false;
			}
		}

		//When player hits the jump button
		if(grounded && CrossPlatformInputManager.GetButtonDown("Jump") && curState == ControlState.NORMAL)
		{
			//Add force to boost the player upwards
			rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
			grounded = false;

			//Lock the state here (reset in the code above ^^^^)
			curState = ControlState.JUMPING;
		}
			

	}
		

	void WallRunning()
	{
		RaycastHit leftHit, rightHit;

		Ray leftSide = new Ray (transform.position,transform.TransformDirection(Vector3.left));
		Ray rightSide = new Ray (transform.position,transform.TransformDirection(Vector3.right));

		if (Physics.Raycast(leftSide,out leftHit, wallRunDistance))
		{
			if(leftHit.collider.tag == "Environment")
			{
				
				stickLeft = true;
			}
			else
			{
				stickLeft = false;
			}
		}
		else if (Physics.Raycast(rightSide,out rightHit, wallRunDistance))
		{
			if (rightHit.collider.tag == "Environment")
			{
				stickRight = true;
			}
			else
			{
				stickRight = false;
			}
		}

		if (grounded && stickLeft && CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			rb.useGravity = false;
		}
		else if (grounded && stickRight && CrossPlatformInputManager.GetButtonDown("Jump"))
		{

		}

	}
}
