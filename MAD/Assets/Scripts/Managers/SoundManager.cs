using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
	private static bool audioPlay = true;
	public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
	public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
	private static AudioSource tmpMusicSource;


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
//		tmpMusicSource = musicSource;
	}

	public void stopMusicSource(){
		musicSource.Stop ();
	}

	public void playMusicSource(){
		musicSource.Play ();
	}


	public void stopAudio(){
		var sceneName = SceneManager.GetActiveScene ().name;
//		var audio = instance.GetComponent<AudioSource> ();
		if (sceneName == "ToolboxAndCostAndMode") {
			
			if (GameObject.Find ("Background Image").GetComponent<AudioSource> ().clip != null && musicSource.clip != GameObject.Find ("Background Image").GetComponent<AudioSource> ().clip) {
//				tmpMusicSource = musicSource;
				musicSource.Stop ();

				musicSource.clip = GameObject.Find ("Background Image").GetComponent<AudioSource> ().clip;
				musicSource.Play ();
				Debug.Log ("hha");
			}
			audioPlay = false;
		} else if (audioPlay == false) {

			musicSource.Stop ();
			musicSource.clip = (AudioClip)Resources.Load("beginningMusic");
			musicSource.Play ();
			audioPlay = true;
		}

	}



	public void PlaySingle(AudioClip clip)
	{
		//Set the clip of our efxSource audio source to the clip passed in as a parameter.
		efxSource.clip = clip;

		//Play the clip.
		efxSource.Play ();
	}


	void Start (){
		
	}

	// Update is called once per frame
	void Update () {
		stopAudio ();
	}
}
