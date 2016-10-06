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
	public int dragNailNum;
	public Vector3 nailPosition;
	public bool canBeDrag;

	private static int boardNum = 2;
	private static int nailNum = 4;
	private float originImageWidth;
	private float originImageHeight;

	private float boardWidth = 500f;
	private float boardHeight = 60f;
	private float nailWidth = 50f;
	private float nailHeight = 50f;
	//private BoxCollider2D sceneCollider;

	private Vector2 startPosition;
	private Vector2 inputPosition;
	private Vector2 pivotOffset;
	private Transform startParent;
	private Vector2 oneClickStartPosition;
	private Image image;
	private Sprite originImageSprite;

	private GameObject[] boardObjects;

//	private GameObject NailDropAreaLeft;
//	private GameObject NailDropAreaRight;
//	private GameObject NailDropAreaMiddle;

	void Start(){
		image = itemBeingDragged.GetComponent<Image>();
		originImageWidth = image.rectTransform.rect.width;
		originImageHeight = image.rectTransform.rect.height;
		//print (originImage.rectTransform.rect.height);
		originImageSprite = image.sprite;
		startPosition = itemBeingDragged.transform.position;
//		dragNailNum = 0;
//		nailPosition = itemBeingDragged.transform.position; 


		//***********
		//need to revise for if there are several boards
		//Debug.Log(itemBeingDragged.name);
		//Debug.Log(itemBeingDragged.GetComponentInChildren<Transform>());
		boardObjects= GameObject.FindGameObjectsWithTag("Board");
		foreach(var jj in boardObjects){
			Debug.Log(jj.name);
		}
//		NailDropAreaLeft = GameObject.Find ("NailDropArea-left");
//		NailDropAreaRight = GameObject.Find ("NailDropArea-right");
//		NailDropAreaMiddle = GameObject.Find ("NailDropArea-middle");
		//Debug.Log (GameObject.Find ("NailDropArea-left"));


		if (itemBeingDragged.tag == "Board") {
			countTool.text = "x " + boardNum;
			var children = itemBeingDragged.GetComponentsInChildren<Image>();
			foreach( Image child in children){
				//Debug.Log(child.name);
				if (child.name == "RotateButton-right" || child.name == "RotateButton-left") {
					//Debug.Log ("hah");
					child.enabled = false;
				}
			}
		} else if (itemBeingDragged.tag == "Nail"){
			countTool.text = "x " + nailNum;
		}


		canBeDrag = true;
		
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

				var sceneCollider = itemBeingDragged.GetComponent<BoxCollider2D> ();
				sceneCollider.size = new Vector2 (boardWidth, boardHeight);
		
//				sceneCollider.bounds.size = new Vector3 (boardWidth, boardHeight, 0);
			} else if (itemBeingDragged.tag == "Nail") {
				nailNum = nailNum - 1;
				countTool.text = "x " + nailNum;
				image.rectTransform.sizeDelta = new Vector2 (nailWidth, nailHeight);

				var sceneCollider = itemBeingDragged.GetComponent<BoxCollider2D> ();
				sceneCollider.size = new Vector2 (nailWidth, nailHeight);
			}

			image.sprite = itemOutSidePanel;
		} else {
			var children = itemBeingDragged.GetComponentsInChildren<Image>();
			foreach( Image child in children){
				//Debug.Log(child);
				if (child.name == "RotateButton-right"|| child.name == "RotateButton-left") {
					//Debug.Log ("hah");
					child.enabled = false;
				}
			}
		}

		itemBeingDragged.transform.SetParent (GameObject.Find ("Canvas").transform);

		checkNailLeaveTheBoard ();

	}


	private void checkNailLeaveTheBoard(){
		if(itemBeingDragged.tag == "Nail"){
			foreach(var singleBoard in boardObjects){
				GameObject NailDropAreaLeft = null;
				GameObject NailDropAreaRight = null;
				GameObject NailDropAreaMiddle = null;

				var items = singleBoard.GetComponentsInChildren<Transform> ();
				foreach(var item in items){
					if (item.name == "NailDropArea-left") {
						NailDropAreaLeft = item.gameObject;
					}
					if (item.name == "NailDropArea-right") {
						NailDropAreaRight = item.gameObject;
					}
					if (item.name == "NailDropArea-middle") {
						NailDropAreaMiddle = item.gameObject;
					}
				}

				if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (NailDropAreaLeft.GetComponent<Collider2D> ().bounds)) {
					//itemBeingDragged.transform.position = NailDropAreaLeft.transform.position;
					//Debug.Log ("in draggable     " + itemBeingDragged.transform.position);
					var board = NailDropAreaLeft.transform.parent.GetComponent<Draggable> ();
					board.dragNailNum -= 1;
					board.canBeDrag = true;
					Debug.Log ("nail leave the board" + board.dragNailNum);
				} else if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (NailDropAreaRight.GetComponent<Collider2D> ().bounds)) {
					//itemBeingDragged.transform.position = NailDropAreaLeft.transform.position;
					//Debug.Log ("in draggable     " + itemBeingDragged.transform.position);
					var board = NailDropAreaLeft.transform.parent.GetComponent<Draggable> ();
					board.dragNailNum -= 1;
					board.canBeDrag = true;
					Debug.Log ("nail leave the board" + board.dragNailNum);
				} else if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (NailDropAreaMiddle.GetComponent<Collider2D> ().bounds)) {
					//itemBeingDragged.transform.position = NailDropAreaLeft.transform.position;
					//Debug.Log ("in draggable     " + itemBeingDragged.transform.position);
					var board = NailDropAreaLeft.transform.parent.GetComponent<Draggable> ();
					board.dragNailNum -= 1;
					board.canBeDrag = true;
					Debug.Log ("nail leave the board" + board.dragNailNum);
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
		if (canBeDrag) {
		
			inputPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			transform.position = inputPosition - pivotOffset;
		}

	}
		

	public void OnEndDrag(PointerEventData eventData) {
		if (itemBeingDragged.tag == "Nail") {
			bool flagFound = false;
			foreach (var singleBoard in boardObjects) {
				GameObject NailDropAreaLeft = null;
				GameObject NailDropAreaRight = null;
				GameObject NailDropAreaMiddle = null;

				var items = singleBoard.GetComponentsInChildren<Transform> ();
				foreach (var item in items) {
					if (item.name == "NailDropArea-left") {
						NailDropAreaLeft = item.gameObject;
					}
					if (item.name == "NailDropArea-right") {
						NailDropAreaRight = item.gameObject;
					}
					if (item.name == "NailDropArea-middle") {
						NailDropAreaMiddle = item.gameObject;
					}
				}
				if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (NailDropAreaLeft.GetComponent<Collider2D> ().bounds)) {
					itemBeingDragged.transform.position = NailDropAreaLeft.transform.position;
					//Debug.Log ("in draggable     " + itemBeingDragged.transform.position);
					var board = NailDropAreaLeft.transform.parent.GetComponent<Draggable> ();
					board.dragNailNum += 1;
					//dragNailNum += 1;
					board.nailPosition = NailDropAreaLeft.transform.position;

					board.canBeDrag = false;
					flagFound = true;
					Debug.Log ("nail pin on the board" + board.dragNailNum);


					//too verbose, should revise
//				var children = NailDropAreaLeft.transform.parent.GetComponentsInChildren<Image> ();
//				foreach (Image child in children) {
//					Debug.Log (child);
//					if (child.name == "RotateButton-left") {
//						//Debug.Log ("haha drog");
//						child.enabled = false;
//					}
//				}
				} else if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (NailDropAreaRight.GetComponent<Collider2D> ().bounds)) {
					itemBeingDragged.transform.position = NailDropAreaRight.transform.position;
					var board = NailDropAreaRight.transform.parent.GetComponent<Draggable> ();
					board.dragNailNum += 1;
					board.nailPosition = NailDropAreaRight.transform.position;
					board.canBeDrag = false;
					flagFound = true;

					Debug.Log ("nail pin on the board" + board.dragNailNum);
				} else if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (NailDropAreaMiddle.GetComponent<Collider2D> ().bounds)) {
					itemBeingDragged.transform.position = NailDropAreaMiddle.transform.position;
					var board = NailDropAreaMiddle.transform.parent.GetComponent<Draggable> ();
					board.dragNailNum += 1;
					board.nailPosition = NailDropAreaMiddle.transform.position;
					board.canBeDrag = false;
					flagFound = true;

					Debug.Log ("nail pin on the board" + board.dragNailNum);
				} 
			}
			if (!flagFound){
				//				nailNum = nailNum + 1;
				//				countTool.text = "x " + nailNum;
				itemBeingDragged.transform.position = startPosition;
			}
		}
		

		if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (panel.GetComponent<Collider2D> ().bounds)) {
			if (itemBeingDragged.tag == "Board") {
				boardNum = boardNum + 1;
				countTool.text = "x " + boardNum;
				//reset the object rotate to 0
				itemBeingDragged.transform.rotation=Quaternion.identity;


				var children = itemBeingDragged.GetComponentsInChildren<Image> ();
				foreach (Image child in children) {
					//Debug.Log (child);
					if (child.name == "RotateButton-right"|| child.name == "RotateButton-left") {
						//Debug.Log ("haha drog");
						child.enabled = false;
					}
				}


			} else if (itemBeingDragged.tag == "Nail") {
				nailNum = nailNum + 1;
				countTool.text = "x " + nailNum;
			}

			itemBeingDragged.transform.SetParent (GameObject.Find ("Toolbar-Board").transform);
			image.sprite = originImageSprite;

			image.rectTransform.sizeDelta = new Vector2 (originImageWidth, originImageHeight);
			transform.position = startPosition;
		} else {
			if (itemBeingDragged.tag == "Board") {
				var children = itemBeingDragged.GetComponentsInChildren<Image> ();
				foreach (Image child in children) {
					//Debug.Log (child);
					if (child.name == "RotateButton-right"|| child.name == "RotateButton-left") {
						//Debug.Log ("haha drog");
						child.enabled = true;
					}
				}
			}
			itemBeingDragged.transform.SetParent (GameObject.Find ("Canvas").transform);
		}
	}



}
