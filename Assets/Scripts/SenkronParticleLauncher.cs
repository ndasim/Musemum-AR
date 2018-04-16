using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenkronParticleLauncher: Senkron {
	public int startIndex;								// In which frame our senkrons will start to run with localized frames
	public ParticleSystem particle;

	bool isPlaying;

	// Use this for initialization
	void Start () {
		particle.Pause ();
		particle.Clear ();
	}


	public override void hide ()
	{

	}

	// Update is called once per frame
	override public void Sync (int globalIndex) {
		if (globalIndex > startIndex) {
			if (!isPlaying) {
				isPlaying = true;
				particle.Play ();
			}
		} else {
			if (isPlaying) {
				isPlaying = false;
				particle.Stop ();
			}
		}
	}
}
