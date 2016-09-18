using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class LevelPassedPanel : MonoBehaviour {
	
	public Button levelButton;
	public Button replayButton;
	public Button nextButton;
	public GameObject levelPassedPanelObject;



	void OnClickLevelButton () {
		Debug.Log ("Go back to level list.");
	}

	void OnClickReplayButton () {
		Debug.Log ("Replay this level.");
		//		ResetCharacters ();
	}

	void OnClickNextButton () {
		Debug.Log ("Go to next level!");
	}
		
	public void Choice () {
		levelPassedPanelObject.SetActive (true);

		levelButton.onClick.RemoveAllListeners();
		levelButton.onClick.AddListener (OnClickLevelButton);
		//		levelButton.onClick.AddListener (ClosePanel);

		replayButton.onClick.RemoveAllListeners();
		replayButton.onClick.AddListener (OnClickReplayButton);
		replayButton.onClick.AddListener (ClosePanel);

		nextButton.onClick.RemoveAllListeners();
		nextButton.onClick.AddListener (OnClickNextButton);
		//		nextButton.onClick.AddListener (ClosePanel);

		levelButton.gameObject.SetActive (true);
		replayButton.gameObject.SetActive (true);
		nextButton.gameObject.SetActive (true);
	}

	void ClosePanel () {
		levelPassedPanelObject.SetActive (false);
	}
}
