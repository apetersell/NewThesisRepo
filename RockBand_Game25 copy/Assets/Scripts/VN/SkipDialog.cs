using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipDialog : MonoBehaviour {

	GlobalManager globe;

	// Use this for initialization
	void Start () 
	{
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (!globe.performance) {
				SceneGuy sg = GameObject.Find ("GlobalStats").GetComponent<SceneGuy> ();
				sg.transitionScene ("Main");
			} else {
				globe.StartMiniGaming ();
			}
		}
	}
}
