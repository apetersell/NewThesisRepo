using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	Image img;
	GlobalManager gm; 
	public bool stressBar;
	public bool Dance;
	public bool Vocal;
	public bool PR;
	public bool Stress;
	public bool Relationship;
	public bool JP;
	public bool Lee;
	public bool Aig;
	public bool fanBar;
	public static Color barBlue = new Color(0.42f, 0.81f, 0.96f,1);
	public static Color barDark = new Color (0.63f, 0.49f, 0, 1);
	public static Color barLight = new Color (1, 0.76f, 0, 1);
	public static Color barFanPassed = new Color (.42f, .96f, .45f);
	public static float lerpSpeed = 1;
	Color lerpingColor;

	// Use this for initialization
	void Start () 
	{
		img = GetComponent<Image> ();
		gm = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
	}
	
	// Update is called once per frame
	void Update () 
	{
		lerpingColor = lerpingColor = Color.Lerp (barLight, barDark, Mathf.PingPong (Time.time * lerpSpeed, 1));  
		if (stressBar) {
			if (Dance) {
				img.fillAmount = gm.DanceScore / StoryManager.statMeterFull;
			} else if (Vocal) {
				img.fillAmount = gm.VocalScore / StoryManager.statMeterFull;
			} else if (PR) {
				img.fillAmount = gm.PRScore / StoryManager.statMeterFull;
			}
		} else if (fanBar) {
			colorHandleFanBar ();
			if (Aig) {
				img.fillAmount = gm.AigFans / StoryManager.fanFlyingColors1;
			} else if (JP) {
				img.fillAmount = (gm.AigFans + gm.JPFans) / StoryManager.fanFlyingColors1;
			} else if (Lee) {
				img.fillAmount = gm.totalFans / StoryManager.fanFlyingColors1;
			}
		}else {
			colorHandle ();
			if (Dance) {
				img.fillAmount = gm.effectiveDance / StoryManager.statMeterFull;
			} else if (Vocal) {
				img.fillAmount = gm.effectiveVocal / StoryManager.statMeterFull;
			} else if (PR) {
				img.fillAmount = gm.effectivePR / StoryManager.statMeterFull;
			} else if (Relationship) {
				if (JP) {
					img.fillAmount = gm.jPeRelationship / 100;
				} else if (Lee) {
					img.fillAmount = gm.leeRelationship / 100;
				}
			} else if (Stress) {
				img.fillAmount = gm.Stress / StoryManager.statMeterFull;
			}
		}
	}

	void colorHandle()
	{
		if (!Stress) {
			if (img.fillAmount >= 1) {
				img.color = lerpingColor;
			} else {
				img.color = barBlue;
			}
		}
	}

	void colorHandleFanBar()
	{
		if (img.fillAmount >= (StoryManager.fanPassing1 / StoryManager.fanFlyingColors1) && img.fillAmount < 1) {
			img.color = barFanPassed;
		} else if (img.fillAmount >= 1) {
			img.color = lerpingColor;
		} else {
			img.color = barBlue;
		}
	}
}
