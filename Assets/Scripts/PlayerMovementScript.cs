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
	public Collider col;

	[Header("Settings")]
	public float sensitivity;
	public float forwardSpeed;
	public float backwardSpeed;
	public float jumpingForce;

	//Private variables
	private bool grounded = false;
	private float landingDistance;

	void Start ()
	{
		//Get components required in script
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();

		//Setup
		landingDistance = col.bounds.extents.y;
	}

	void Update()
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

		//Calculate new angle when turned left or right
		float newRot = transform.rotation.eulerAngles.y + (CrossPlatformInputManager.GetAxis("Horizontal") * sensitivity * Time.deltaTime);
		//Apply new angle to the gameobject
		transform.rotation = Quaternion.Euler(0, newRot, 0);

		//Raycasting
		RaycastHit hit;
		Ray landingRay = new Ray(transform.position, Vector3.down);

		if(Physics.Raycast(landingRay, out hit, landingDistance))
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

		//When player hits the jump button
		if(grounded && CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			//Add force to boost the player upwards
			rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
			grounded = false;
		}
	}
}
