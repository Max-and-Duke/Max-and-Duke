using UnityEngine;
using System.Collections;

public enum Buttons{
	Right,
	Left,
	Up,
	Down,
	A,
	B,
	D
}

public enum Dog{
	Max,
	Duke
}
	
	
public enum Condition{
	GreaterThan,
	LessThan
}

[System.Serializable]
public class InputAxisState{
	public string axisName;
	public float offValue;
	public Buttons button;
	public Condition condition;

	public bool value{

		get{
			var val = Input.GetAxis(axisName);

			switch(condition){
			case Condition.GreaterThan:
				return val > offValue;
			case Condition.LessThan:
				return val < offValue;
			}

			return false;
		}

	}
}

public class InputManager : MonoBehaviour {

	public InputAxisState[] inputs;
	public InputState inputState;
	public Dog curDog = Dog.Max;

	public Walk maxWalk;
	public Walk dukeWalk;

	public Jump maxJump;
	public Jump dukeJump;

//	public Dog dog = Dog.Max;
//	public int count = 0;
	// Use this for initialization
	void Start () {
	
	}

	private void OnGUI(){

		if (GUI.Button (new Rect (15, 15, 50, 50), "left")) {
			if (curDog == Dog.Max) {
				maxWalk.body2d.velocity = new Vector2 (-maxWalk.speed, maxWalk.body2d.velocity.y);
			} else {
				dukeWalk.body2d.velocity = new Vector2 (-dukeWalk.speed, dukeWalk.body2d.velocity.y);
			}
		
		}

		if (GUI.Button (new Rect (80, 15, 50, 50), "right")) {
			if (curDog == Dog.Max) {
				maxWalk.body2d.velocity = new Vector2 (maxWalk.speed, maxWalk.body2d.velocity.y);
			} else {
				dukeWalk.body2d.velocity = new Vector2 (dukeWalk.speed, dukeWalk.body2d.velocity.y);
			}
		}



		if (GUI.Button (new Rect (900, 15, 50, 50), "jump")) {
			if (curDog == Dog.Max)
				maxJump.body2d.velocity = new Vector2 (maxJump.body2d.velocity.x, maxJump.jumpSpeed);
			else {
				dukeJump.body2d.velocity = new Vector2 (dukeJump.body2d.velocity.x, dukeJump.jumpSpeed);
			}
		}
	
		if (GUI.Button (new Rect (450, 15, 50, 50), "switch")) {
			if (curDog == Dog.Max) {
				curDog = Dog.Duke;
			} else {
				curDog = Dog.Max;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var input in inputs) {
//			if (input.button == Buttons.D && input.value && count > 100) {
//				count = 0;
//
//				if (dog == Dog.Max) {
//					dog = Dog.Duke;
//				} else {
//					dog = Dog.Max;
//				}
//			} else
//				count++;
//
//			if(inputState.dog == dog)
				inputState.SetButtonValue(input.button, input.value);
		}
	}
}
