﻿using UnityEngine;
using System.Collections;

public class FailureDetection : MonoBehaviour {

	public LevelFailedPanel levelFailedPanel;
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D collider) {

		if (collider.gameObject.name == "Max" || collider.gameObject.name == "Duke") {
			SoundManager.instance.musicSource.Stop ();
			levelFailedPanel.Choice ();
		}
	}
}