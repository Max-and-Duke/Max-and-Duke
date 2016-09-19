using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	public static Rotate instance;
	public float delay = 2.0f;
	public bool active = false;

	public int rotationDirection = -1; // -1 for clockwise, 1 for anti-clockwise
	public int rotationStep = 10;    // should be less than 90

	private Vector3 currentRotation, targetRotation;

	void Start () {
		instance = this;

		StartCoroutine (objectRotator ());
//		rotateObject ();
	}

	IEnumerator objectRotator() {
		yield return new WaitForSeconds (delay);

		if (active) {
			rotateObject ();
		}
			
		StartCoroutine (objectRotator ());
	}

	void rotateObject()
	{
		currentRotation = gameObject.transform.eulerAngles;
		targetRotation.z = (currentRotation.z + (90 * rotationDirection));
		StartCoroutine (objectRotationAnimation());
	}


	IEnumerator objectRotationAnimation()
	{
		// add rotation step to current rotation.
		currentRotation.z += (rotationStep * rotationDirection);
		gameObject.transform.eulerAngles = currentRotation;

		yield return new WaitForSeconds (0);

		if (((int)currentRotation.z >
			(int)targetRotation.z && rotationDirection < 0)  ||  // for clockwise
			((int)currentRotation.z <  (int)targetRotation.z && rotationDirection > 0)) // for anti-clockwise
		{
			StartCoroutine (objectRotationAnimation());
		}
		else
		{
			gameObject.transform.eulerAngles = targetRotation;
		}
	}
}
