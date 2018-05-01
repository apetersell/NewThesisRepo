using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboFlash : MonoBehaviour 
{
	Text text;
	Color lerpTo;
	public static float lerpSpeed = 10;
	Color lerpingColor;
	public Color light;
	public Color dark; 
	public Color [] aigLights;
	public Color[] aigDarks;
	public bool active;
	public static float activeTime = 2;
	public float timer;
	string displayText;
	public bool aig;
	public ScoreManager sm;
	int aigFlashIndex;
	public GameObject Friend;
	bool friendFlashed;
	GlobalManager globe;

	// Use this for initialization
	void Start () 
	{
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		if (active) {
			text.color = lerpingColor = Color.Lerp (light, dark, Mathf.PingPong (Time.time * lerpSpeed, 1));  
			timer += Time.deltaTime;
		} else {
			text.color = Color.clear;
		}

		if (timer >= activeTime) 
		{
			active = false;
			timer = 0;
		}

		text.text = "x" + displayText;

		if (aig) {
			aigControl ();
		} else {
			friendControl ();
		}
	}

	public void doFlash(int index, float multiplier)
	{
		active = true;
		displayText = multiplier.ToString ();
		if (index != 0) 
		{
			dark = aigDarks [index - 1];
			light = aigLights [index - 1];
		}
	}

	void aigControl()
	{
		if (sm.inARow == 0) 
		{
			aigFlashIndex = 0;
		}
		if (sm.inARow >= ScoreManager.firstMulti && aigFlashIndex == 0) 
		{
			doFlash (aigFlashIndex, 2f);
			aigFlashIndex = 1; 
		}
		if (sm.inARow >= ScoreManager.secondMulti && aigFlashIndex == 1) 
		{
			doFlash (aigFlashIndex, 2.5f);
			aigFlashIndex = 2; 
		}
		if (sm.inARow >= ScoreManager.thirdMulti && aigFlashIndex == 2) 
		{
			doFlash (aigFlashIndex, 3);
			aigFlashIndex = 3; 
		}
	}

	void friendControl()
	{
		bool here = false;
		float friendMulti = 0;
		if (Friend.gameObject.name == "obj_JP") {
			here = globe.JPPresent;
			friendMulti = ScoreManager.multi (globe.jPeRelationship);
		} else {
			here = globe.LeePresent;
			friendMulti = ScoreManager.multi (globe.leeRelationship);
		}

		if (here && !friendFlashed) 
		{
			doFlash (0, friendMulti);
			friendFlashed = true;
		}
			
		if (!here) 
		{
			friendFlashed = false;
			active = false;
			timer = 0;
		}
	}
}
