using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRun : MonoBehaviour
{

	public float movementSpeed = 5f;
	public GameObject node;

	void Awake ()
	{
		//node = FindObjectOfType<GameObject> ();
	}

	void Start ()
	{
		
	}

	void Update ()
	{
		LookAtAndMove ();
	}

	// Character will look at prebuilt node on x-axis and runs towards it
	void LookAtAndMove ()
	{
		if (node.tag == "Node") {
			this.transform.LookAt (node.transform.position);
		}
		this.transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime, Space.Self);
	}
}
