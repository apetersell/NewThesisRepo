using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Yarn.Unity;

public class UnlockManager : MonoBehaviour {

	public static bool restUnlocked;
	public static bool songWritingUnlocked;
	public static bool modelingUnlocked;
	public static bool talkShowUnlocked;
	public Sprite[] unlockMessges; 
	bool extended; 
	public Vector3 full; 
	public float speed;
	Sprite spriteToBe;
	AudioSource auds;
	public AudioClip unlockSound;

	// Use this for initialization
	void Start () 
	{
		auds = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[YarnCommand ("unlock")]
	public void unlock (string sent)
	{
		switch (sent) 
		{
		case "Dance":
			spriteToBe = unlockMessges [0];
			appear ();
			break;
		case "Vocal":
			spriteToBe = unlockMessges [1];
			appear ();
			break;
		case "PR":
			spriteToBe = unlockMessges [2];
			appear ();
			break;
		case "Rest":
			spriteToBe = unlockMessges [3];
			appear ();
			restUnlocked = true;
			break;
		case "Model":
			spriteToBe = unlockMessges [4];
			appear ();
			modelingUnlocked = true;
			break;
		case "SongWriting":
			spriteToBe = unlockMessges [5];
			appear ();
			songWritingUnlocked = true;
			break;
		case "TalkShow":
			spriteToBe = unlockMessges [6];
			appear ();
			talkShowUnlocked = true;
			break;
		case "XIX":
			spriteToBe = unlockMessges [4];
			appear ();
			break;
		}
	}

	public void appear ()
	{
		if (extended) {
			extended = false;
			transform.DOScale (Vector3.zero, speed).OnComplete (appear);
		} else {
			GetComponent<Image> ().sprite = spriteToBe;
			auds.PlayOneShot (unlockSound);
			transform.DOScale (full, speed);
			extended = true;
		}
	}

	[YarnCommand ("vanish")]
	public void vanish()
	{
		extended = false;
		transform.DOScale (Vector3.zero, speed);
	}

	public static void cleanHouse ()
	{
		talkShowUnlocked = false;
		restUnlocked = false;
		songWritingUnlocked = false;
		modelingUnlocked = false;
	}
}
