using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public enum WindowType
{
	first = 0,
	second,
	total
}
public class CloseWindow : MonoBehaviour {
	public WindowType myType;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(myType == WindowType.first)
		{
			if
			(
					CrossPlatformInputManager.GetAxis("Vertical") != 0 ||
					CrossPlatformInputManager.GetAxis("Horizontal") != 0 ||
					CrossPlatformInputManager.GetButtonDown("Jump")
			)
			{
				this.gameObject.SetActive(false);
			}
		}
		else if(myType == WindowType.second)
		{
			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				this.gameObject.SetActive(false);
			}
		}
	}
}
