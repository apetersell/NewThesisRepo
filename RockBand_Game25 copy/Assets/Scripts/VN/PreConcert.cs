using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PreConcert : MonoBehaviour {

	GlobalManager globe;
	public Text danceTotal;
	public Text vocalTotal;
	public Text prTotal;
	public Text effectiveDance;
	public Text effectiveVocal;
	public Text effectivePR;
	public Text effectiveStress;
	public Text dancePerHit;
	public Text vocalPerHit;
	public Text prPerHit;
	int danceFanValue;
	int vocalFanValue;
	int prFanValue;
	int dancePerHitDisplay; 
	int vocalPerHitDisplay; 
	int prPerHitDisplay;
	public Image screen;
	public GameObject title;
	public GameObject bars;
	public GameObject label;
	public GameObject dancePerHitGO;
	public GameObject vocalPerHitGO;
	public GameObject prPerHitGO;
	public float fadeInTime;
	bool timerStart;
	float timer;
	public float barsTime;
	bool shownBars;
	public float labelTime;
	bool showLabels; 
	public float showNumbersTime;
	bool shownNumbers;
	public float lerpNumbersTime;
	bool lerpedNumbers;
	bool complete;
	bool started;
	public GameObject readyButton;

	// Use this for initialization
	void Start () 
	{
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
	}
	
	// Update is called once per frame
	void Update () 
	{
		danceFanValue = Mathf.RoundToInt (globe.effectiveDance / 50);
		vocalFanValue = Mathf.RoundToInt (globe.effectiveVocal / 50);
		prFanValue = Mathf.RoundToInt (globe.effectivePR / 50); 
		stringDisplays ();
		timedEvents (); 
		if (timerStart) 
		{
			timer += Time.deltaTime;
		}
	}

	void stringDisplays()
	{
		effectiveDance.text = Mathf.Round (globe.DanceScore).ToString () + "<color=#F49AC1FF> (-" + Mathf.Round(globe.Stress) + ")</color>";
		effectiveVocal.text = Mathf.Round (globe.VocalScore).ToString () + "<color=#F49AC1FF> (-" + Mathf.Round(globe.Stress) + ")</color>";
		effectivePR.text = Mathf.Round (globe.PRScore).ToString () + "<color=#F49AC1FF> (-" + Mathf.Round(globe.Stress) + ")</color>";
		effectiveStress.text = Mathf.Round (globe.Stress).ToString ();
		danceTotal.text = Mathf.Round (globe.effectiveDance).ToString ();
		vocalTotal.text = Mathf.Round (globe.effectiveVocal).ToString ();
		prTotal.text = Mathf.Round (globe.effectivePR).ToString ();
		dancePerHit.text = dancePerHitDisplay.ToString ();
		vocalPerHit.text = vocalPerHitDisplay.ToString ();
		prPerHit.text = prPerHitDisplay.ToString ();
	}

	public void startPreConcert()
	{
		if (!started) 
		{
			started = true;
			screen.DOFade (1f, fadeInTime).OnComplete (displayTitle);
		}
	}

	void displayTitle()
	{
		title.SetActive (true);
		timerStart = true;
	}

	void timedEvents ()
	{
		if (timer >= barsTime && !shownBars) 
		{
			shownBars = true;
			bars.SetActive (true);
		}
		if (timer >= labelTime && !showLabels) 
		{
			showLabels = true;
			label.SetActive (true);
		}
		if (timer >= showNumbersTime && !shownNumbers) 
		{
			dancePerHitGO.SetActive(true);
			vocalPerHitGO.SetActive(true);
			prPerHitGO.SetActive (true);
		}
		if (timer >= lerpNumbersTime && !lerpedNumbers) 
		{
			DOTween.To(()=>dancePerHitDisplay, x=> dancePerHitDisplay = x, danceFanValue, 1);
			DOTween.To(()=>vocalPerHitDisplay, x=> vocalPerHitDisplay = x, vocalFanValue, 1);
			DOTween.To(()=>prPerHitDisplay, x=> prPerHitDisplay = x, prFanValue, 1).OnComplete(numbersScale);
			lerpedNumbers = true;
		}
	}

	void numbersScale ()
	{
		if (dancePerHitDisplay <= 0) 
		{
			dancePerHitDisplay = 1;
		}
		if (vocalPerHitDisplay <= 0) 
		{
			vocalPerHitDisplay = 1;
		}
		if (prPerHitDisplay <= 0) 
		{
			prPerHitDisplay = 1;
		}
		dancePerHitGO.GetComponent<ScaleEffect> ().scaleEffect ();
		vocalPerHitGO.GetComponent<ScaleEffect> ().scaleEffect ();
		prPerHitGO.GetComponent<ScaleEffect> ().scaleEffect ();
		readyButton.SetActive (true);
		complete = true;
	}

	public void startConcert()
	{
		globe.StartMiniGaming ();
	}
}
