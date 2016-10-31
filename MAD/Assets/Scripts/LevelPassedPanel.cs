using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class LevelPassedPanel : MonoBehaviour {
	
	public Button levelButton;
	public Button replayButton;
	public Button nextButton;
	public GameObject levelPassedPanelObject;
	public GameObject buttonPanelObject;

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
		levelPassedPanelObject.SetActive (false);
	}

	void ShowButtonPanel() {
		buttonPanelObject.SetActive (true);
	}
		
	void HideButtonPanel() {
		
		buttonPanelObject.SetActive (false);
	}

	private void ShowStars() {
		var costManager = GameObject.Find ("Cost Manager").GetComponent<CostManager> ();
		var numOfStars = costManager.GetStarNumbers ();
	
		switch (numOfStars) {
		case 1:
			SetAlpha ("Star 2", 0f);
			SetAlpha ("Star 3", 0f);
			break;
		case 2:
			SetAlpha ("Star 2", 1.0f);
			SetAlpha ("Star 3", 0f);
			break;
		case 3:
			SetAlpha ("Star 2", 1.0f);
			SetAlpha ("Star 3", 1.0f);
			break;
		default:
			break;
		}
	}

	private void SetAlpha(string name, float alpha) {
		var spriteRenderer = GameObject.Find (name).GetComponent<SpriteRenderer> ();
		var color = spriteRenderer.color;
		color.a = alpha;
		spriteRenderer.color = color;
	}

	public void Choice () {
		
		levelPassedPanelObject.SetActive (true);
		ShowStars ();
		HideGameObjects ();
		HideButtonPanel ();

		levelButton.onClick.RemoveAllListeners();
		levelButton.onClick.AddListener (OnClickLevelButton);

		replayButton.onClick.RemoveAllListeners();
		replayButton.onClick.AddListener (OnClickReplayButton);
		replayButton.onClick.AddListener (ClosePanel);
		replayButton.onClick.AddListener (GoToPlayMode);
		replayButton.onClick.AddListener (ShowButtonPanel);

		nextButton.onClick.RemoveAllListeners();
		nextButton.onClick.AddListener (OnClickNextButton);

		levelButton.gameObject.SetActive (true);
		replayButton.gameObject.SetActive (true);
		nextButton.gameObject.SetActive (true);
	}


}
