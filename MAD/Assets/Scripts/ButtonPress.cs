using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {

	public Sprite spriteOn;
	public Sprite spriteOff;


	void OnCollisionEnter2D ()
	{
		this.GetComponent<SpriteRenderer> ().sprite = spriteOn;
		Rotate.instance.active = true;

	}

	void OnCollisionExit2D() {
		this.GetComponent<SpriteRenderer> ().sprite = spriteOff;
		Rotate.instance.active = false;
	}


}
