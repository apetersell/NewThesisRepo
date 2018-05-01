using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberEffectGenerator : MonoBehaviour {

	public GameObject effect;
	public Color color;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void doEffect(float value)
	{
		int rounded = Mathf.RoundToInt (value);
		GameObject newEffect = Instantiate (effect) as GameObject;
		newEffect.GetComponent<Renderer> ().sortingLayerName = "Effects";
		newEffect.transform.position = this.transform.position;
		newEffect.GetComponent<TextMesh> ().color = color;
		newEffect.GetComponent<TextMesh> ().text = "+" + rounded.ToString ();
	}
}
