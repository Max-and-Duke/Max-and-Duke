﻿using UnityEngine;
using System.Collections;

public class DukeFaceDirection : AbstractBehavior {

	public bool facingRight;
	// Use this for initialization
	void Start () {
		facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		float xPos = Mathf.Abs (transform.localScale.x);
		if (facingRight == false)
			xPos = -xPos;

		transform.localScale = new Vector3 (xPos, transform.localScale.y, transform.localScale.z);
	}
}
