using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstParticles : MonoBehaviour {

	ParticleSystem ps;
	public int numOfParticles;
	// Use this for initialization
	void Start () 
	{
		
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	public void burst (Color sent, int num)
	{
		ps = GetComponent<ParticleSystem> (); 
		ParticleSystem.MainModule ma = ps.main;
		ma.startColor = new Color (sent.r, sent.g, sent.b); 
		ps.Emit (num);
	}
}