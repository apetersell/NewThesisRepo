using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedMiniGame : MonoBehaviour {

	public GameObject [] unlockables;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		unlockables [0].SetActive (UnlockManager.restUnlocked);
	}
}
