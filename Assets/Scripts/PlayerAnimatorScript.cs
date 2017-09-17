using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
	public PlayerScript self;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		self.anim.SetBool ("Running", self.controlScript.Velocity.magnitude > float.Epsilon);
		self.anim.SetBool ("Jumping", self.controlScript.Jumping);
		self.anim.SetFloat ("VerticalVelocity", self.controlScript.Velocity.y);
	}
}
