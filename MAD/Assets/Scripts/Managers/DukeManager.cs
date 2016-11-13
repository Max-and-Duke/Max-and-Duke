using UnityEngine;
using System.Collections;

public class DukeManager : MonoBehaviour {
	private Rigidbody2D dukeBody;
	private Animator animator;
	// Use this for initialization
	void Start () {
		dukeBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float absX = Mathf.Abs (dukeBody.velocity.x);
		if(absX < 0.001){
			animator.SetInteger ("AnimState", 0);
		}

		if(absX >= 0.01){
			animator.SetInteger ("AnimState", 1);
			Debug.Log (absX);
		}
	
	}
}
