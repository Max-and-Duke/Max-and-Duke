using UnityEngine;
using System.Collections;

public class Slidable : MonoBehaviour {
	public static Slidable instance;
	private bool isOpen = false;

	private Vector3 openPosition;
	private Vector3 closePosition;
	public bool Open {
		get { 
			return isOpen;
		}
		set { 
			Slide (value);
			isOpen = value;
		}
	}

	// Use this for initialization
	void Awake () {
		//Check if there is already an instance of SoundManager
		if (instance == null) {
			//if not, set it to this.
			instance = this; 
		}
		//If instance already exists:
		else if (instance != this) {
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);
		}
	}

	void Start () {
		openPosition = new Vector3 (100, 200, 350);
		closePosition = new Vector3 (400, 200, 350);
		transform.position = isOpen ? openPosition : closePosition;
	}

	private void SlideTo (GameObject gameObject, Vector3 to) {
		// this is just a dummy implementation for now
//		var vector = gameObject.transform.position;
//		vector [1] = positionSlideTo;
		iTween.MoveTo(gameObject, to, 1.0f);
	}

	private void Slide (bool status) {
		SlideTo (gameObject, status ? openPosition : closePosition);
	}
}
