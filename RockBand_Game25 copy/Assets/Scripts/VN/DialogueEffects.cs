using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using DG.Tweening;

public class DialogueEffects : MonoBehaviour 
{
	Text t;

	// Use this for initialization
	void Start () 
	{
		t = GetComponent<Text> ();
	}

	[YarnCommand("layerSet")]
	public void GetLayer (string num)
	{
		Debug.Log ("OH YEAH!" + num);
	}

	[YarnCommand("size")] 
	public void changeFontSize(string sent)
	{
		int size = int.Parse (sent);
		t.fontSize = size;
	}

	[YarnCommand ("color")]
	public void changeFontCOlor (string sent)
	{
		switch (sent) 
		{
		case "White":
			t.color = Color.white;
			break;
		case "Red":
			t.color = Color.red;
			break;
		case "Blue":
			t.color = Color.blue;
			break;
		case "Green":
			t.color = Color.green;
			break;
		case "Yellow":
			t.color = Color.yellow;
			break;
		case "Cyan":
			t.color = Color.cyan;
			break;
		case "Black":
			t.color = Color.black;
			break;
		case "Magenta":
			t.color = Color.magenta;
			break;
		}
	}
}
