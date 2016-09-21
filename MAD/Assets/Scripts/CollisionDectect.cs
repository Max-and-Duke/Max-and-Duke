using UnityEngine;
using System.Collections;

public class CollisionDectect : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log ("Enter~");
		if (collider.gameObject.name == "Draggable-board1") {
			Debug.Log ("board zhuang");
		}

		if (collider.gameObject.name == "Nail") {
			Debug.Log ("nail zhuang");
		}
	}
}
