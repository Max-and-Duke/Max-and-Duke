using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
//	public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
//	public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.


	void Awake ()
	{
		//Check if there is already an instance of SoundManager
		if (instance == null)
			//if not, set it to this.
			instance = this;
		//If instance already exists:
		else if (instance != this)
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}
		
	// Update is called once per frame
	void Update () {
	
	}
}
