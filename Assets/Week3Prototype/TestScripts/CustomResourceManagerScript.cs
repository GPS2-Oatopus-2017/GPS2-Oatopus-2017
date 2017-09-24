﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomResourceManagerScript : MonoBehaviour
{

	//instantiate all gameobjects here
	void Start ()
	{
		string path = "TestPrefabs/SpawnPoolManager";
		GameObject gamePath = Resources.Load (path) as GameObject;
		GameObject go = Instantiate (gamePath) as GameObject;
		go.name = "SpawnPoolManager";
	}

	void Update ()
	{
		
	}
}
