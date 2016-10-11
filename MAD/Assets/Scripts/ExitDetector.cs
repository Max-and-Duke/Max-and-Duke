using UnityEngine;
using System.Collections;

public class ExitDetector : MonoBehaviour {

	public LevelPassedPanel levelPassedPanel;
	public InputManager inputManager;

	private bool maxIsIn = false;
	private bool dukeIsIn = false;

	void Awake () {
		levelPassedPanel.gameObject.SetActive (false);
	}

	void UpdateStatus(Collider2D collider, bool enter) {
		maxIsIn = collider.gameObject.name == "Max" ? enter : maxIsIn;
		dukeIsIn = collider.gameObject.name == "Duke" ? enter : dukeIsIn;		
	}

	void OnTriggerEnter2D(Collider2D collider) {

		UpdateStatus (collider, true);

		if (CheckBothAreIn ()) {
			inputManager.initKeyState ();
			TriggerLevelPassedPanel ();
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		UpdateStatus (collider, false);
	}

	public bool CheckBothAreIn() {
		return maxIsIn && dukeIsIn;
	}

	private void TriggerLevelPassedPanel(){
		levelPassedPanel.Choice ();
	}


}
