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

	private float anglePrev;

	void Start () {
//		v =  (itemBeingRotate.transform.position - nailStaticPosition);;
	
	}
	
	public void OnBeginDrag(PointerEventData eventData) {
		anglePrev = itemBeingRotate.transform.eulerAngles.z;
		var draggableScript = this.transform.parent.GetComponent<Draggable>();
		if (draggableScript.dragNailNum == 1) {
			nailPosition = draggableScript.nailPosition;
			Debug.Log ("nailPosition is " + nailPosition);
//			Debug.Log ("this is a nail");
		} else {
			nailPosition = this.transform.parent.transform.position;
//			Debug.Log ("parent name is:" + this.transform.parent.name);
//			Debug.Log ("parent pivot position is" + this.transform.parent.transform.position);
		}

		boardPos = this.transform.parent.transform.position;
		Debug.Log ("boardpos is" + boardPos);
		//Debug.Log ("begindrag boardposition" + this.transform.parent.transform.position);

		pivotOffset = Camera.main.ScreenToWorldPoint (Input.mousePosition) - this.transform.position;
		pivotOffset.z = 0;
		//v = this.transform.parent.transform.position - nailPosition;
		var tmp = this.transform.position - nailPosition;
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

		//Vector3 pos = Camera.main.WorldToScreenPoint (itemBeingRotate.transform.position);
	    Vector3 pos = Camera.main.WorldToScreenPoint (nailPosition);
		//Vector3 dir = Input.mousePosition - pos;
		Vector3 dir = Input.mousePosition - pos;
		dir.z = 0;
//		if (dir.x < 0) {
//			dir.x= dir.x * (-1);
//		}

		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg; 


		//Debug.Log (">>>>>>" + angle.ToString() + "\n>>>>>>" + anglePrev);
//		Debug.Log (nailPosition);
		//angle = angle - anglePrev;
		//Debug.Log("ondrag" + v);
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);

		itemBeingRotate.transform.position = nailPosition + q * v;
		itemBeingRotate.transform.rotation = q;


	}

	public void OnEndDrag(PointerEventData eventData) {
		//Debug.Log ("onEndDrag" + this.transform.parent.transform.position);
//		Debug.Log ("OnEndDrag");
//		itemBeingRotate.transform.parent.transform.rotation=Quaternion.identity;
	}
}
