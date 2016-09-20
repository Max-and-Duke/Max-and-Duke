using UnityEngine;
using System.Collections;


public class StairCollisonDetect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.name == "Max") {
			InputManager.maxCanClimb = true;
		}

		if (collider.gameObject.name == "Duke") {
			InputManager.dukeCanClimb = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.name == "Max") {
			InputManager.maxCanClimb = false;
			InputManager.maxBody2d.gravityScale = 40;
		}

		if (collider.gameObject.name == "Duke") {
			InputManager.dukeCanClimb = false;
			InputManager.dukeBody2d.gravityScale = 40;

		}
	}
}
