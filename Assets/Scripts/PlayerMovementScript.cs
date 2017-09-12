using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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
	private float radius;
	private float landingDistance;

	void Start ()
	{
		//Get components required in script
		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();

		//Setup
		radius = col.radius;
		landingDistance = col.bounds.extents.y;
	}

	void FixedUpdate()
	{
		//Get direaction of where the player is moving
		float moveDir = CrossPlatformInputManager.GetAxis("Vertical");
		float moveSpeed = 0;

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
	}

	void Update()
	{
		//Calculate new angle when turned left or right
		float newRot = transform.rotation.eulerAngles.y + (CrossPlatformInputManager.GetAxis("Horizontal") * sensitivity * Time.deltaTime);
		//Apply new angle to the gameobject
		transform.rotation = Quaternion.Euler(0, newRot, 0);
		
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
		
		//When player hits the jump button
		if(CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			if(grounded)
			{
				//Add force to boost the player upwards
				rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
			}
			grounded = false;
		}

		if (isOnLedge) 
		{
			rb.constraints = RigidbodyConstraints.FreezeAll;
			if (miniGame.miniGameComplete == true)
			{
				rb.AddForce (Vector3.up * jumpingForce, ForceMode.Impulse);
				rb.constraints = ~RigidbodyConstraints.FreezeAll;
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
			rb.constraints = ~RigidbodyConstraints.FreezeAll;
		}
	}
}
