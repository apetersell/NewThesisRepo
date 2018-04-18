using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour {

	public static GameObject mainCam;

	// Use this for initialization
	void Start () {

		if (mainCam == null) {
			mainCam = this.gameObject;
		} else {
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (mainCam);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
