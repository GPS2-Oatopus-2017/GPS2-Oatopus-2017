using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectDistanceScript : MonoBehaviour {

	public GameObject player;
	public float range = 20f;
	public bool isInspected = false;
	float distance;
	Renderer rend;
	Color originalColor;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		originalColor = rend.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isInspected)
		{
			distance = Vector3.Distance(transform.position, player.transform.position);
			if(distance <= range)
			{
				rend.material.color = Color.blue;
				//!testing
				if(Input.GetKeyDown(KeyCode.A)) 
					{
						PathFinderManager.Instance.startMove = true;
					PathFinderManager.Instance.goal = this.gameObject;	
					}
				//!
				DetectTap();
			}
			else
			{
				PathFinderManager.Instance.confirmWindow.SetActive(false);
				rend.material.color = originalColor;
			}
		}
		else
		{
			rend.material.color = originalColor;
		}
	}

	void DetectTap()
	{
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			Ray rayCast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit castHit;
			if(Physics.Raycast(rayCast, out castHit))
			{
				if(castHit.collider.CompareTag("InspectableObjects"))
				{
					PathFinderManager.Instance.goal = castHit.collider.gameObject;
					PathFinderManager.Instance.startMove = true;
				}
			}	
		}
	}
}
