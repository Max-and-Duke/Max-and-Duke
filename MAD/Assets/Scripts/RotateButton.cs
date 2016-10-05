using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class RotateButton : MonoBehaviour,IBeginDragHandler,IDragHandler, IEndDragHandler{

	public GameObject itemBeingRotate;
	private Vector3 v;
//	public Vector3 nailStaticPosition;
	public Vector3 nailPosition;
	private Vector3 pivotOffset;
	// Use this for initialization
	public Vector3 boardPos;

	void Start () {
//		v =  (itemBeingRotate.transform.position - nailStaticPosition);;
	
	}
	
	public void OnBeginDrag(PointerEventData eventData) {

		var draggableScript = this.transform.parent.GetComponent<Draggable>();
		if (draggableScript.dragNailNum == 1) {
			nailPosition = draggableScript.nailPosition;
//			Debug.Log ("this is a nail");
		} else {
			nailPosition = this.transform.parent.transform.position;
//			Debug.Log ("parent name is:" + this.transform.parent.name);
//			Debug.Log ("parent pivot position is" + this.transform.parent.transform.position);
		}

		boardPos = this.transform.parent.transform.position;

		Debug.Log ("begindrag boardposition" + this.transform.parent.transform.position);

		pivotOffset = Camera.main.ScreenToWorldPoint (Input.mousePosition) - this.transform.position;
		pivotOffset.z = 0;
		var tmp = this.transform.position - nailPosition;
		if (tmp.x < 0) {
			v = nailPosition - this.transform.parent.transform.position;
		} else {
			v = this.transform.parent.transform.position - nailPosition;
		}
	}

	public void OnDrag(PointerEventData eventData) {

		//Vector3 pos = Camera.main.WorldToScreenPoint (itemBeingRotate.transform.position);
	    Vector3 pos = Camera.main.WorldToScreenPoint (nailPosition);
		//Vector3 dir = Input.mousePosition - pos;
		Vector3 dir = Input.mousePosition - pos;
		dir.z = 0;
//		if (dir.x < 0) {
//			dir.x= dir.x * (-1);
//		}

		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
//		Debug.Log (angle);
//		Debug.Log (nailPosition);
		Debug.Log("ondrag" + v);
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
		itemBeingRotate.transform.position = nailPosition + q * v;
		itemBeingRotate.transform.rotation = q;


	}

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log ("onEndDrag" + this.transform.parent.transform.position);
//		Debug.Log ("OnEndDrag");
//		itemBeingRotate.transform.parent.transform.rotation=Quaternion.identity;
	}
}
