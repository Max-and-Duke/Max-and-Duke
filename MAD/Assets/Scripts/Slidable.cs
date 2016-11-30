using UnityEngine;
using System.Collections;

public class Slidable : MonoBehaviour {
	public static Slidable instance;
	private bool isOn = false;

	public float timeSpan = 3.0f;
	public Vector3 DefaultPosition;
	public Vector3 ActivePosition;
	public bool On {
		get { 
			return isOn;
		}
		set { 
			Slide (value);
			isOn = value;
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
		transform.position = isOn ? ActivePosition : DefaultPosition;
	}

	private void SlideTo (GameObject gameObject, Vector3 to) {
		// this is just a dummy implementation for now
//		var vector = gameObject.transform.position;
//		vector [1] = positionSlideTo;
		iTween.MoveTo(gameObject, to, timeSpan);
	}

	private void Slide (bool status) {
		SlideTo (gameObject, status ? ActivePosition : DefaultPosition);
	}
}
