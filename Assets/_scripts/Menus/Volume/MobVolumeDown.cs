﻿using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class MobVolumeDown : MonoBehaviour {
	
	public Text current;
	VRTK_ControllerActions controllerActions;
	bool clickable;
	float timeStamp;
	int currentVolume;

	float ConvertNumber(int feed){
		float newNumber = feed * 0.1f;
		return newNumber;
	}

	void Update()
	{
		if(clickable == false)
		{
			if (Time.time > timeStamp + 0.5f) 
			{
				clickable = true;
			}
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.name == "Head" || coll.name=="Ring") {
			controllerActions = coll.GetComponentInParent<VRTK_ControllerActions> ();
			controllerActions.TriggerHapticPulse (1.0f);
		}
	}

	void OnTriggerStay(Collider coll)
	{
		if (coll.GetComponentInParent<VRTK_ControllerEvents> ().triggerPressed == true && clickable == true) 
		{
			clickable = false;
			timeStamp = Time.time;
			currentVolume = PlayerPrefs.GetInt ("towers and mobs");
			if (currentVolume >0) {
				currentVolume -= 1;
				PlayerPrefs.SetInt ("towers and mobs", currentVolume);
				Display (currentVolume);
			}
		}
	}

	void Display(int volume){
		current.text = volume.ToString ();
	}
}
