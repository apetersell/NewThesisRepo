using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipDialog : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SceneGuy sg = GameObject.Find ("GlobalStats").GetComponent<SceneGuy> ();
			sg.transitionScene ("Main");
		}
	}
}
