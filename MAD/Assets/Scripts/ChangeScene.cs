using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class ChangeScene : MonoBehaviour {
	public AudioClip buttonClick;

	private static ArrayList buttonNames = 
		new ArrayList{"Reset Button","Next Button","Go to Level 1","Go to Level 2","Go to Level 3"};

	public void ChangeSceneTo(string nextScene) {
		SceneManager.LoadScene (nextScene, LoadSceneMode.Single);

		if (buttonNames.Contains(EventSystem.current.currentSelectedGameObject.name)) {
			Draggable.costSoFar = 0;

			if (SoundManager.instance) {
				SoundManager.instance.playMusicSource ();
			}
		}
		if (SoundManager.instance) {
			SoundManager.instance.PlaySingle (buttonClick);
		}
	}
}
