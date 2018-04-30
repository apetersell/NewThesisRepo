using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DJSchedgy : MonoBehaviour {

	public AudioClip titleMusic;
	public AudioClip scheduleMusic;
	public AudioClip selectedTrack;
	public AudioClip VNMusic;
	public AudioClip[] miniGameSongs;
	public AudioClip postGameJingle;
	bool playedJingle;

	GlobalManager globe;
	AudioSource aud;

	void Awake () 
	{
		
	}
	// Use this for initialization
	void Start () 
	{
		globe = GetComponent<GlobalManager> ();
		aud = GetComponent<AudioSource> ();
		shuffle ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (globe.myState == PlayerState.timescheduling) {
			if (aud.clip != scheduleMusic) {
				aud.Stop ();
				aud.clip = scheduleMusic;
				aud.Play ();
				aud.loop = true;
			} 
		} else if (globe.myState == PlayerState.miniGaming) 
		{
			if (!globe.isStopped) {
				if (aud.clip != selectedTrack) {
					aud.Stop ();
					aud.clip = selectedTrack;
					aud.Play ();
					playedJingle = false;
					aud.loop = true;
				}
			} else {
				aud.loop = false;
				if (!playedJingle) 
				{
					if (!aud.isPlaying) 
					{
						aud.Stop ();
						aud.clip = postGameJingle;
						aud.Play ();
						playedJingle = true;
					}
				}
			}
		} else if (globe.myState == PlayerState.visualNoveling) {
			if (aud.clip != VNMusic) {
				aud.Stop ();
				aud.clip = VNMusic;
				aud.Play ();
				aud.loop = true;
			}
		} else if (globe.myState == PlayerState.titleScreen) {
			if (aud.clip != titleMusic) {
				aud.Stop ();
				aud.clip = titleMusic;
				aud.Play ();
				aud.loop = true;
			}
		}
	}

	public void shuffle ()
	{
		int rando = Random.Range (0, miniGameSongs.Length);
		selectedTrack = miniGameSongs [rando];
	}
}
