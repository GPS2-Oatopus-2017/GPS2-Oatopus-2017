using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeScript : MonoBehaviour {

	public GameObject building;
	public GameObject frontLedge;

	private float range;
	private float warningRange;
	public float distance;

	void Start()
	{
		building = frontLedge.transform.parent.gameObject;
		range = GameManagerScript.instance.range;
		warningRange = GameManagerScript.instance.warningRange;
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			distance = Vector3.Distance(other.transform.position, frontLedge.transform.position);

			if(distance <= range)
			{
				BuildingManagerScript.instance.PositiveColor(building);
			}
			else if(distance > range && distance <= warningRange)
			{
				BuildingManagerScript.instance.WarningColor(building);
			}
			else if(distance > warningRange)
			{
				BuildingManagerScript.instance.DefaultColor(building);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			BuildingManagerScript.instance.DefaultColor(building);
		}
	}
}
