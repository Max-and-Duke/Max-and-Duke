using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	private Vector2 startPosition;
	private Vector2 inputPosition;
	private Vector2 touchOffset;
	private Vector2 pivotOffset;
	private Transform startParent;
	public Text countTool;
	public GameObject panel;
	private bool collide = false;

	public void OnBeginDrag(PointerEventData eventData) {
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		inputPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		pivotOffset = inputPosition - startPosition;
		//startParent = transform.parent;
		Debug.Log (startPosition);
		//Debug.Log ("OnBeginDrag");
	}

	void OnTriggerEnter2D(Collider2D collider) {
		//		if (collider.gameObject.name == "Board") {
		Debug.Log ("zhuang");
		//		}

	}

	public void OnDrag(PointerEventData eventData) {
		//Debug.Log ("OnDrag");
		inputPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//		RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 1f);
//		if (touches.Length > 0)
//		{
//			var hit = touches[0];
//			if (hit.transform != null)
//			{
//				
////				itemBeingDragged = hit.transform.gameObject;
//				touchOffset = (Vector2)hit.transform.position - inputPosition;
//				//                    draggedObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
//			}
//		}

		transform.position = inputPosition - pivotOffset;
//		itemBeingDragged.transform.position = inputPosition;

//		this.transform.position = new Vector3(Input.mousePosition.x - Screen.width / 2  * PixelPerfectCamera.pixelToUnits, Input.mousePosition.y - Screen.height / 2 * PixelPerfectCamera.pixelToUnits, 0);
	}
		

	public void OnEndDrag(PointerEventData eventData) {
		itemBeingDragged = null;

//		if (transform.renderer.bounds.Intersects(object2.renderer.bounds)) {
//			// Do some stuff
//		}
		//Debug.Log ("OnEndDrag");
	}



}
