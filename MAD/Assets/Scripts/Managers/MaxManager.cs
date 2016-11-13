using UnityEngine;
using System.Collections;

public class MaxManager : MonoBehaviour {

	private Rigidbody2D maxBody;
	private Animator animator;
	// Use this for initialization
	void Start () {
		maxBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		float absX = Mathf.Abs (maxBody.velocity.x);
		if(absX == 0){
			animator.SetInteger ("AnimState", 0);
		}

		if (absX > 0) {
			animator.SetInteger ("AnimState", 1);
		}

//		var z = Mathf.Clamp (transform.rotation.z + 1, -60, 60);
//
//		transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
			
			
	}
}
