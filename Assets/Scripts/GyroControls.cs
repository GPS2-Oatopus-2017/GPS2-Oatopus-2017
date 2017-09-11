using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControls : MonoBehaviour {

	public bool gyroEnabled;
	private Gyroscope gyro;

	private GameObject camContainer;
	private Quaternion defaultRot;

	// Use this for initialization
	void Start ()
	{
		camContainer = new GameObject("Camera Container");
		camContainer.transform.SetParent(transform.parent);
		transform.SetParent(camContainer.transform);

		gyroEnabled = EnableGyro();
	}

	bool EnableGyro()
	{
		if(SystemInfo.supportsGyroscope)
		{
			Debug.Log("WTF?");
			gyro = Input.gyro;
			gyro.enabled = true;

			camContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
			defaultRot = new Quaternion(0, 0, 1, 0);

			return true;
		}

		return false;
	}

	// Update is called once per frame
	void Update ()
	{
		if(gyroEnabled)
		{
			transform.localRotation = gyro.attitude * defaultRot;
		}
	}
}
