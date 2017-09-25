using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveScript : MonoBehaviour
{

	//object move forward slowly
	//will use a static spawn box instead of one attached to player
	//feels better that's for sure
	//this thing will move depending on speed???

	public float objectSpeed = 5f;

	void Start ()
	{
		
	}

	void Update ()
	{
		this.transform.Translate (Vector3.back * objectSpeed * Time.deltaTime, Space.Self); 
	}


}
