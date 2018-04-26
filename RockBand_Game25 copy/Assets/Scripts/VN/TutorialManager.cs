using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TutorialManager : MonoBehaviour { 

	public GameObject[] tutorials;
	public static int tutorialIndex;
	Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[YarnCommand("setTutorial")]
	public void setTutorial(string sent)
	{
		int newIndex = int.Parse (sent);
		tutorialIndex = newIndex;
	}

	[YarnCommand("start")]
	public void startTutorial ()
	{
		tutorials [tutorialIndex].SetActive (true);
		anim = tutorials [tutorialIndex].GetComponent<Animator> ();
	}

	[YarnCommand("next")]
	public void tutorialNext ()
	{
		anim.SetTrigger ("Next");
	}

	[YarnCommand ("end")]
	public void endTutorial ()
	{
		for (int i = 0; i < tutorials.Length; i++) 
		{
			tutorials [i].SetActive (false);
		}
	}
}
