using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FanBarColor : MonoBehaviour {

	Image img;
	Color lerpingColor;

	// Use this for initialization
	void Start () 
	{
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		lerpingColor = lerpingColor = Color.Lerp (BarScript.barLight, BarScript.barDark, Mathf.PingPong (Time.time * BarScript.lerpSpeed, 1));  
		colorHandleFanBar ();
	}

	void colorHandleFanBar()
	{
		if (img.fillAmount >= (StoryManager.fanPassing1 / StoryManager.fanFlyingColors1) && img.fillAmount < 1) {
			img.color = BarScript.barFanPassed;
		} else if (img.fillAmount >= 1) {
			img.color = lerpingColor;
		} else {
			img.color = BarScript.barBlue;
		}
	}
}
