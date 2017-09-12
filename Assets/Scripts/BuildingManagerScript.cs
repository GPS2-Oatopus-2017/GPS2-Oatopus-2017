using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManagerScript : MonoBehaviour {

	public static BuildingManagerScript instance;
	public GameObject[] buildings;

	private Material defaultMat;
	public Material warningMat;
	public Material positiveMat;
 
	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		buildings = GameObject.FindGameObjectsWithTag("Building");
		defaultMat = buildings[0].GetComponent<MeshRenderer>().material;
	}

	public void PositiveColor(GameObject building)
	{
		Debug.Log(building);
		for(int i = 0; i < buildings.Length; i++)
		{
			if(buildings[i].name == building.name)
				buildings[i].GetComponent<MeshRenderer>().material = positiveMat;
		}
	}

	public void WarningColor(GameObject building)
	{
		Debug.Log(building);
		for(int i = 0; i < buildings.Length; i++)
		{
			if(buildings[i].name == building.name)
				buildings[i].GetComponent<MeshRenderer>().material = warningMat;
		}
	}

	public void DefaultColor(GameObject building)
	{
		for(int i = 0; i < buildings.Length; i++)
		{
			if(buildings[i].name == building.name)
				buildings[i].GetComponent<MeshRenderer>().material = defaultMat;
		}
	}
}
