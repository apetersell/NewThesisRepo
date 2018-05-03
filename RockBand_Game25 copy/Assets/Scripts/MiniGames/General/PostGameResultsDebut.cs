using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PostGameResultsDebut : MonoBehaviour {

	public Image fanBar;
	public Text fanNumberDisplay;
	public Text judgement;
	public AudioClip[] sounds;
	public GameObject fanBarGO;
	public GameObject fanNumberGO;
	public GameObject judgementGO;
	public GameObject title;
	public Image screen;
	GlobalManager globe;
	public float fanFillAmount;
	public float displayFloat; 
	bool timerStart;
	public float timer;
	public float barOn;
	public float numberOn;
	public float startFill;
	bool didBarOn;
	bool didNumberOn;
	bool didFill;
	bool didFinalJudgement;
	string addedStressString;
	public GameObject continueButton;
	public GameObject continueText;
	string judgeText;
	string numberDispay;
	public float passingGrade; 
	public float fadeInTime;
	Color lerpingColor; 
	AudioSource auds;

	// Use this for initialization
	void Start () 
	{
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		auds = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () 
	{ 
		//lerpingColor = lerpingColor = Color.Lerp (BarScript.barLight, BarScript.barDark, Mathf.PingPong (Time.time * BarScript.lerpSpeed, 1));   
		if (Input.GetKeyDown (KeyCode.RightArrow)) 
		{
			if (didFinalJudgement) {
				loadVN ();
			}
		}
		stringDisplays ();
		if (timerStart) 
		{
			timer += Time.deltaTime;
			timedEvents ();
		}

		fanBar.fillAmount = fanFillAmount / StoryManager.fanFlyingColors1;
		passingGrade = StoryManager.fanPassing1;

	}

	void stringDisplays()
	{
		if (globe.AigFans < passingGrade) 
		{
			judgeText = "Uh...";
		}
		if (globe.AigFans >= passingGrade && globe.AigFans < StoryManager.fanFlyingColors1) 
		{
			judgeText = "Nice!";
		}
		if (globe.AigFans >= StoryManager.fanFlyingColors1) 
		{
			judgeText = "OUTSTANDING!";
		}

		if (displayFloat == 0) {
			numberDispay = "";
		} else {
			numberDispay = Mathf.RoundToInt (displayFloat).ToString () + " fans";
		}

		fanNumberDisplay.text = numberDispay;
		judgement.text = judgeText;

	}

	public void startPostGame()
	{
		screen.DOFade (.85f, fadeInTime).OnComplete(displayStats);
		displayFloat = globe.AigFans;
	}

	void displayStats()
	{
		timerStart = true;
		title.SetActive (true);
	}

	void timedEvents()
	{
		if (timer > barOn) {
			fanBarGO.SetActive (true);
		}
		if (timer > numberOn) {
			if (!didNumberOn) 
			{
				fanNumberGO.SetActive (true);
				fanNumberGO.GetComponent<ScaleEffect> ().scaleEffect ();
				didNumberOn = true;
			}
		}
		if (timer > startFill && !didFill) {
			didFill = true;
			DOTween.To (() => displayFloat, x => displayFloat = x, 0, 3);
			DOTween.To (() => fanFillAmount, x => fanFillAmount = x, globe.AigFans, 3).OnComplete(judge);
		}
	}

	public void judge()
	{
		judgementGO.SetActive (true);
		if (!didFinalJudgement) 
		{
			judgementGO.GetComponent<ScaleEffect> ().scaleEffect ();
			continueButton.SetActive (true);
			continueText.SetActive(true);
			playSound ();
			didFinalJudgement = true;
		}
	}

	public void playSound()
	{
		if (globe.AigFans < passingGrade) 
		{
			auds.PlayOneShot(sounds[0]);
		}
		if (globe.AigFans >= passingGrade && globe.AigFans < StoryManager.fanFlyingColors1) 
		{
			auds.PlayOneShot(sounds[1]);
		}
		if (globe.AigFans >= StoryManager.fanFlyingColors1) 
		{
			auds.PlayOneShot(sounds[2]);
		}
	}

	public void loadVN() 
	{
		globe.loadVNScene ();
	}
}
