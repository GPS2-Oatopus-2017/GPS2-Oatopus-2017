using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeCollisionScript : MonoBehaviour
{

	PlayerMovementScript player;

	void Start ()
	{
		player = FindObjectOfType<PlayerMovementScript> ();
	}

	void Update ()
	{
		
	}

	//player holds on ledge when colliding with objects with tag "Ledge"
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Ledge") {
			Debug.Log ("Touch The Ledge");
			player.isOnLedge = true;
		}
	}
}
