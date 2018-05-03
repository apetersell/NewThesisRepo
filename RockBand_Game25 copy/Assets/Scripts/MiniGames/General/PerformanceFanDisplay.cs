using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceFanDisplay : MonoBehaviour 
{
	Image img;
	// Use this for initialization
	void Start () 
	{
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
//		img.fillAmount = (globe.AigFans / StoryManager.fanFlyingColors1);
	}
}
