using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TouchableMultiVideoController : Touchable {

	public SenkronVideoSurface[] surfaces;
	public Senkron[] senkronObjects;

	public RawImage controller;
	public RawImage playPause;
	public BoxCollider boxCollider;
	public Frame frameShow;

	public int start = 0;

	bool isPlaying;
	bool forcePause;

	float globalIndex;

	public int FPS = 30;

	AttachedAudio audio = new AttachedAudio();

	bool audioAttached;

	bool firstPlay = true;

	void Awake(){
		audioAttached = GetComponent("AudioSource");
		audio.attachedAudioSource = GetComponent<AudioSource>();
	}


	// Use this for initialization
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


	// Update is called once per frame
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
		Color color = playPause.color;
		playPause.texture = Resources.Load ("pauseBtn") as Texture;
		color.a = 255;
		playPause.color = color;

		isPlaying = true;
		controller.enabled = false;
		forcePause = false;
	}


	public void Pause(){
		playPause.texture = globalIndex != 0 ? Resources.Load ("playBtn") as Texture : null;
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
		if(audioAttached)
			audio.attachedAudioSource.Stop ();
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
