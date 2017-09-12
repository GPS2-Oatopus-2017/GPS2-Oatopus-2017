using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	public static GameManagerScript instance;

	[Header("Jumping From Buildings")]
	public float range;
	public float warningRange;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
