using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateDisplay : MonoBehaviour 
{
	public bool schedule;
	string display;
	string first;
	string second;
	GlobalManager globe;

	void Start ()
	{
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
	}

	void Update () 
	{
		first = GameObject.Find ("GlobalStats").GetComponent<CalendarTracker> ().displayDate;
		second = GameObject.Find ("GlobalStats").GetComponent<CalendarTracker> ().oneWeeklater;
		if (schedule) {
			display = first + " - " + second;
		} else {
			if (globe.performance && globe.myState == PlayerState.miniGaming) {
				display = "Concert";
			} else {
				display = first;
			}
		}
		GetComponent<Text>().text = display;
	}
}
