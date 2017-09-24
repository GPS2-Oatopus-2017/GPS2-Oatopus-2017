using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnObjectScript : MonoBehaviour
{

	public GameObject lobbySet;
	public GameObject pathSet;

	public float timeToSpawn = 1f;
	public float spawnTimer = 0f;

	public int random = 0;

	void Start ()
	{
		
	}

	void Update ()
	{
		if (spawnTimer > timeToSpawn) {
			if (random == 0) {
				CustomObjectPoolScript.Instance.Spawn ("Lobby", new Vector3 (this.transform.position.x, lobbySet.transform.position.y, this.transform.position.z), Quaternion.identity);
			} else if (random == 1) {
				CustomObjectPoolScript.Instance.Spawn ("PathWay1", new Vector3 (this.transform.position.x, pathSet.transform.position.y - 1, this.transform.position.z), Quaternion.identity);
			}
			//Instantiate (lobbySet, new Vector3 (this.transform.position.x, lobbySet.transform.position.y, this.transform.position.z), Quaternion.identity);
			spawnTimer = 0f;
			random = Random.Range (0, 2);
		} else {
			spawnTimer += Time.deltaTime;
		}
	}
}
