using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSenkronizer : Senkron {
	Senkron[] senkrons;

	public int startIndex;								// In which frame our senkrons will start to run with localized frames
	public GameObject subSenkrons;

	// Use this for initialization
	void Start () {
		senkrons = subSenkrons.GetComponents<Senkron> ();
	}


	public override void hide ()
	{
		
	}

	// Update is called once per frame
	override public void Sync (int globalIndex) {
		if (globalIndex > startIndex) {
			foreach (Senkron senkron in senkrons) {
				senkron.Sync (globalIndex - startIndex);		// Localize indexes of sub senkrons
			}
		} else {
			foreach (Senkron senkron in senkrons) {
				senkron.hide ();		// Localize indexes of sub senkrons
			}
		}
	}
}
