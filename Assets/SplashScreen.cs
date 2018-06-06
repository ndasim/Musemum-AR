using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

	private AsyncOperation async;
	public string sceneName = "AudioTour";

	private void Start()
	{
		StartCoroutine(LoadScene());
	}

	IEnumerator LoadScene()
	{
		if (sceneName == "")
			yield break;

		async = Application.LoadLevelAsync(sceneName);

		Debug.Log("start loading");

		yield return async;
	}

	private void SwitchScene()
	{
		Debug.Log("switching");

		if (async != null)
			async.allowSceneActivation = true;
	}

	private bool fading = false;

	private void Update()
	{
		if (async != null && async.isDone)
		{
			Debug.Log("done loading");
			fading = true;
			SwitchScene ();
		}
	}
}
