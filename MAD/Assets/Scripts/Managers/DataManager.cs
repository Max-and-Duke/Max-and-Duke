using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DataManager : MonoBehaviour {
	public static DataManager instance = null;
	public  SceneData data;

	void Awake() {
		if (instance == null) {
			//if not, set it to this.
			instance = this; 
		}
		//If instance already exists:
		else if (instance != this) {
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);
		}

		/// for loading json from outside file, 
		/// see http://pressonegames.com/parsing-json-files-in-unity/
		TextAsset asset = Resources.Load(GetFileName()) as TextAsset; 
		if (asset) {
			data = JsonUtility.FromJson<SceneData> (asset.text);
		}
		Draggable.boardNum = data.numBoard;
		Draggable.nailNum = data.numNail;
		Draggable.boxNum = data.numBox;

	}

	private string GetFileName() {
		Scene scene = SceneManager.GetActiveScene();
		return scene.name.Split(' ')[1]; // level number
	}
}


