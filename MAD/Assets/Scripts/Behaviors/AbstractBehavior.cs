using UnityEngine;
using System.Collections;

public abstract class AbstractBehavior : MonoBehaviour {

	public Buttons[] inputButtons;

	protected InputState inputState;
	public Rigidbody2D body2d;
	protected CollisionState collisionState;
	public Dog dog = Dog.Max;

	protected virtual void Awake(){
		inputState = GetComponent<InputState> ();
		body2d = GetComponent<Rigidbody2D> ();
		collisionState = GetComponent<CollisionState> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
