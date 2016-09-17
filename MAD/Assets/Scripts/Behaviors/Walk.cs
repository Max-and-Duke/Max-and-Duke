using UnityEngine;
using System.Collections;

public class Walk : AbstractBehavior {

	public float speed = 50f;
	public float runMultiplier = 2f;
	public bool running;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		running = false;

		var right = inputState.GetButtonValue (inputButtons [0]);
		var left = inputState.GetButtonValue (inputButtons [1]);
		var run = inputState.GetButtonValue (inputButtons [2]);

		if (right || left) {

			var tmpSpeed = speed;

			if(run && runMultiplier > 0){
				tmpSpeed *= runMultiplier;
				running = true;
			}

			var velX = tmpSpeed * (float)inputState.direction;

			body2d.velocity = new Vector2(velX, body2d.velocity.y);

		}
	}

	private void OnGUI(){
		if (GUI.Button (new Rect (15, 15, 50, 50), "left")) {
			body2d.velocity = new Vector2(0f - (float)speed, body2d.velocity.y);
		}

		if (GUI.Button (new Rect (80, 15, 50, 50), "right")) {
			body2d.velocity = new Vector2((float)speed, body2d.velocity.y);
		}
	}
}
