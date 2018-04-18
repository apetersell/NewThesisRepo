using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorProperty : MonoBehaviour {

	public Texture2D usual;
	public Sprite[] miniGameIcons;
	public Vector2 cursorCenter;

	// Use this for initialization
	void Start () 
	{
		//Cursor.SetCursor (usual, Vector2.zero, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = Input.mousePosition;
	}

	public void changeCursor (int index)
	{
		//Cursor.SetCursor (miniGameIcons[index], Vector2.zero, CursorMode.Auto);	
		GetComponent<Image>().sprite = miniGameIcons[index];
	}
}
