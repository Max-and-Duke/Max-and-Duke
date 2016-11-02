using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class ChangeScene : MonoBehaviour {
	public AudioClip buttonClick;

	public void ChangeSceneTo(string nextScene) {
		
		if (EventSystem.current.currentSelectedGameObject.name == "Reset Button") {
			Draggable.boardNum = 2;
			Draggable.nailNum = 4;
			Draggable.costSoFar = 0;
		}
		SoundManager.instance.PlaySingle (buttonClick);
		SceneManager.LoadScene (nextScene, LoadSceneMode.Single);
	}

}
