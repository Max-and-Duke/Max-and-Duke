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
			InputManager.maxBody2d.isKinematic = true;
		}

		if (collider.gameObject.name == "Duke") {
			InputManager.dukeCanClimb = true;
			InputManager.dukeBody2d.isKinematic = true;

		}
	}

	void OnTriggerStay2D(Collider2D collider){
		if (collider.gameObject.name == "Max") {
			InputManager.maxBody2d.velocity = new Vector2 (0, 0);
		}

		if (collider.gameObject.name == "Duke") {
			InputManager.dukeBody2d.velocity = new Vector2 (0, 0);

		}
	}
	void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.name == "Max") {
			InputManager.maxCanClimb = false;
			InputManager.maxBody2d.gravityScale = InputManager.GRAVITY;
			InputManager.maxBody2d.isKinematic = false;

		}

		if (collider.gameObject.name == "Duke") {
			InputManager.dukeCanClimb = false;
			InputManager.dukeBody2d.gravityScale = InputManager.GRAVITY;
			InputManager.dukeBody2d.isKinematic = false;

		}
	}
}
