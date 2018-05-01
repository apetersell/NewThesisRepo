using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NumberEffect : MonoBehaviour {

	TextMesh tm;
	public float speed;
	float yPos;
	public float fadeTime;
	public float scaleTime;
	public Vector2 endScale;

	// Use this for initialization
	void Start () 
	{
		tm = GetComponent<TextMesh> ();
		yPos = transform.position.y;
		transform.DOScale (endScale, scaleTime).OnComplete (startFade);

	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 pos = new Vector2 (transform.position.x, yPos);
		transform.position = pos;
		yPos += speed;
	}

	void startFade()
	{
		StartCoroutine (fade ());
	}

	IEnumerator fade () 
	{
		Color startColor = tm.color;
		float currentTime = 0.0f;
		do
		{ 
			tm.color = Color.Lerp (startColor, Color.clear, currentTime/fadeTime);  
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= fadeTime); 
		Destroy(this.gameObject);
	}
}
