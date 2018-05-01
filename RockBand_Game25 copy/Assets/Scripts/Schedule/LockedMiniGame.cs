using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedMiniGame : MonoBehaviour {

	public GameObject [] unlockables;
	public GameObject [] lockedIcons;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		unlockables [0].SetActive (UnlockManager.restUnlocked);
		unlockables [1].SetActive (UnlockManager.modelingUnlocked);
		unlockables [2].SetActive (UnlockManager.songWritingUnlocked);
		unlockables [3].SetActive (UnlockManager.talkShowUnlocked);

		if (UnlockManager.restUnlocked) 
		{
			lockedIcons[0].SetActive (false);
		}
		if (UnlockManager.modelingUnlocked) 
		{
			lockedIcons[1].SetActive (false);
		}
		if (UnlockManager.songWritingUnlocked) 
		{
			lockedIcons[2].SetActive (false);
		}
		if (UnlockManager.talkShowUnlocked) 
		{
			lockedIcons[3].SetActive (false);
		}
	}
}
