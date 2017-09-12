using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerEventScript : MonoBehaviour {

	public GameObject triggerEventCanvas;
	public Text timerTextBox;

	public bool timerStarted = false;
	public bool eventDone = false; // Can Remove If This Is Repeatable Event

	public int cameraFieldOfView = 20;

	public float timeLeft = 10.0f;
	private float realTimer;

	void Start()
	{
		timerTextBox.gameObject.GetComponent<Text>();
		realTimer = timeLeft;
	}


	void Update()
	{
		if(timerStarted == true)
		{
			timerTextBox.text = "Time Left : " + (int)timeLeft;
			timeLeft -= Time.unscaledDeltaTime;

			if(timeLeft <= 0)
			{
				int randNum = Random.Range(0,1);

				if(randNum == 0)
				{
					Option1();
				}

				else
				{
					Option2();
				}
			}
		}
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if(eventDone != true)
			{
				triggerEventCanvas.SetActive(true);
				Camera.main.fieldOfView -= cameraFieldOfView;
				Time.timeScale = 0.0f;
				timerStarted = true;
			}
		}
	}


	public void Option1()
	{
		timerStarted = false;
		timeLeft = realTimer; // For Resetting Timer To 10.0f
		triggerEventCanvas.SetActive(false);
		Time.timeScale = 1.0f;
		Camera.main.fieldOfView += cameraFieldOfView;
		eventDone = true; // Can Remove If This Is Repeatable Event
	}


	public void Option2()
	{
		timerStarted = false;
		timeLeft = realTimer; // For Resetting Timer To 10.0f
		triggerEventCanvas.SetActive(false);
		Time.timeScale = 1.0f;
		Camera.main.fieldOfView += cameraFieldOfView;
		eventDone = true; // Can Remove If This Is Repeatable Event
	}
}
