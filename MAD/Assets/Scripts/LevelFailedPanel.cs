using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class LevelFailedPanel : MonoBehaviour {

	public Button levelButton;
	public Button replayButton;
	public GameObject levelFailedPanelObject;
	public GameObject buttonPanelObject;

	void OnClickReplayButton () {
		ClosePanel();
		GoToPlayMode();
		ShowButtonPanel();
	}

	void HideGameObjects() {
		GameObject modeManagerGameObject = GameObject.Find ("Mode Manager");
		ModeManager modeManager = modeManagerGameObject.GetComponent<ModeManager> ();
		modeManager.HideAllComponents ();
	}

	void GoToPlayMode() {
		GameObject modeManagerGameObject = GameObject.Find ("Mode Manager");
		ModeManager modeManager = modeManagerGameObject.GetComponent<ModeManager> ();
		modeManager.GoToMode (Mode.Play);
	}

	void ClosePanel () {
		levelFailedPanelObject.SetActive (false);
	}

	void ShowButtonPanel() {
		buttonPanelObject.SetActive (true);
	}

	void HideButtonPanel() {
		buttonPanelObject.SetActive (false);
	}

	public void Choice () {
		levelFailedPanelObject.SetActive (true);
		HideGameObjects ();
		HideButtonPanel ();

		replayButton.onClick.RemoveAllListeners();
		replayButton.onClick.AddListener (OnClickReplayButton);

		levelButton.gameObject.SetActive (true);
		replayButton.gameObject.SetActive (true);
	}

}
