using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public enum ControlState
{
	NORMAL,
	JUMPING,
	WALLRUNNING,
	LOCKCONTROL
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
	private bool stickToWall = false;
	private float landingDistance;
	private float wallRunDistance;
	public float runningTime = 0.0f;
	private bool startRunning = false;

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


		//Let the player move at NORMAL, JUMPING & WALLRUNNING, but not LOCKCONTROL
		if (curState == ControlState.NORMAL || curState == ControlState.JUMPING || curState == ControlState.WALLRUNNING)
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

		//Same raycasing as above
		CheckWallRunning();

		//make sure that the jump button is not calling on both functions
		if (curState == ControlState.WALLRUNNING)
		{
			WallRunning();
		}
		else if (curState == ControlState.NORMAL)
		{
			//When player hits the jump button
			if(grounded && CrossPlatformInputManager.GetButtonDown("Jump"))
			{
				//Add force to boost the player upwards
				rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
				grounded = false;

				//Lock the state here (reset in the code above ^^^^)
				curState = ControlState.JUMPING;
			}

			//Just in case running finish early
			startRunning = false;
			runningTime = 0.0f;
			rb.useGravity = true;
		}

			
	

	}
		
	void CheckWallRunning()
	{
		RaycastHit leftHit, rightHit;

		//using transfromDirection for the left and right (follow playerObject, not scene)
		Ray leftSide = new Ray (transform.position,transform.TransformDirection(Vector3.left));
		Ray rightSide = new Ray (transform.position,transform.TransformDirection(Vector3.right));

		if (Physics.Raycast(leftSide,out leftHit, wallRunDistance))
		{
			if(leftHit.collider.tag == "Environment" )
			{
				stickToWall = true;
				curState = ControlState.WALLRUNNING;
			}
			else
			{
				stickToWall = false;
			}
		}
		else if (Physics.Raycast(rightSide,out rightHit, wallRunDistance))
		{
			if (rightHit.collider.tag == "Environment")
			{
				stickToWall = true;
				curState = ControlState.WALLRUNNING;
			}
			else
			{
				stickToWall = false;
			}
		}
	}

	void WallRunning()
	{
		//Run check
		if (grounded && stickToWall && curState == ControlState.WALLRUNNING && CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			startRunning = true;
		}

		//Running Sequence
		if (startRunning)
		{
			runningTime += Time.deltaTime;
			if (runningTime <= 1.5f)
			{
				rb.useGravity = false;
				transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.Self);
				//rb.AddForce(Vector3.up * jumpingForce/5, ForceMode.Impulse);
			}
			else if (runningTime > 1.5f)
			{ 
				startRunning = false;
				runningTime = 0.0f;
				rb.useGravity = true;
				curState = ControlState.NORMAL;
			}
		}

	}
}
