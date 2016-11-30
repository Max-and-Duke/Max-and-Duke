using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;


public enum Mode {
	// must be indexed for `componentSettings`
	Deploy = 0,
	Play = 1
}


public class ModeManager : MonoBehaviour {
	public Sprite[] modeSprite;

	private Mode currentMode = Mode.Deploy;
	private int numOfModes = System.Enum.GetNames (typeof(Mode)).Length;
	private bool active = true;

	// =====================================================================================
	// this holds all components effected by mode-changing, AKA "Toolbox" and "Console" for now
	private Dictionary<string, GameObject> componentsPool = new Dictionary<string, GameObject>();
	private Dictionary<string, float[]> componentSettings;
	// =====================================================================================

	private void SetPosY (GameObject gameObject, float posY) {
		var vector = gameObject.transform.position;
		vector[1] = posY;
		gameObject.transform.position = vector;
	}

	private void LoadSettingsFromFile (string fileName) {
		/// DATA STRUCTURE EXPLANATION:
		/// for each entry in the dictionary, 
		/// - KEY: gameObject name (string)
		/// - VALUE: postions for each Mode using the Mode index as array index
		/// E.g. `Mode.Deploy` is indexed as `0`, so the postion (in Y)
		/// of gameObject `Console` in Deploy mode is 
		/// 	float posY = componentSettings["Console"][0];
		/// the last number in the array is the idle position (off-screen position)
		componentSettings = new Dictionary<string, float[]> 
		{
			{ "Console", new []{ -1080.0f, -540.0f, -1080.0f } },
			{ "Toolbox", new []{ -500.0f, -640.0f, -640.0f } },
		};
	}

	/// <summary>
	/// This method:
	/// 1. reads comopent position configuration from resourse file and put it in `componentSettings`
	/// 2. intializes all components' postion is scene
	/// 3. puts all components' in `componentsPool`
	/// </summary>
	private void LoadComponents() {
		LoadSettingsFromFile ("ModeSettings");

		foreach (KeyValuePair<string, float[]> entry in componentSettings) {
			var componentName = entry.Key;
			var component = GameObject.Find (componentName);
			SetPosY (component, entry.Value[0]);
			componentsPool.Add (componentName, component);
		}
	}

	/// <summary>
	/// Slides `gameObject` to `positionSlideTo` with continuous translation.
	/// </summary>
	private void SlideTo (GameObject gameObject, float positionSlideTo) {
		// this is just a dummy implementation for now
		var vector = gameObject.transform.position;
		vector [1] = positionSlideTo;
		iTween.MoveTo(gameObject, vector, 1.0f);
	}

	public void GoToMode(Mode mode) {
		currentMode = mode;
		if (currentMode == Mode.Deploy) {
			InputManager.instance.RePosition ();
		}
		foreach (KeyValuePair<string, float[]> entry in componentSettings) {
			SlideTo (componentsPool[entry.Key], entry.Value[(int)mode]);
		}
	}

	void Awake() {
		LoadComponents ();
	}
		
	public void HideAllComponents() {
		foreach (KeyValuePair<string, float[]> entry in componentSettings) {
			var component = componentsPool [entry.Key];
			var posY = entry.Value [entry.Value.Length - 1]; // this is the idle position
			SetPosY(component, posY);
		}
	}

	/// <summary>
	/// Returns the next mode based on the enumeration order of Mode.
	/// </summary>
	private Mode GetNextMode() {
		return (Mode)((1 + (int)currentMode) % numOfModes); 
	}

	private void SwithButtonImage() {
		GameObject modeButton = GameObject.Find ("Mode Button");
		Image image = modeButton.GetComponent<Image> ();
		image.sprite = modeSprite [(int) currentMode];
	}

	public void SwitchMode() {
		currentMode = GetNextMode();
		GoToMode (currentMode);
		SwithButtonImage ();
		active = !active;
		setRotateArrow (active);
		changeToolBoxBoardTag (active);
		setIsKinematic (active);
		setHingeJoint (!active);
		setDraggable (active);

	}

	// ================================================================
	// ================================================================

