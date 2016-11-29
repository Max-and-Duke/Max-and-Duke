using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardManager : MonoBehaviour {
	public Walk maxWalk;
	public Walk dukeWalk;
	public Jump maxJump;
	public Jump dukeJump;
	public static Rigidbody2D maxBody2d ;
	public static Rigidbody2D dukeBody2d ;
	public CollisionState maxCollisionState;
	public CollisionState dukeCollisionState;
	public MaxFaceDirection maxFaceDirection;
	public DukeFaceDirection dukeFaceDirection;

	// Use this for initialization
	void Start () {
	
	}

	void Awake() {
		maxBody2d = maxWalk.body2d;
		dukeBody2d = dukeWalk.body2d;

	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (InputManager.leftKeyDown);
		var val = Input.GetAxis("Horizontal");
		if (val > 0) {
			if (InputManager.curDog == Dog.Max) {
				maxWalk.body2d.velocity = new Vector2 (maxWalk.speed, maxWalk.body2d.velocity.y);
				maxFaceDirection.facingRight = true;
			} else {
				dukeWalk.body2d.velocity = new Vector2 (dukeWalk.speed, dukeWalk.body2d.velocity.y);
				dukeFaceDirection.facingRight = true;
			}
		} else if (val < 0) {
			if (InputManager.curDog == Dog.Max) {
				maxWalk.body2d.velocity = new Vector2 (-maxWalk.speed, maxWalk.body2d.velocity.y);
				maxFaceDirection.facingRight = false;
			} else {
				dukeWalk.body2d.velocity = new Vector2 (-dukeWalk.speed, dukeWalk.body2d.velocity.y);
				dukeFaceDirection.facingRight = false;
			}
		} 

		var vertical = Input.GetAxis("Vertical");

		if (vertical > 0) {
			if (InputManager.curDog == Dog.Max && InputManager.maxCanClimb) {
				maxJump.body2d.gravityScale = 0;
				maxJump.body2d.transform.position = new Vector3 (maxBody2d.transform.position.x, maxBody2d.transform.position.y + 6, maxBody2d.transform.position.z);
			} else if (InputManager.curDog == Dog.Duke && InputManager.dukeCanClimb) {
				dukeJump.body2d.gravityScale = 0;
				dukeJump.body2d.transform.position = new Vector3 (dukeBody2d.transform.position.x, dukeBody2d.transform.position.y + 6, dukeBody2d.transform.position.z);
			}
			if (InputManager.curDog == Dog.Max && !InputManager.maxCanClimb) {
				if (maxCollisionState.standing)
					maxJump.body2d.velocity = new Vector2 (maxJump.body2d.velocity.x, maxJump.jumpSpeed);
			} else if (InputManager.curDog == Dog.Duke && !InputManager.dukeCanClimb) {
				if (dukeCollisionState.standing) {
					dukeJump.body2d.velocity = new Vector2 (dukeJump.body2d.velocity.x, dukeJump.jumpSpeed);
				}
			}


		} else if (vertical < 0) {
			if (InputManager.curDog == Dog.Max && InputManager.maxCanClimb) {
				maxWalk.body2d.gravityScale = 0;
				maxWalk.body2d.transform.position = new Vector3(maxBody2d.transform.position.x, maxBody2d.transform.position.y-4, maxBody2d.transform.position.z);
			} else if (InputManager.curDog == Dog.Duke && InputManager.dukeCanClimb) {
				dukeWalk.body2d.gravityScale = 0;
				dukeWalk.body2d.transform.position = new Vector3(dukeBody2d.transform.position.x, dukeBody2d.transform.position.y-4, dukeBody2d.transform.position.z);

			}
		}

		var switchDog = Input.GetAxis ("Fire1");
		GameObject button = GameObject.Find ("Switch Player");
		Image im = button.GetComponent<Image> ();
		AudioSource audio = button.GetComponent<AudioSource> ();
		DogSprites ds = button.GetComponent<DogSprites> ();
		if (switchDog > 0) {
			if (InputManager.curDog == Dog.Max) {
				InputManager.curDog = Dog.Duke;
				im.sprite = ds.dukeSprite;
				audio.clip = (AudioClip)Resources.Load ("dukebark");
				audio.Play ();
			} else {
				InputManager.curDog = Dog.Max;
				im.sprite = ds.maxSprite;
				audio.clip = (AudioClip)Resources.Load ("maxbark");
				audio.Play ();
			}
		}
	}
}
