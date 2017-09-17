using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour {

	[Header("Components")]
	public Rigidbody rb;
	public CapsuleCollider col;
	public Animator anim;
	public RigidbodyFirstPersonController controlScript;
	public PlayerAnimatorScript animScript;

	void Awake () 
	{
		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();
		anim = GetComponent<Animator> ();
		controlScript = GetComponent<RigidbodyFirstPersonController> ();
		animScript = GetComponent<PlayerAnimatorScript> ();

		if (animScript != null) {
			animScript.self = this;
		}
	}


	void Start()
	{
		
	}


	void Update () 
	{
		
	}
}
