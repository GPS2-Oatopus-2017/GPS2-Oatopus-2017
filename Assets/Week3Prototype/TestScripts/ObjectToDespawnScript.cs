using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDespawnScript : MonoBehaviour
{

	void Start ()
	{
		
	}

	void Update ()
	{
		
	}

	/*
	void OnTriggerEnter (Collider other)
	{
		//activate bool to determine if it is subject to deactivation
		if (this.tag == "Floor" && other.tag == "DespawnBox") {
			//Debug.Log ("To Despawn");
		}
	}
	*/

	void OnTriggerExit (Collider other)
	{
		if (this.tag == "Floor" && other.tag == "DespawnBox") {
			//Debug.Log ("Despawn Object");
			CustomObjectPoolScript.Instance.Despawn (this.gameObject);
		}
	}
}
