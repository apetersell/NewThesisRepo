using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorProperty : MonoBehaviour {

	public Texture2D usual;
	public Vector2 hotSpot;

	// Use this for initialization
	void Start () 
	{
		Cursor.SetCursor (usual, hotSpot, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public void changeCursor (int index)
	{
		
	}
}
