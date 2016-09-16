using UnityEngine;
using System.Collections;

public class ExitDetector : MonoBehaviour {

	private bool maxIsIn = false;
	private bool dukeIsIn = false;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name == "Max") {
			maxIsIn = true;
		}
		if (collider.gameObject.name == "Duke") {
			dukeIsIn = true;
		}

		//test:
		if (collider.gameObject.name == "Ball") {
			maxIsIn = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.name == "Max") {
			maxIsIn = false;
		}
		if (collider.gameObject.name == "Duke") {
			dukeIsIn = false;
		}

		//test:
		if (collider.gameObject.name == "Ball") {
			maxIsIn = false;
		}
	}

	public bool CheckBothAreIn() {
		return maxIsIn && dukeIsIn;
	}
}
