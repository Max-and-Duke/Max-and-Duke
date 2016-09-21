using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class RotateButton : MonoBehaviour,IBeginDragHandler,IDragHandler, IEndDragHandler{

	public GameObject itemBeingRotate;

	// Use this for initialization

	void Start () {
	
	}
	
	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log ("OnBeginDrag");
//		var children = GetComponentsInChildren<Image>();
//		foreach( Image child in children){
//			Debug.Log(child);
//			if (child.name == "RotateButton") {
//				Debug.Log ("hah");
//				child.enabled = true;
//			}
//		}
	}

	public void OnDrag(PointerEventData eventData) {
		Debug.Log ("OnDrag");
		// the hack way !!
//		this.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y,0)) - new Vector3(0,0,-10);
		//this.transform.position = Input.mousePosition;
		Vector3 pos = Camera.main.WorldToScreenPoint(itemBeingRotate.transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
		itemBeingRotate.transform.rotation = q;
	}

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log ("OnEndDrag");
	}
}
