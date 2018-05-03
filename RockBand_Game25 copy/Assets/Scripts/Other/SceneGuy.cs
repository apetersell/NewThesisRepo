using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class SceneGuy : MonoBehaviour {

	public Transform [] screenPositions; 
	public GameObject leftScreen;
	public GameObject rightScreen;
	public GameObject minigameSchedule;
	public GameObject fanBar;
	public static bool loadingScene;
	AsyncOperation async; 
	AsyncOperation unloader;
	public static string current;
	public static string nextScene;
	public float transitionSpeed;
	GlobalManager gm;
	TransitionShifter rightTS;
	TransitionShifter leftTS;
	public Color visible;
	// Use this for initialization
	void Start () 
	{
		gm = GetComponent<GlobalManager> ();
		rightTS = rightScreen.GetComponent<TransitionShifter> ();
		leftTS = leftScreen.GetComponent<TransitionShifter> ();
		transitionScene ("Title");
	}
	
	// Update is called once per frame
	void Update () 
	{
		MidGameSchedule.updateLerpColor ();
		visible = MidGameSchedule.lerpingColor;
	}

	public void transitionScene (string transitionTo)
	{
		rightTS.changeSprite (transitionImage (transitionTo));
		leftTS.changeSprite (transitionImage (transitionTo));
		if (!loadingScene) 
		{
			nextScene = transitionTo;
			loadingScene = true;
			leftScreen.transform.DOMove (screenPositions [1].position, transitionSpeed);
			rightScreen.transform.DOMove (screenPositions [3].position, transitionSpeed).OnComplete (startLoad);
		} else {
			Debug.LogError ("Tried to load multiple scenes at once!");
		}
	}
		
	void startLoad ()
	{
		if (isMiniGame (nextScene)) 
		{
			minigameSchedule.SetActive (true);
			minigameSchedule.GetComponent<MidGameSchedule> ().makeDummySchedule ();
		}
		if (gm.performance && nextScene != "VN") 
		{
			fanBar.SetActive (true);
		}
		StartCoroutine (loadErOnUp());
	}


	IEnumerator loadErOnUp ()
	{
		async = SceneManager.LoadSceneAsync (nextScene, LoadSceneMode.Additive);
		while (!async.isDone)
		{
			yield return null;  
		}
		switchScene ();
	}

	void switchScene()
	{
		if (current != null) 
		{
			StartCoroutine (unload ());
		} 
		else 
		{
			StartCoroutine (unveilNewScene ());
		}
	}

	IEnumerator unveilNewScene ()
	{
		yield return new WaitForSeconds (transitionSpeed);
		minigameSchedule.SetActive (false);
		fanBar.SetActive (false);
		leftScreen.transform.DOMove (screenPositions [0].position, transitionSpeed);
		rightScreen.transform.DOMove (screenPositions [2].position, transitionSpeed);
		SceneManager.SetActiveScene (SceneManager.GetSceneByName (nextScene));
		ScoreManager sm = (ScoreManager)FindObjectOfType(typeof(ScoreManager));
		if (sm != null) 
		{
			sm.findReferences ();
		}
		VocalBlockManager vbm = (VocalBlockManager)FindObjectOfType (typeof(VocalBlockManager));
		if (vbm != null)
		{
			vbm.manualStart ();
		}
		GameManager poseyMatchManager = (GameManager)FindObjectOfType (typeof(GameManager));
		if (poseyMatchManager != null) 
		{
			poseyMatchManager.findReferences ();
		}
		gm.stressMultiplier = 1; //Reset stress multiplier 
		current = SceneManager.GetActiveScene().name;
		switch (current) 
		{
		case "VN":
			gm.myState = PlayerState.visualNoveling;
			break;
		case "Main":
			gm.myState = PlayerState.timescheduling;
			break;
		}
		loadingScene = false;
	}

	IEnumerator unload ()
	{
		unloader = SceneManager.UnloadSceneAsync (SceneManager.GetSceneByName (current));
		while (!unloader.isDone) 
		{
			yield return null;
		}
		StartCoroutine (unveilNewScene ());
	}

	public int transitionImage (string sent)
	{
		int result = 0;
		switch (sent) 
		{
		case "Title":
			result = 0;
			break;
		case "VN":
			result = 1;
			break;
		case "Main":
			result = 2;
			break;
		case "PoseyMatchy":
			result = 3;
			break;
		case "PitchyMatchy":
			result = 4;
			break;
		case "PassOutFliers":
			result = 5;
			break;
		case "PhonyResty":
			result = 6;
			break;
		}
		return result;
	}

	public bool isMiniGame (string sent)
	{
		if (sent == "PoseyMatchy" || sent == "PitchyMatchy" || sent == "PassOutFliers" || sent == "PhonyResty" || sent == "TalkeyShowy" || sent == "SongyWritey" || sent == "DressyUppy") {
			return true;
		} else {
			return false;
		}
	}
}
