using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDeckText : MonoBehaviour {

	GlobalManager globe;

	// Use this for initialization
	void Start () 
	{

		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		if (globe != null) 
		{
			if (globe.performance) 
			{
				if (globe.NextActivity == "End of the Week") {
					GetComponent<Text> ().text = "Finish";
				} else {
					GetComponent<Text> ().text = "Concert";
				}
			} 
			else {
				GetComponent<Text> ().text = "Next: " + globe.NextActivity;
			}
		}
	}
	// Update is called once per frame
	void Update () 
	{

	}
}
