using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeCollisionScript : MonoBehaviour
{

	PlayerMovementScript player;

	public GameObject miniGame;

	public Transform climbPoint;

	public float buffer;

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
			player.transform.eulerAngles = new Vector3 (transform.eulerAngles.x, other.transform.eulerAngles.y, transform.eulerAngles.z);
			player.isOnLedge = true;
			miniGame.SetActive(true);	

//			if (this.transform.position.y + buffer < climbPoint.position.y)
//			{
//				this.transform.Translate(Vector3.up * Time.deltaTime);
//
//				if(this.transform.position.y + buffer == climbPoint.position.y)
//				{
//					miniGame.SetActive(true);	
//				}
//			}
//
//			else if (this.transform.position.y + buffer > climbPoint.position.y)
//			{
//				this.transform.Translate(Vector3.down * Time.deltaTime);
//
//				if(this.transform.position.y + buffer == climbPoint.position.y)
//				{
//					miniGame.SetActive(true);	
//				}
//			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		player.isOnLedge = false;
	}
}
