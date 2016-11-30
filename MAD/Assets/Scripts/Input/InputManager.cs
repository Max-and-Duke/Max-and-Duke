using UnityEngine;
using UnityEngine.UI;
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
	public static Dog curDog = Dog.Max;
	public CollisionState maxCollisionState;
	public CollisionState dukeCollisionState;
	public MaxFaceDirection maxFaceDirection;
	public DukeFaceDirection dukeFaceDirection;
	public static bool maxCanClimb = false;
	public static bool dukeCanClimb = false;
	public static Vector3 maxPosition; // = new Vector3 (-490, 230, 350);
	public static Vector3 dukePosition; // = new Vector3 (-730, 230, 350);


	public static float GRAVITY = 120f;


	public Walk maxWalk;
	public Walk dukeWalk;

	public Jump maxJump;
	public Jump dukeJump;

	public static Rigidbody2D maxBody2d ;
	public static Rigidbody2D dukeBody2d ;

	public static bool leftKeyDown;
	public static bool rightKeyDown;
	public static bool upKeyDown;
	public static bool downKeyDown;

	public static InputManager instance = null;

	void Awake() {
		//Check if there is already an instance of SoundManager
		if (instance == null) {
			//if not, set it to this.
			instance = this; 
		}
		//If instance already exists:
		else if (instance != this) {
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);
		}

		initKeyState ();	

		maxBody2d = maxWalk.body2d;
		dukeBody2d = dukeWalk.body2d;
		maxBody2d.gravityScale = GRAVITY;
		dukeBody2d.gravityScale = GRAVITY;
	}

	void Start() {
		maxPosition = DataManager.instance.data.maxPosition; 
		dukePosition = DataManager.instance.data.dukePosition;
	}

	public void initKeyState() {
		leftKeyDown = false;
		rightKeyDown = false;
     	upKeyDown = false;
     	downKeyDown = false;
	}

	public void PressLeft(){
		if (curDog == Dog.Max) {
			maxFaceDirection.facingRight = false;
		} else {
			dukeFaceDirection.facingRight = false;
		}
		leftKeyDown = true;
	}

	public void ReleaseLeft(){
		leftKeyDown = false;
	}

	public void PressRight(){
		if (curDog == Dog.Max) {
			maxFaceDirection.facingRight = true;
		} else {
			dukeFaceDirection.facingRight = true;
		}
		rightKeyDown = true;
	}

	public void ReleaseRight(){
		rightKeyDown = false;
	}


	public void PressUp(){
		upKeyDown = true;
	}

	public void ReleaseUp(){
		upKeyDown = false;
	}

	public void PressDown(){
		downKeyDown = true;
	}

	public void ReleasDown(){
		downKeyDown = false;
	}

	public void SwitchDog(){
		GameObject button = GameObject.Find ("Switch Player");
		Image im = button.GetComponent<Image> ();
		AudioSource audio = button.GetComponent<AudioSource> ();
		DogSprites ds = button.GetComponent<DogSprites> ();

		if (curDog == Dog.Max) {
			curDog = Dog.Duke;
			im.sprite = ds.dukeSprite;
			audio.clip = (AudioClip)Resources.Load ("dukebark");
			audio.Play();
		} else {
			curDog = Dog.Max;
			im.sprite = ds.maxSprite;
			audio.clip = (AudioClip)Resources.Load ("maxbark");
			audio.Play();
		}
	}

	public void Jump(){
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


	public void RePosition(){
		if (SoundManager.instance) {
			SoundManager.instance.playMusicSource ();
		}

		// reset facing
		maxFaceDirection.facingRight = true;
		dukeFaceDirection.facingRight = true;

		// reset position
		maxBody2d.transform.position = maxPosition;
		dukeBody2d.transform.position = dukePosition;

		// reset velocity
		maxBody2d.velocity = new Vector2 (0, 0);
		dukeBody2d.velocity = new Vector2 (0, 0);
	}



	void Update () {

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


			if (curDog == Dog.Max && maxCanClimb) {
				maxJump.body2d.gravityScale = 0;
				maxBody2d.transform.position = new Vector3 (maxBody2d.transform.position.x, maxBody2d.transform.position.y + 6, maxBody2d.transform.position.z);
			} else if (curDog == Dog.Duke && dukeCanClimb) {
				dukeJump.body2d.gravityScale = 0;
				dukeBody2d.transform.position = new Vector3(dukeBody2d.transform.position.x, dukeBody2d.transform.position.y+6, dukeBody2d.transform.position.z);

			}

		}

		if (downKeyDown) {
			if (curDog == Dog.Max && maxCanClimb) {
				maxWalk.body2d.gravityScale = 0;
				maxBody2d.transform.position = new Vector3(maxBody2d.transform.position.x, maxBody2d.transform.position.y-4, maxBody2d.transform.position.z);
			} else if (curDog == Dog.Duke && dukeCanClimb) {
				dukeWalk.body2d.gravityScale = 0;
				dukeBody2d.transform.position = new Vector3(dukeBody2d.transform.position.x, dukeBody2d.transform.position.y-4, dukeBody2d.transform.position.z);

			}
		}
	}

}
