﻿using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class MusicUp : MonoBehaviour {
	
	public Text current;
	VRTK_ControllerActions controllerActions;
	bool clickable;
	float timeStamp;
	int currentVolume;

	void Start(){
		if(PlayerPrefs.HasKey("music")==false){
			PlayerPrefs.SetInt ("music", 3);
		}
		currentVolume =PlayerPrefs.GetInt ("music", 3);
		Display (currentVolume);
	}

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
			currentVolume = PlayerPrefs.GetInt ("music");
			if (currentVolume < 10f) {
				currentVolume += 1;
				PlayerPrefs.SetInt ("music", currentVolume);
				Display (currentVolume);
			}
		}
	}

	void Display(int volume){
		current.text = volume.ToString ();
	}
}
