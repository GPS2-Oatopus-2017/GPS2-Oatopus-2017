using System.Collections;
using System.Collections.Generic;
using UnityEditor.AI;
using UnityEngine.AI;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PathFinderManager : MonoBehaviour {

	public static PathFinderManager Instance;
	public GameObject player;
	public Transform goal;
	public bool startMove;
	public float remainingPos;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		Instance = this;
		agent = player.GetComponent<NavMeshAgent>();
		agent.stoppingDistance = 3;
	}
	
	// Update is called once per frame
	void Update () {
		if(startMove)
		{
			agent.destination = goal.position;
			agent.isStopped = false;
			if(CrossPlatformInputManager.GetAxis("Vertical") != 0 || CrossPlatformInputManager.GetAxis("Horizontal") != 0)
			{
				StopMoving();
			}
			else if(!agent.pathPending)
			{
				remainingPos = Vector3.Distance(player.transform.position,goal.transform.position);

				if(remainingPos <= agent.stoppingDistance)
				{
					StopMoving();	
				}
			}
		}
	}

	void StopMoving()
	{
		agent.isStopped = true;
		goal = null;
		startMove = false;	
	}
}
