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

	void UpdateStatus(Collider2D collider, bool enter) {
		maxIsIn = collider.gameObject.name == "Max" ? enter : maxIsIn;
		dukeIsIn = collider.gameObject.name == "Duke" ? enter : dukeIsIn;		
	}

	void OnTriggerEnter2D(Collider2D collider) {

		UpdateStatus (collider, true);

		if (CheckBothAreIn ()) {
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
