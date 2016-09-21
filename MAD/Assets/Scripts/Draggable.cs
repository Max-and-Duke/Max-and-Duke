using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public GameObject itemBeingDragged;
	public Sprite itemOutSidePanel;
	public Text countTool;
	public GameObject panel;
	public bool collide = false;

	private static int boardNum = 2;
	private static int nailNum = 4;
	private float originImageWidth;
	private float originImageHeight;

	private float boardWidth = 500f;
	private float boardHeight = 60f;
	private float nailWidth = 50f;
	private float nailHeight = 50f;
	private BoxCollider2D sceneCollider;

	private Vector2 startPosition;
	private Vector2 inputPosition;
	private Vector2 pivotOffset;
	private Transform startParent;
	private Vector2 oneClickStartPosition;
	private Image image;
	private Sprite originImageSprite;

	void Start(){
		image = itemBeingDragged.GetComponent<Image>();
		originImageWidth = image.rectTransform.rect.width;
		originImageHeight = image.rectTransform.rect.height;
		//print (originImage.rectTransform.rect.height);
		originImageSprite = image.sprite;
		startPosition = itemBeingDragged.transform.position;
		if (itemBeingDragged.tag == "Board") {
			countTool.text = "x " + boardNum;
			var children = itemBeingDragged.GetComponentsInChildren<Image>();
			foreach( Image child in children){
				//Debug.Log(child);
				if (child.name == "RotateButton") {
					//Debug.Log ("hah");
					child.enabled = false;
				}
			}
		} else if (itemBeingDragged.tag == "Nail"){
			countTool.text = "x " + nailNum;
		}



		
	}


	public void OnBeginDrag(PointerEventData eventData) {
		itemBeingDragged = gameObject;
		oneClickStartPosition = itemBeingDragged.transform.position;
		inputPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		pivotOffset = inputPosition - oneClickStartPosition;
		//Debug.Log ("onbegin()");
		if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (panel.GetComponent<Collider2D> ().bounds)) {
			if (itemBeingDragged.tag == "Board") {
				boardNum = boardNum - 1;
				countTool.text = "x " + boardNum;
				image.rectTransform.sizeDelta = new Vector2 (boardWidth, boardHeight);

				sceneCollider = itemBeingDragged.GetComponent<BoxCollider2D> ();
				sceneCollider.size = new Vector2 (boardWidth, boardHeight);
		
//				sceneCollider.bounds.size = new Vector3 (boardWidth, boardHeight, 0);
			} else if (itemBeingDragged.tag == "Nail") {
				nailNum = nailNum - 1;
				countTool.text = "x " + nailNum;
				image.rectTransform.sizeDelta = new Vector2 (nailWidth, nailHeight);
			}

			image.sprite = itemOutSidePanel;
		} else {
			var children = itemBeingDragged.GetComponentsInChildren<Image>();
			foreach( Image child in children){
				//Debug.Log(child);
				if (child.name == "RotateButton") {
					//Debug.Log ("hah");
					child.enabled = false;
				}
			}
		}

	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name == "ToolBarPanel") {
			collide = true;
		}

	}

	public void OnDrag(PointerEventData eventData) {
		
		inputPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		transform.position = inputPosition - pivotOffset;

	}
		

	public void OnEndDrag(PointerEventData eventData) {
		

		if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (panel.GetComponent<Collider2D> ().bounds)) {
			if (itemBeingDragged.tag == "Board") {
				boardNum = boardNum + 1;
				countTool.text = "x " + boardNum;
				//reset the object rotate to 0
				itemBeingDragged.transform.rotation=Quaternion.identity;


				var children = itemBeingDragged.GetComponentsInChildren<Image> ();
				foreach (Image child in children) {
					//Debug.Log (child);
					if (child.name == "RotateButton") {
						//Debug.Log ("haha drog");
						child.enabled = false;
					}
				}


			} else if (itemBeingDragged.tag == "Nail") {
				nailNum = nailNum + 1;
				countTool.text = "x " + nailNum;
			}
			image.sprite = originImageSprite;

			image.rectTransform.sizeDelta = new Vector2 (originImageWidth, originImageHeight);
			transform.position = startPosition;
		} else {
			if (itemBeingDragged.tag == "Board") {
				var children = itemBeingDragged.GetComponentsInChildren<Image> ();
				foreach (Image child in children) {
					//Debug.Log (child);
					if (child.name == "RotateButton") {
						//Debug.Log ("haha drog");
						child.enabled = true;
					}
				}
			}
		}
	}



}
