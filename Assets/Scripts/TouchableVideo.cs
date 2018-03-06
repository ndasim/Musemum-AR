using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Taken from https://assetstore.unity.com/packages/tools/universal-video-texture-lite-5420
 * 
 * Re-edited by ndasim
 */

public class TouchableVideo : MonoBehaviour {
	public float FPS = 30;

	public int firstFrame;
	public int lastFrame;

	public string FileName = "VidTex";
	public string digitsFormat = "0000";

	public enum digitsLocation {Prefix, Postfix};
	public digitsLocation DigitsLocation = digitsLocation.Postfix;

	public bool enableAudio = false;

	public bool enableReplay = true;

	public bool showInstructions = true;
		
	bool audioAttached = false;

	bool firstPlay = true;

	bool playing = false;

	string indexStr = "";

	Texture newTex;
	Texture lastTex;

	float index = 0;

	int intIndex = 0;
	int lastIndex = -1;

	AttachedAudio myAudio = new AttachedAudio(); // Creates an audio class for audio management 

	public RawImage view;
	public RawImage controller;

	public BoxCollider boxCollider;

	void Awake(){
		audioAttached = GetComponent("AudioSource");
	}

	void Start() {	
		index = firstFrame;

		if (audioAttached) {
			myAudio.attachedAudioSource = GetComponent<AudioSource>();
			myAudio.fps = FPS;
			myAudio.frameIndex = firstFrame;
		}
	}


	void Update() {
		if (!boxCollider.enabled)
			Stop ();

		view.enabled = boxCollider.enabled;
		controller.enabled = boxCollider.enabled && !playing;
		
		if (playing) {
			// Forces audio sync on first play (helpful for some devices)
			if (firstPlay && enableAudio || !myAudio.attachedAudioSource.isPlaying) {
				myAudio.Play ();
				firstPlay = false;
			}

			if (audioAttached) {
				index = myAudio.attachedAudioSource.timeSamples / (myAudio.attachedAudioSource.clip.samples / lastFrame);	// Syncronise with audio
			} else {
				index += FPS * Time.deltaTime;
			}


			intIndex = (int)index;

			if (index >= lastFrame)
				index = lastFrame;

			if (intIndex != lastIndex) {
				indexStr = string.Format ("{0:" + digitsFormat + "}", intIndex); 

				if (DigitsLocation == digitsLocation.Postfix)
					newTex = Resources.Load (FileName + indexStr) as Texture;
				else
					newTex = Resources.Load (indexStr + FileName) as Texture;

				lastIndex = intIndex;
			}

			view.texture = newTex;
		} else if (audioAttached){
			myAudio.attachedAudioSource.Pause ();
		}
	}

	public void Play(){
		playing = true;
		controller.enabled = false;
	}

	public void Pause(){
		playing = false;
		controller.enabled = true;
	}

	public void Stop(){
		playing = false;
		firstPlay = true;
		myAudio.attachedAudioSource.Stop ();
	}

	public bool isPlaying(){
		return playing;
	}

	public void Touch(){
		if (playing) {
			Pause ();
		}
		else{
			Play ();
		}
	}
}

// Class for audio management

public class AttachedAudio{
	public AudioSource attachedAudioSource;

	public float frameIndex = 0;
	public float fps = 0;

	public bool togglePlay = true;

	public void Play(){
		if (attachedAudioSource)
		if (!attachedAudioSource.isPlaying)
			attachedAudioSource.Play();
	}

	public void Sync(){
		if (attachedAudioSource)
			attachedAudioSource.time = frameIndex / fps;
	}
}
