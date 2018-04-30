using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FriendEffect : MonoBehaviour 
{
	AudioSource auds;
	public Sprite happyFaceJPe; 
	public Sprite angryFaceJpe; 
	public Sprite happyFaceLee;
	public Sprite angryFaceLee;
	public bool happy;
	public float speed;
	public AudioClip happySound;
	public AudioClip angrySound; 
	public float scaleTime;
	public float expandedScale;
	public float fadeTime;
	Vector3 originalScale;
	Image img;


	// Use this for initialization
	void Start () 
	{
		auds = GetComponent<AudioSource> ();
		transform.DOScale (expandedScale, scaleTime).OnComplete(fadeAway);
		originalScale = GetComponent<RectTransform> ().localScale;
		if (happy) {
			auds.PlayOneShot (happySound);
		} else {
			auds.PlayOneShot (angrySound);
		}
	

	}

	public void changeSprite(string sent)
	{
		img = GetComponent<Image> ();
		if (sent == "J-Pe") {
			if (happy) {
				img.sprite = happyFaceJPe; 
			} else {
				img.sprite = angryFaceJpe; 
			}
		} else {
			if (happy) {
				img.sprite = happyFaceLee;
			} else {
				img.sprite = angryFaceLee;
			}
		}
	}

	public void fadeAway()
	{
		img.DOColor (Color.clear, fadeTime).OnComplete (die);
	}

	public void die()
	{
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () 
	{
		GetComponent<RectTransform> ().localPosition = new Vector3 (
			GetComponent<RectTransform> ().localPosition.x,
			GetComponent<RectTransform> ().localPosition.y + speed,
			GetComponent<RectTransform> ().localPosition.z);
	}
}
