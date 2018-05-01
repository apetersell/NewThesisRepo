using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;

public class PassyPlayerControls : MonoBehaviour {

	public float activeFrames;
	public float activeTimer;
	bool playerActive;
	Detector detectBoxL; 
	Detector detectBoxR;
	Animator anim;
	public BurstParticles[] particles;
	bool hit;
	float numberOfParticles;
	public ScoreManager sm; 

	void Awake ()
	{
		activeTimer = activeFrames;
		detectBoxL = GameObject.Find ("LeftDetector").GetComponent<Detector>(); 
		detectBoxR = GameObject.Find ("RightDetector").GetComponent<Detector> ();
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () 
	{
		//ScoreManager sm = (ScoreManager)FindObjectOfType (typeof(ScoreManager)); 
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		anim.SetBool ("Hit", hit);
		controls ();
	}

	void controls () 
	{

		if (playerActive) 
		{
			activeTimer--;
		} 
		else 
		{
			if (Input.GetKeyDown(KeyCode.RightArrow)) 
			{
				setPlayerActive (1);
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow)) 
			{
				setPlayerActive (-1);
			}
		}

		if (activeTimer <= 0) 
		{
			playerActive = false;
			activeTimer = activeFrames;
		}
	}

	void setPlayerActive (int dir)
	{
		playerActive = true;
		if (dir == 1) 
		{
			if (detectBoxR.NPC != null) {
				PassyNPC p = detectBoxR.NPC.GetComponent<PassyNPC> ();
				if (p.active) {
					p.gotFlier (true);
					hit = true;
					anim.SetTrigger ("RightHit");
					particles [0].burst (Color.white, sm.particleNum);
					particles [0].gameObject.GetComponent<NumberEffectGenerator> ().doEffect (sm.valueOfMatch);
				} else {
					hit = false;
					anim.SetTrigger ("RightMiss");
				}
			} else {
				hit = false;
				anim.SetTrigger ("RightMiss");
			}
		}
		if (dir == -1) 
		{
			anim.SetTrigger ("Left");
			if (detectBoxL.NPC != null) {
				PassyNPC p = detectBoxL.NPC.GetComponent<PassyNPC> ();
				if (p.active) {
					p.gotFlier (true);
					hit = true;
					anim.SetTrigger ("LeftHit");
					particles [1].burst (Color.white, sm.particleNum);
					particles [1].gameObject.GetComponent<NumberEffectGenerator> ().doEffect (sm.valueOfMatch);
				} else {
					hit = false;
					anim.SetTrigger ("LeftMiss");
				}
			} else {
				hit = false;
				anim.SetTrigger ("LeftMiss");
			}
		}
	}
}
