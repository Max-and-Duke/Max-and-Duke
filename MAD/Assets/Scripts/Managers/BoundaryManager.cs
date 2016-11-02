using UnityEngine;
using System.Collections;

public class BoundaryManager : MonoBehaviour {

	public LevelFailedPanel levelFailedPanel;
	public InputManager inputManager;

	private Vector3 world;
	private float halfSize;

	void Awake () {
		levelFailedPanel.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		world = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
		halfSize = GetComponent<Renderer>().bounds.size.x / 2;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 characterPos = transform.position;
		if (characterPos.x < halfSize - world.x) 
			characterPos.x = halfSize - world.x;
		if (characterPos.x > world.x - halfSize)
			characterPos.x = world.x - halfSize;
		// lower bound
		if (characterPos.y < -halfSize + 2 * world.y) {
			characterPos.y = -halfSize + 2 * world.y;
			inputManager.initKeyState ();
//			TriggerLevelFailedPanel ();
		}
		// upper bound
//		if (characterPos.y > -world.y - halfSize)
//			characterPos.y = -world.y - halfSize;

		transform.position = characterPos;
	}

	private void TriggerLevelFailedPanel() {
		levelFailedPanel.Choice ();
	}
}
