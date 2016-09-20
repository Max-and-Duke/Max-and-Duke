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
	public CollisionState maxCollisionState;
	public CollisionState dukeCollisionState;
	public static bool maxCanClimb = false;
	public static bool dukeCanClimb = false;

	public Walk maxWalk;
	public Walk dukeWalk;

	public Jump maxJump;
	public Jump dukeJump;

	public static Rigidbody2D maxBody2d ;
	public static Rigidbody2D dukeBody2d ;

	private bool leftKeyDown = false;
	private bool rightKeyDown = false;
	private bool upKeyDown = false;
	private bool downKeyDown = false;

//	public Dog dog = Dog.Max;
//	public int count = 0;
	// Use this for initialization
	void Start () {
		maxBody2d = maxWalk.body2d;
		dukeBody2d = dukeWalk.body2d;
	}

	public void pressLeft(){
		leftKeyDown = true;
	}

	public void releaseLeft(){
		leftKeyDown = false;
	}

	public void pressRight(){
		rightKeyDown = true;
	}

	public void releaseRight(){
		rightKeyDown = false;
	}


	public void pressUp(){
		upKeyDown = true;
	}

	public void releaseUp(){
		upKeyDown = false;
	}

	public void pressDown(){
		downKeyDown = true;
	}

	public void releasDown(){
		downKeyDown = false;
	}

	public void switchDog(){
		if (curDog == Dog.Max) {
			curDog = Dog.Duke;
		} else {
			curDog = Dog.Max;
		}
	}

	public void jump(){
		Debug.Log ("Clicked");
		if (curDog == Dog.Max) {
			if (maxCollisionState.standing)
				maxJump.body2d.velocity = new Vector2 (maxJump.body2d.velocity.x, maxJump.jumpSpeed);
		}
		else {
			if (dukeCollisionState.standing) {
				dukeJump.body2d.velocity = new Vector2 (dukeJump.body2d.velocity.x, dukeJump.jumpSpeed);
			}
		}
	}


//	private void OnGUI(){
//
//		if (GUI.Button (new Rect (15, 15, 50, 50), "left")) {
//			if (curDog == Dog.Max) {
//				maxWalk.body2d.velocity = new Vector2 (-maxWalk.speed, maxWalk.body2d.velocity.y);
//			} else {
//				dukeWalk.body2d.velocity = new Vector2 (-dukeWalk.speed, dukeWalk.body2d.velocity.y);
//			}
//		
//		}
//
//		if (GUI.Button (new Rect (80, 15, 50, 50), "right")) {
//			if (curDog == Dog.Max) {
//				maxWalk.body2d.velocity = new Vector2 (maxWalk.speed, maxWalk.body2d.velocity.y);
//			} else {
//				dukeWalk.body2d.velocity = new Vector2 (dukeWalk.speed, dukeWalk.body2d.velocity.y);
//			}
//		}
//
//
//
//		if (GUI.Button (new Rect (900, 15, 50, 50), "jump")) {
//			if (curDog == Dog.Max)
//				maxJump.body2d.velocity = new Vector2 (maxJump.body2d.velocity.x, maxJump.jumpSpeed);
//			else {
//				dukeJump.body2d.velocity = new Vector2 (dukeJump.body2d.velocity.x, dukeJump.jumpSpeed);
//			}
//		}
//	
//		if (GUI.Button (new Rect (450, 15, 50, 50), "switch")) {
//			if (curDog == Dog.Max) {
//				curDog = Dog.Duke;
//			} else {
//				curDog = Dog.Max;
//			}
//		}
//	}
	
	// Update is called once per frame
	void Update () {
		
//		foreach (var input in inputs) {
////			if (input.button == Buttons.D && input.value && count > 100) {
////				count = 0;
////
////				if (dog == Dog.Max) {
////					dog = Dog.Duke;
////				} else {
////					dog = Dog.Max;
////				}
////			} else
////				count++;
////
////			if(inputState.dog == dog)
//				inputState.SetButtonValue(input.button, input.value);
//		}

		if (leftKeyDown) {
			if (curDog == Dog.Max) {
				maxWalk.body2d.velocity = new Vector2 (-maxWalk.speed, maxWalk.body2d.velocity.y);
			} else {
				dukeWalk.body2d.velocity = new Vector2 (-dukeWalk.speed, dukeWalk.body2d.velocity.y);
			}
		}

		if (rightKeyDown) {
			if (curDog == Dog.Max) {
				maxWalk.body2d.velocity = new Vector2 (maxWalk.speed, maxWalk.body2d.velocity.y);
			} else {
				dukeWalk.body2d.velocity = new Vector2 (dukeWalk.speed, dukeWalk.body2d.velocity.y);
			}
		}

		if (upKeyDown) {

//			Debug.Log ("pressed");
//			if (curDog == Dog.Max) {
//				if (maxCollisionState.standing && !maxCanClimb)
//					maxJump.body2d.velocity = new Vector2 (maxJump.body2d.velocity.x, maxJump.jumpSpeed);
//				else if (maxCanClimb) {
//					maxJump.body2d.gravityScale = 0;
//					maxJump.body2d.velocity = new Vector2 (0, 0);
//					maxBody2d.transform.position = new Vector3(maxBody2d.transform.position.x, maxBody2d.transform.position.y+3, maxBody2d.transform.position.z);
//
//
//				}
//			}
//			else {
//				if (dukeCollisionState.standing && !dukeCanClimb) {
//					dukeJump.body2d.velocity = new Vector2 (dukeJump.body2d.velocity.x, dukeJump.jumpSpeed);
//				}
//				else if (dukeCanClimb) {
//					dukeJump.body2d.gravityScale = 0;
//					dukeJump.body2d.velocity = new Vector2 (0, 0);
//					dukeBody2d.transform.position = new Vector3(dukeBody2d.transform.position.x, dukeBody2d.transform.position.y+3, dukeBody2d.transform.position.z);
//
//				}
//			}

			if (curDog == Dog.Max && maxCanClimb) {
				maxJump.body2d.gravityScale = 0;
//				maxJump.body2d.velocity = new Vector2 (0, 0);
				maxBody2d.transform.position = new Vector3 (maxBody2d.transform.position.x, maxBody2d.transform.position.y + 3, maxBody2d.transform.position.z);
			} else if (curDog == Dog.Duke && dukeCanClimb) {
				dukeJump.body2d.gravityScale = 0;
//				dukeJump.body2d.velocity = new Vector2 (0, 0);
				dukeBody2d.transform.position = new Vector3(dukeBody2d.transform.position.x, dukeBody2d.transform.position.y+3, dukeBody2d.transform.position.z);

			}

		}

		if (downKeyDown) {
			Debug.Log (dukeCanClimb);
			if (curDog == Dog.Max && maxCanClimb) {
				maxWalk.body2d.gravityScale = 0;
				maxJump.body2d.velocity = new Vector2 (0, 0);
				maxBody2d.transform.position = new Vector3(maxBody2d.transform.position.x, maxBody2d.transform.position.y-2, maxBody2d.transform.position.z);
			} else if (curDog == Dog.Duke && dukeCanClimb) {
				dukeWalk.body2d.gravityScale = 0;
				dukeJump.body2d.velocity = new Vector2 (0, -dukeJump.jumpSpeed);
				dukeBody2d.transform.position = new Vector3(dukeBody2d.transform.position.x, dukeBody2d.transform.position.y-2, dukeBody2d.transform.position.z);

			}
		}
	}

}
