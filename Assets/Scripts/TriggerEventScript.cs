using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerEventScript : MonoBehaviour {

	public GameObject triggerEventCanvas;
	public Transform eventObjectToFocusOn;
	public Text timerTextBox;
	private Quaternion previousCameraRotation;
	private float previousFOV;

	public bool timerStarted = false;
	public bool eventDone = false; // Can Remove If This Is Repeatable Event
	public bool animDone = false;

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
		if(timerStarted)
		{
			if(!animDone)
			{
				Vector3 targetDir = eventObjectToFocusOn.position - Camera.main.transform.position;
				float step = 5* Time.deltaTime;
				Vector3 newDir = Vector3.RotateTowards(Camera.main.transform.forward, targetDir, step, 0.0F);
				Camera.main.transform.rotation = Quaternion.LookRotation(newDir);
				//Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.FromToRotation(eventObjectToFocusOn.position, Camera.main.transform.position), 2* Time.deltaTime);
				if(Camera.main.fieldOfView > cameraFieldOfView) Camera.main.fieldOfView -= 20 * Time.deltaTime;
				else
				{
					Camera.main.transform.rotation = Quaternion.LookRotation(targetDir);
					animDone = true;
				}
			}
			else
			{
				triggerEventCanvas.SetActive(true);
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
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if(eventDone != true) // Can Remove If This Is Repeatable Event
			{
				previousCameraRotation = Camera.main.transform.rotation;
				previousFOV = Camera.main.fieldOfView;
				//Time.timeScale = 0.0f;
				timerStarted = true;
				animDone = false;
			}
		}
	}


	public void Option1()
	{
		timerStarted = false;
		timeLeft = realTimer; // For Resetting Timer To 10.0f
		triggerEventCanvas.SetActive(false);
		//Time.timeScale = 1.0f;
		Camera.main.transform.rotation = previousCameraRotation;
		Camera.main.fieldOfView = previousFOV;
		eventDone = true; // Can Remove If This Is Repeatable Event
	}


	public void Option2()
	{
		timerStarted = false;
		timeLeft = realTimer; // For Resetting Timer To 10.0f
		triggerEventCanvas.SetActive(false);
		//Time.timeScale = 1.0f;
		Camera.main.transform.rotation = previousCameraRotation;
		Camera.main.fieldOfView = previousFOV;
		eventDone = true; // Can Remove If This Is Repeatable Event
	}
}
