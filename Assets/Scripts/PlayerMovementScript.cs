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
	public CapsuleCollider col;

	[Header("Settings")]
	public float sensitivity;
	public float forwardSpeed;
	public float backwardSpeed;
	public float jumpingForce;

	//cliff mini game
	public float ledgeSpeed;
	public bool isOnLedge = false;
	public ProgressBar miniGame;

	//Private variables
	private bool grounded = false;
	private bool stickToWall = false;
	private float radius;
	private float landingDistance;

	private float wallRunDistance;
	private float runningTime = 0.0f;
	private bool startRunning = false;

	//emum States
	public ControlState curState;

	void Start ()
	{
		//Get components required in script
		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();

		//Setup
		radius = col.radius;
		landingDistance = col.bounds.extents.y;
		wallRunDistance = .8f;

		//current State
		curState = ControlState.NORMAL;
	}

	void FixedUpdate()
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
			Debug.Log(string.Format("{0}, {1}", CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")));
		}
	}

	void Update()
	{
		if (curState == ControlState.NORMAL || curState == ControlState.JUMPING || curState == ControlState.WALLRUNNING)
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

				//reset here
				curState = ControlState.NORMAL;
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

		CheckWallRunning();

		//make sure that the jump button is not calling on both functions
		if (curState == ControlState.WALLRUNNING)
		{
			WallRunning();

		}
		else if (curState == ControlState.NORMAL)
		{
			//When player hits the jump button
			if(CrossPlatformInputManager.GetButtonDown("Jump"))
			{
				if(grounded)
				{
					//Add force to boost the player upwards
					rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);

					//Lock the state here (reset in the code above ^^^^)
					curState = ControlState.JUMPING;
				}
				grounded = false;
			}

			//Just in case running finish early
			startRunning = false;
			runningTime = 0.0f;
			rb.useGravity = true;
		}

		if (isOnLedge) 
		{
			rb.constraints = RigidbodyConstraints.FreezeAll;
			if (miniGame.miniGameComplete == true)
			{
				rb.AddForce (Vector3.up * jumpingForce, ForceMode.Impulse);
				rb.constraints = RigidbodyConstraints.FreezeRotation;
//				rb.constraints = ~RigidbodyConstraints.FreezeAll;
				isOnLedge = false;
				miniGame.miniGameComplete = false;
				Debug.Log(miniGame.miniGameComplete);
			}

			else if(miniGame.letGo == true)
			{
				//rb.AddForce (Vector3.back * jumpingForce, ForceMode.Impulse);
				//rb.constraints = ~RigidbodyConstraints.FreezeAll;
				rb.constraints = ~RigidbodyConstraints.None;
				miniGame.letGo = false;
				isOnLedge = false;
			}
			/*if (CrossPlatformInputManager.GetAxis ("Horizontal") < 0) 
			{
				transform.Translate (Vector3.left * ledgeSpeed * Time.deltaTime);
			} 
			else if (CrossPlatformInputManager.GetAxis ("Horizontal") > 0) 
			{
				transform.Translate (Vector3.right * ledgeSpeed * Time.deltaTime);
			}*/
		} 
		else 
		{
			rb.constraints = RigidbodyConstraints.FreezeRotation;
		}
	}

	void CheckWallRunning()
	{
		RaycastHit wallHit;
		//using transfromDirection for the left and right (follow playerObject, not scene)
		Ray leftSide = new Ray (transform.position,transform.TransformDirection(Vector3.left));
		Ray rightSide = new Ray (transform.position,transform.TransformDirection(Vector3.right));

		if (Physics.Raycast(leftSide,out wallHit, wallRunDistance))
		{
			if(wallHit.collider.tag == "Environment" )
			{
				stickToWall = true;
				curState = ControlState.WALLRUNNING;
			}
		}
		else if (Physics.Raycast(rightSide,out wallHit, wallRunDistance))
		{
			if (wallHit.collider.tag == "Environment")
			{
				stickToWall = true;
				curState = ControlState.WALLRUNNING;
			}
		}
		else if (stickToWall)
		{
			stickToWall = false;
			curState = ControlState.NORMAL;
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
		if (startRunning && stickToWall)
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
