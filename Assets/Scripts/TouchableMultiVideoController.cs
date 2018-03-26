using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TouchableMultiVideoController : Touchable {

	public SenkronVideoSurface[] surfaces;
	public SenkronRotateAnimator[] rotateAnimators;
	public SenkronSlideUpAnim[] slideUpAnimators;

	public RawImage controller;
	public BoxCollider boxCollider;
	public Frame frameShow;

	bool isPlaying;

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
		surfaces = GetComponents<SenkronVideoSurface> ();
		rotateAnimators = GetComponents<SenkronRotateAnimator> ();
		slideUpAnimators = GetComponents<SenkronSlideUpAnim> ();

		if (audioAttached) {
			audio.attachedAudioSource = GetComponent<AudioSource>();
			audio.fps = FPS;
			audio.frameIndex = 0;
		}
	}


	// Update is called once per frame
	void Update () {
		if (!boxCollider.enabled)
			Stop ();

		controller.enabled = boxCollider.enabled && !isPlaying;
		if (isPlaying) {
			// Forces audio sync on first play (helpful for some devices)
			if (audioAttached) {
				if (firstPlay || !audio.attachedAudioSource.isPlaying) {
					audio.Play ();
					firstPlay = false;
				}
			}

			globalIndex += FPS * Time.deltaTime;

			frameShow.showframe (globalIndex);

			foreach (SenkronVideoSurface surface in surfaces){
				surface.Sync ((int)globalIndex);
			}

			foreach (SenkronRotateAnimator animator in rotateAnimators){
				animator.Sync ((int)globalIndex);
			}

			foreach (SenkronSlideUpAnim animator in slideUpAnimators){
				animator.Sync ((int)globalIndex);
			}
		}
	}


	public void Play(){
		isPlaying = true;
		controller.enabled = false;
	}


	public void Pause(){
		isPlaying = false;
		controller.enabled = true;
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
			Pause ();
		}
		else{
			Play ();
		}
	}
}
