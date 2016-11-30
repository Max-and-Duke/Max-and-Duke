using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {

	public Sprite spriteOn;
	public Sprite spriteOff;


	void OnCollisionEnter2D ()
	{
		this.GetComponent<SpriteRenderer> ().sprite = spriteOn;
		if (Rotate.instance) {
			Rotate.instance.active = true;
		}
		if (Slidable.instance) {
			Slidable.instance.Open = true;
		}
	}

	void OnCollisionExit2D() {
		this.GetComponent<SpriteRenderer> ().sprite = spriteOff;
		if (Rotate.instance) {
			Rotate.instance.active = false;
		}
		if (Slidable.instance) {
			Slidable.instance.Open = false;
		}
	}


}
