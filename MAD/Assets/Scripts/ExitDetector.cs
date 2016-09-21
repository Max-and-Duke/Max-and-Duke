using UnityEngine;
using System.Collections;

public class ExitDetector : MonoBehaviour {

	public LevelPassedPanel levelPassedPanel;

	private bool maxIsIn = false;
	private bool dukeIsIn = false;

	void Start () {
		levelPassedPanel.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name == "Max") {
			Debug.Log ("Max Tube");
			maxIsIn = true;
		}
		if (collider.gameObject.name == "Duke") {
			dukeIsIn = true;
		}

		//test:
		if (collider.gameObject.name == "Ball") {
			Debug.Log ("Ball Tube");
			maxIsIn = true;
		}

		if (CheckBothAreIn ()) {
			TriggerLevelPassedPanel ();
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
		return maxIsIn; //maxIsIn && dukeIsIn;
	}

	private void TriggerLevelPassedPanel(){
		levelPassedPanel.Choice ();
	}


}
