using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public void ChangeSceneTo(string nextScene) {
		SceneManager.LoadScene (nextScene);
	}

}
