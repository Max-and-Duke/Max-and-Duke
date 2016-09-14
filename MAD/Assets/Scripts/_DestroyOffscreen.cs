﻿using UnityEngine;
using System.Collections;

public class DestroyOffscreen : MonoBehaviour {

	public float offset = 16f;
	public delegate void OnDestroy();
	public event OnDestroy DestroyCallback;

	private bool offscreen;
	private float offscreenX = 0;
	private Rigidbody2D body2d;


	void Awake() {
		body2d = GetComponent<Rigidbody2D> ();
	}
	// Use this for initialization
	void Start () {
		offscreenX = (Screen.width / PixelPerfectCamera.pixelToUnits) / 2 + offset;	
	
	}
	
	// Update is called once per frame
	void Update () {
		var posX = transform.position.x;
		var dirX = body2d.velocity.x;

		offscreen = (dirX < 0 && posX < -offscreenX || dirX > 0 && posX > offscreenX) ? true : false;
		if (offscreen) {
			OnOutOfBounds ();
		}
	}

	private void OnOutOfBounds () {
		offscreen = false;
		GameObjectUtil.Destroy (gameObject);

		if (DestroyCallback != null) {
			DestroyCallback ();
		}
	}
}
