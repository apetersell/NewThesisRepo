using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetter : MonoBehaviour {

	Canvas c;
	// Use this for initialization
	void Start () 
	{
		c = GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeRenderMode ()
	{
		c.renderMode = RenderMode.ScreenSpaceCamera;
	}

	public void changeBack ()
	{
		c.renderMode = RenderMode.ScreenSpaceOverlay;
	}
}
