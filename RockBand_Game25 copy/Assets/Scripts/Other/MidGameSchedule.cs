using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidGameSchedule : MonoBehaviour {

	GlobalManager gm;
	public Sprite[] icons;
	public static List<UnitType> dummyList = new List<UnitType>();
	public static int index;
	public Color alphadOut;
	public Color lessThanWhite;
	public static Color staticLTW;
	public List<Image> mainNodes = new List<Image> (); 
	public static ScheduleUnit currentUnit;
	public float expandedScale;
	public static Color lerpingColor;
	public float colorLerpSpeed; 
	public static float staticCLS;
	public Vector3 onScreenPos;

	// Use this for initialization
	void Start () 
	{
		staticLTW = lessThanWhite;
		staticCLS = colorLerpSpeed;
		gm = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		for (int i = 0; i < 14; i++) 
		{
			string search = (i + 1).ToString ();
			Image main = GameObject.Find (search).GetComponent<Image> ();
			mainNodes.Add (main);
		}
	}
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void makeDummySchedule()
	{
		if (transform.localPosition != onScreenPos) 
		{
			transform.localPosition = onScreenPos;
			Debug.Log ("SHIFTED");
		}
		for (int i = 0; i < dummyList.Count; i++) 
		{
			mainNodes [i].sprite = icon (dummyList [i]);
			mainNodes [i].gameObject.GetComponent<Animator> ().SetBool ("Chosen", false);
			mainNodes [i].transform.localScale = new Vector3 (1, 1, 1);
			mainNodes [i].color = lessThanWhite;
		}
		foreach (var item in mainNodes) 
		{
			if (mainNodes.IndexOf (item) < index) {
				item.gameObject.GetComponent<Animator> ().SetBool ("Chosen", false);
				item.color = alphadOut;
				item.transform.localScale = new Vector3 (1, 1, 1);
			} else if (mainNodes.IndexOf (item) >= index && mainNodes.IndexOf (item) < index + currentUnit.time) {
				item.gameObject.GetComponent<Animator> ().SetBool ("Chosen", true);
				item.transform.localScale = new Vector3 (expandedScale, expandedScale, 1);
			} else {
				item.gameObject.GetComponent<Animator> ().SetBool ("Chosen", false);
				item.color = lessThanWhite;
				item.transform.localScale = new Vector3 (1, 1, 1);
			}
		}
	}

	public Sprite icon (UnitType type)
	{
		Sprite result = null;
		switch (type) 
		{
			case UnitType.Dance:
				result = icons[0];
				break;
			case UnitType.Vocal:
				result = icons [1];
				break;
			case UnitType.PR:
				result = icons [2];
					break;
			case UnitType.Rest:
				result = icons [3];
					break;
			case UnitType.StreetModeling:
				result = icons [4];
					break;
			case UnitType.Songwriting:
				result = icons [5];
					break;
			case UnitType.TalkShow:
				result = icons [6];
					break;
		}
		return result;
	}

	public static void updateLerpColor()
	{
		lerpingColor = Color.Lerp (staticLTW, Color.white, Mathf.PingPong (Time.time * staticCLS, 1));  
	}
}
