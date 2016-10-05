using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum Mode {
	Deploy = 0,
	Play = 1
}

public class ModeManager : MonoBehaviour {
	public Sprite[] modeSprite;

	private Mode mode = Mode.Deploy;
	private int numOfModes = System.Enum.GetNames (typeof(Mode)).Length;
	private Dictionary<string, GameObject> gameObjectPool = new Dictionary<string, GameObject>();
	private Dictionary<string, bool> gameObjectSettingsForDeployMode = new Dictionary<string, bool>
	{
		{"Console", false},
		{"Toolbox", true}
	}; // will be stored outside code.s
	private Dictionary<string, bool> gameObjectSettingsForPlayMode = new Dictionary<string, bool>
	{
		{"Console", true},
		{"Toolbox", false}
	}; // will be stored outside code.s
		

	void SetGameObjectActiveInPool(string target, bool active) {
		GameObject targetGameObject;

		if (gameObjectPool.ContainsKey (target)) {
			targetGameObject = gameObjectPool [target];
		} else {
			targetGameObject = GameObject.Find (target);
			gameObjectPool.Add (target, targetGameObject);
		}

		targetGameObject.SetActive (active);
	}

	void SetAllGameObjectsActiveWithSettings (Dictionary<string, bool> gameObjectSettingsForDeployMode) {
		foreach (KeyValuePair<string, bool> entry in gameObjectSettingsForDeployMode) {
			SetGameObjectActiveInPool (entry.Key, entry.Value);
		}
	}

	public void GoToMode(Mode mode) {
		/// sprite for button image sprite should be update as well... (TBImplement)

		switch (mode) {
		case Mode.Deploy:
			SetAllGameObjectsActiveWithSettings (gameObjectSettingsForDeployMode);
			setIsKinematic (true);
			break;
		case Mode.Play:
			SetAllGameObjectsActiveWithSettings (gameObjectSettingsForPlayMode);
			break;
		}
	}
		
	void Awake() {
		GoToMode (Mode.Deploy);
	}

	void ToggleAllGameObjectsActive () {
		foreach(KeyValuePair<string, GameObject> entry in gameObjectPool)
		{
			bool active = !entry.Value.activeInHierarchy;

			if (!active) {
				setIsKinematic (active);
				setHingeJoint ();
				setRotateArrow (active);
			}

			entry.Value.gameObject.SetActive (active);

			if (active) {
				setIsKinematic (active);
				setRotateArrow (active);
			}

			Debug.Log ("Just toggled <" + entry.Key + "> to " + active.ToString () + ".");
		}
	}

	public void DisableAllGameObjectsInPool () {
		foreach(KeyValuePair<string, GameObject> entry in gameObjectPool)
		{
			entry.Value.gameObject.SetActive (false);
		}
	}
		
	public void SwitchMode() {
		ToggleAllGameObjectsActive ();

		GameObject modeButton = GameObject.Find ("Mode Button");
		Image image = modeButton.GetComponent<Image> ();

		mode = (Mode)((1 + (int)mode) % numOfModes); // switch to next mode

		image.sprite = modeSprite [(int) mode];
	}

	public void setIsKinematic(bool a){
		if (a) {
			var boards = GameObject.FindGameObjectsWithTag("Board");
			foreach (var board in boards) {
				board.GetComponent<Rigidbody2D> ().isKinematic = true;
			}

		} else {
			var boards = GameObject.FindGameObjectsWithTag("Board");
			foreach (var board in boards) {
				board.GetComponent<Rigidbody2D> ().isKinematic = false;
			}
		}
	}

	public void setRotateArrow(bool active){
		var boards = GameObject.FindGameObjectsWithTag("Board");
		foreach (var board in boards) {
			var items = board.GetComponentsInChildren<Image> ();
			foreach (var item in items) {
				if(item.name == "RotateButton-right"){
					item.enabled = active;
				}
				if (item.name == "RotateButton-left") {
					item.enabled = active;
				}
			}
		}
	}

	public void setHingeJoint(){
		var boards = GameObject.FindGameObjectsWithTag("Board");
		foreach (var board in boards) {
			var boardScript = board.transform.GetComponent<Draggable> ();
			if (boardScript.dragNailNum == 1) {
				HingeJoint2D boardHJ = board.AddComponent<HingeJoint2D> ();
				//boardHJ.enableCollision = true;
				boardHJ.connectedAnchor = boardScript.nailPosition;
				Debug.Log (board.transform.position);
				Debug.Log (boardScript.nailPosition);
				boardHJ.anchor = boardScript.nailPosition - board.transform.position;
			}
			if (boardScript.dragNailNum >= 2 ) {
				board.GetComponent<Rigidbody2D> ().isKinematic = true;
			}
		}
	}


}
