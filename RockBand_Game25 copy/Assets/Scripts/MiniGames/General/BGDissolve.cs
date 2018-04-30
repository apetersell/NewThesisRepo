using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BGDissolve : MonoBehaviour {

	public SpriteRenderer day;
	GlobalManager gm;
	public static float shiftTime = 1;

	// Use this for initialization
	void Start () 
	{
		gm = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		if (gm.night) {
			day.color = Color.clear;
		} else {
			day.color = Color.white;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void timeShift (bool night)
	{
		if (night) {
			day.DOColor (Color.clear, shiftTime);
		} else {
			day.DOColor (Color.white, shiftTime);
		}
	}
}