	/// OBSOLETE
	void ToggleAllGameObjectsActive () {
		foreach(KeyValuePair<string, GameObject> entry in componentsPool)
		{
			//////////////////////////////////////////////////////////
			// Hey Zhonghao, 
			// FYI, 
			// gameObjects (Toolbox and Console) are ALWAYS active now.
			// you can use `currentMode` to get current mode. and you
			// really wanna do what you wanna do here somewhere else
			// cuz this method is OBSOLETE now.
			//////////////////////////////////////////////////////////

			bool active = !entry.Value.activeInHierarchy;

			// deploy mode
			if (entry.Key.Equals ("Toolbox")) {
				if (active) { // if current toolbox is active
					setIsKinematic (active);
					setHingeJoint (!active);
					setDraggable (active);
					setRotateArrow (active);
				}
			}

			entry.Value.gameObject.SetActive (active);
			// play mode
			if (entry.Key.Equals ("Toolbox")) {
				if (!active) {
					setIsKinematic (active);
					setHingeJoint (!active);
					setDraggable (active);
					setRotateArrow (active);

				}
			}
		}
	}


	public void changeToolBoxBoardTag(bool isGoDeployMode){
		if (!isGoDeployMode) {
			// board
			var toolBoxBoard = GameObject.Find ("Draggable-board");
			toolBoxBoard.GetComponent<Collider2D> ().enabled = false;
			var childrenColliders = toolBoxBoard.GetComponentsInChildren<Collider2D>();
			foreach (var childCollider in childrenColliders) {
				childCollider.enabled = false;
			}
			toolBoxBoard.tag = "Special-board";

			// box
			var boxGameObject = GameObject.Find ("Draggable-box");
			if (boxGameObject) {
				boxGameObject.GetComponent<Collider2D> ().enabled = isGoDeployMode;
				boxGameObject.tag = "Special-box";
			}


		} else {
			// board
			var toolBoxBoard = GameObject.FindGameObjectWithTag("Special-board");
			toolBoxBoard.GetComponent<Collider2D> ().enabled = true;
			var childrenColliders = toolBoxBoard.GetComponentsInChildren<Collider2D>();
			foreach (var childCollider in childrenColliders) {
				childCollider.enabled = true;
			}
			toolBoxBoard.tag = "Board";

			// box
			var boxGameObject = GameObject.Find ("Draggable-box");
			if (boxGameObject) {
				boxGameObject.GetComponent<Collider2D> ().enabled = isGoDeployMode;
				boxGameObject.tag = "Box";
			}

		}

	}
		
	public void setIsKinematic(bool isKinematic){
		foreach (var board in GameObject.FindGameObjectsWithTag("Board")) {
			var rigidBody2D = board.GetComponent<Rigidbody2D> ();
			rigidBody2D.isKinematic = isKinematic;
			rigidBody2D.useAutoMass = false;
			rigidBody2D.mass = isKinematic ? 1 : 100000; // magic number!
			rigidBody2D.angularDrag = 0.5f;

		}
		foreach (var box in GameObject.FindGameObjectsWithTag("Box")) {
			box.GetComponent<Rigidbody2D> ().isKinematic = isKinematic;
		}
	}

	public void setDraggable(bool active){
		var boards = GameObject.FindGameObjectsWithTag("Board");
		foreach (var board in boards) {
			board.GetComponent<Draggable> ().enabled = active;
		}

		var nails = GameObject.FindGameObjectsWithTag("Nail");
		foreach (var nail in nails) {
			nail.GetComponent<Draggable> ().enabled = active;
		}

		var boxes = GameObject.FindGameObjectsWithTag("Box");
		foreach (var box in boxes) {
			box.GetComponent<Draggable> ().enabled = active;
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

	public void setHingeJoint(bool active){
		var boards = GameObject.FindGameObjectsWithTag ("Board");
		if (active) {
			foreach (var board in boards) {
				var boardScript = board.transform.GetComponent<Draggable> ();
				if (boardScript.dragNailNum == 1) {
					HingeJoint2D boardHJ = board.AddComponent<HingeJoint2D> ();

					boardHJ.connectedAnchor = boardScript.nailPosition;
					boardHJ.anchor = getRelativePosition(board.transform, boardScript.nailPosition);
					boardHJ.enableCollision = active;
				}
				if (boardScript.dragNailNum >= 2) {
					board.GetComponent<Rigidbody2D> ().isKinematic = true;
				}
			}
		} else {
			foreach (var board in boards) {
				Destroy (board.GetComponent<HingeJoint2D> ());
			}
		}
	}




	public Vector3 getRelativePosition(Transform origin, Vector3 position) {
		Vector3 distance = position - origin.position;
		Vector3 relativePosition = Vector3.zero;
		relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
		relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
		relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);

		return relativePosition;
	}


}
