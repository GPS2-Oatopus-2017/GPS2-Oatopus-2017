using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PathFinderManager : MonoBehaviour {

	public static PathFinderManager Instance;
	public GameObject player;
	public GameObject confirmWindow;
	public GameObject goal;
	public InspectDistanceScript inspectObjects;
	public bool startMove;
	public float remainingPos;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		confirmWindow.SetActive(false);
		Instance = this;
		agent = player.GetComponent<NavMeshAgent>();
		agent.enabled = false;
		agent.stoppingDistance = 3;
	}
	
	// Update is called once per frame
	void Update () {
		if(startMove)
		{
			agent.enabled = true;
			agent.destination = goal.transform.position;
			inspectObjects = goal.GetComponent<InspectDistanceScript>();
			agent.isStopped = false;
			if(CrossPlatformInputManager.GetAxis("Vertical") != 0 || CrossPlatformInputManager.GetAxis("Horizontal") != 0 || CrossPlatformInputManager.GetButtonDown("Jump"))
			{
				StopMoving();
				inspectObjects = null;
			}
			else if(!agent.pathPending)
			{
				remainingPos = Vector3.Distance(player.transform.position,goal.transform.position);
				if(remainingPos <= agent.stoppingDistance)
				{
					//player.transform.LookAt(goal.transform.position);
					StopMoving();
					confirmWindow.SetActive(true);
					Debug.Log("on");
				}
			}
		}
	}

	void StopMoving()
	{
		agent.isStopped = true;
		agent.enabled = false;
		//goal = null;
		startMove = false;	
	}

	public void Inspected()
	{
		inspectObjects.isInspected = true;
		goal = null;
		inspectObjects = null;
	}
}
