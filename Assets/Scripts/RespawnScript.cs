using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnScript : MonoBehaviour
{

	// Use this for initialization
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			SceneManager.LoadScene ("RefinePrototype");
		}
	}

	void Start ()
	{
		
	}
}
