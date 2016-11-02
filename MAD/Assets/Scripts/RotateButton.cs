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

	private int dragNum = 0;



	void Start () {
//		v =  (itemBeingRotate.transform.position - nailStaticPosition);;
	
	}
	
	public void OnBeginDrag(PointerEventData eventData) {
		var draggableScript = this.transform.parent.GetComponent<Draggable>();
		dragNum = draggableScript.dragNailNum;
		if (dragNum == 1) {
			nailPosition = draggableScript.nailPosition;
//			Debug.Log ("nailPosition is " + nailPosition);
//			Debug.Log ("this is a nail");
		} else {
			nailPosition = this.transform.parent.transform.position;
//			Debug.Log ("parent name is:" + this.transform.parent.name);
//			Debug.Log ("parent pivot position is" + this.transform.parent.transform.position);
		}

		boardPos = this.transform.parent.transform.position;
//		Debug.Log ("boardpos is" + boardPos);
		//Debug.Log ("begindrag boardposition" + this.transform.parent.transform.position);

		pivotOffset = Camera.main.ScreenToWorldPoint (Input.mousePosition) - this.transform.position;
		pivotOffset.z = 0;
		//v = this.transform.parent.transform.position - nailPosition;
		var distBarToNail = Vector3.Distance (this.transform.position, nailPosition);
		var distPivotToNail = Vector3.Distance (this.transform.parent.transform.position, nailPosition);

		if (Mathf.Abs(distPivotToNail) < 1) {
			v = new Vector3 (0, 0, 0);
		}
		else if (Mathf.Abs (distBarToNail) > Mathf.Abs (distPivotToNail)) {
			//v = nailPosition - this.transform.parent.transform.position;
			v = new Vector3 (208, 0, 0);
		} else if (Mathf.Abs (distBarToNail) < Mathf.Abs (distPivotToNail)) {
//			v = this.transform.parent.transform.position - nailPosition;
			v = new Vector3 (-208, 0, 0);
		}

//		Debug.Log()
	}

	public void OnDrag(PointerEventData eventData) {
		if (dragNum <= 1) {
			Vector3 pos = Camera.main.WorldToScreenPoint (nailPosition);
			Vector3 dir = Input.mousePosition - pos;
			dir.z = 0;

			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg; 
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);

			itemBeingRotate.transform.position = nailPosition + q * v;
			itemBeingRotate.transform.rotation = q;
		}

	}

	public void OnEndDrag(PointerEventData eventData) {
		//Debug.Log ("onEndDrag" + this.transform.parent.transform.position);
//		Debug.Log ("OnEndDrag");
//		itemBeingRotate.transform.parent.transform.rotation=Quaternion.identity;
	}
}
