using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchableMultiVideoController : Touchable {

	public SenkronVideoSurface[] surfaces;
	public Senkron[] senkronObjects;

	public RawImage controller;
	public RawImage playPause;
	public RawImage restartIcon;
	public BoxCollider boxCollider;
	public Frame frameShow;

	public int start = 0;

	bool isPlaying;
	bool forcePause;
	bool audioAttached;
	bool firstPlay = true;

	float globalIndex;

	public int FPS = 30;

	AttachedAudio audio = new AttachedAudio();

	void Awake(){
		audioAttached = GetComponent("AudioSource");
		audio.attachedAudioSource = GetComponent<AudioSource>();
	}
		
	void Start () {
		playPause.texture = null;
		surfaces = GetComponents<SenkronVideoSurface> ();
		senkronObjects = GetComponents<Senkron> ();

		globalIndex = start;

		if (audioAttached) {
			audio.attachedAudioSource = GetComponent<AudioSource>();
			audio.fps = FPS;
			audio.attachedAudioSource.time = start / FPS;
		}
	}
		
	void Update () {
		if (!boxCollider.enabled || forcePause) {
			Pause ();
		}
		else if (globalIndex != 0 && !isPlaying) {
			Play ();
		}

		foreach (SenkronVideoSurface surface in surfaces) {
			surface.rawImage.enabled = boxCollider.enabled;
		}

		playPause.enabled = boxCollider.enabled;
		controller.enabled = boxCollider.enabled && firstPlay;
		if (isPlaying) {
			// Forces audio sync on first play (helpful for some devices)
			if (audioAttached) {
				if (firstPlay || !audio.attachedAudioSource.isPlaying) {
					audio.Play ();
					firstPlay = false;
				}
			}

			if (audioAttached) {
				globalIndex = audio.attachedAudioSource.time * FPS;	// Syncronise with audio
			} else {
				globalIndex += FPS * Time.deltaTime;
			}

			frameShow.showframe (globalIndex);

			foreach (SenkronVideoSurface surface in surfaces){
				surface.Sync ((int)globalIndex);
			}

			foreach (Senkron animator in senkronObjects){
				animator.Sync ((int)globalIndex);
			}
		}
	}
		
	public void Play(){
		playPause.texture = Resources.Load ("pauseBtn") as Texture;
		restartIcon.texture = Resources.Load ("restartBtn") as Texture;

		playPause.color = new Color(255,255,255,255);
		restartIcon.color = new Color(255,255,255,255);

		isPlaying = true;
		controller.enabled = false;
		forcePause = false;
	}


	public void Pause(){
		playPause.texture = globalIndex != 0 ? Resources.Load ("playBtn") as Texture : null;
		restartIcon.texture = globalIndex != 0 ? Resources.Load ("restartBtn") as Texture : null;

		isPlaying = false;
		//controller.enabled = true;
		if(audioAttached)
			audio.attachedAudioSource.Pause ();
	}

	public void ForcePause(){
		forcePause = true;
	}

	public void Stop(){
		globalIndex = 0;
		isPlaying = false;
		firstPlay = true;

		playPause.color = new Color (0, 0, 0, 0);
		restartIcon.color = new Color (0, 0, 0, 0);

		if(audioAttached)
			audio.attachedAudioSource.Stop ();

		// One more broadcast
		frameShow.showframe (globalIndex);

		foreach (SenkronVideoSurface surface in surfaces){
			surface.Sync ((int)globalIndex);
		}

		foreach (Senkron animator in senkronObjects){
			animator.Sync ((int)globalIndex);
		}
	}
		
	override public void Touch(){
		if (isPlaying) {
			ForcePause ();
		}
		else{
			Play ();
		}
	}
}
