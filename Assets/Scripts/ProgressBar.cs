using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	PlayerMovementScript player;

	[Header("Settings")]
	public Slider sliderFill;
	public float maxTime = 10f;
	public float startingTime = 5f;
	public float minTime = 0f;
	public float currTime;
	public float increaseValue; 
	public bool miniGameComplete; //Successfully completed minigame
	public bool letGo; //Let go of the ledge

	// Use this for initialization
	void Start () {
		//sliderFill = this.GetComponent<Slider>();
		player = FindObjectOfType<PlayerMovementScript> ();
		currTime = startingTime;
		miniGameComplete = false;
		letGo = false;
	}

	void OnEnable()
	{
		currTime = startingTime;
	}

	// Update is called once per frame
	void Update () {
		if(currTime > 0f)
		{
			currTime -= Time.deltaTime;
			sliderFill.value = currTime / maxTime;
		}

		if(currTime > maxTime)
		{
			miniGameComplete = true;
			gameObject.SetActive(false);
		}

		if(currTime <= minTime)
		{
			letGo = true;
			gameObject.SetActive(false);
		}
	}

	public void IncreaseValue()
	{
		currTime += increaseValue;
	}
}
