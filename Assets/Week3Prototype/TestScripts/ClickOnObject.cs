using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnObject : MonoBehaviour
{

	// A node that the player will always follow
	// When press on which place to jump using raycast, node will move to the next jumpnode and player jumps while looking at node
	// Player lands and node continues on path which the player follows
	// Should navmesh be utilized for node pathfinding?

	//Rigidbody rb;
	public float jumpForce = 5f;

	void Awake ()
	{
		//rb = GetComponent<Rigidbody> ();
	}

	void Start ()
	{
		
	}

	void Update ()
	{
		JumpTowardsObject ();
	}

	// Character will look at object that is pressed, and jump towards it
	void JumpTowardsObject ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			//this.rb.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
			transform.Translate (new Vector3 (0f, jumpForce, 0f), Space.Self);
		}
	}
}
