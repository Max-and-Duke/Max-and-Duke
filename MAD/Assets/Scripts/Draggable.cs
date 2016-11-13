using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public GameObject itemBeingDragged;
	public Sprite itemOutSidePanel;
	public Text countTool;
	public GameObject panel;
	public bool collide = false;
	public int dragNailNum;
	public Vector3 nailPosition;
	public Vector3[] nailPositionsRecord = new Vector3[3];
	public bool canBeDrag;

	public GameObject boardPrefab;
	public GameObject nailPrefab;

	private GameObject[] boardClonedGameObject;
	private GameObject[] nailClonedGameObject;

	public static void Ha() {
		
	}

	private Dictionary<string, int> costDetails = new Dictionary<string, int>
	{
		{"Board", 30},
		{"Nail", 10}
	};
	public static int costSoFar = 0;

	public static int boardNum = 2;
	public static int nailNum = 4;

	private float originImageWidth;
	private float originImageHeight;

	private Vector3 emptyNailPos = new Vector3();

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


	private CostManager costManager;

//	private GameObject[] boardObjects;

//	private GameObject NailDropAreaLeft;
//	private GameObject NailDropAreaRight;
//	private GameObject NailDropAreaMiddle;
	void Awake() {

//		boardNum = 2;
//		nailNum = 4;

	}
	void Start(){
		boardClonedGameObject = new GameObject[boardNum];
		nailClonedGameObject = new GameObject[nailNum];

		image = itemBeingDragged.GetComponent<Image>();
		originImageWidth = image.rectTransform.rect.width;
		originImageHeight = image.rectTransform.rect.height;
		//print (originImage.rectTransform.rect.height);
		originImageSprite = image.sprite;
		startPosition = itemBeingDragged.transform.position;
		dragNailNum = 0;
//		nailPosition = itemBeingDragged.transform.position; 

		costManager = GameObject.Find ("Cost Manager").GetComponent<CostManager> ();
		//***********
		//need to revise for if there are several boards
		//Debug.Log(itemBeingDragged.name);
		//Debug.Log(itemBeingDragged.GetComponentInChildren<Transform>());

//		boardObjects= GameObject.FindGameObjectsWithTag("Board");

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
				

				if (boardNum <= 0) {
					itemBeingDragged = null;
					return;
				}

				if (itemBeingDragged.GetComponent<Image> ().sprite == itemOutSidePanel)
					return;
				
				boardNum = boardNum - 1;
				if (boardNum >= 0) {
					countTool.text = "x " + boardNum;


					boardClonedGameObject[boardNum] = GameObject.Instantiate (itemBeingDragged);

					itemBeingDragged = boardClonedGameObject [boardNum];
					itemBeingDragged.GetComponent<Image> ().rectTransform.sizeDelta = new Vector2 (boardWidth, boardHeight);
//					itemBeingDragged.transform.position = inputPosition - pivotOffset;
//					Debug.Log("before setParent" + itemBeingDragged.transform.position);
					itemBeingDragged.transform.position = inputPosition - pivotOffset;
//					Debug.Log("after reset to toolbox transform position" + itemBeingDragged.transform.position);
					itemBeingDragged.transform.SetParent(GameObject.Find("Canvas").transform);
//					Debug.Log("after set parent" + itemBeingDragged.transform.position);
					itemBeingDragged.transform.position = new Vector3(itemBeingDragged.transform.position.x,
						itemBeingDragged.transform.position.y,
						2f);
					Debug.Log (itemBeingDragged.transform.position);

				}
//				image.rectTransform.sizeDelta = new Vector2 (boardWidth, boardHeight);

//				var sceneCollider = itemBeingDragged.GetComponent<BoxCollider2D> ();
//				sceneCollider.size = new Vector2 (boardWidth, boardHeight);


		
//				sceneCollider.bounds.size = new Vector3 (boardWidth, boardHeight, 0);
			} else if (itemBeingDragged.tag == "Nail") {
				
//				image.rectTransform.sizeDelta = new Vector2 (nailWidth, nailHeight);

				if (nailNum <= 0) {
					itemBeingDragged = null;
					return;
				}
				if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (panel.GetComponent<Collider2D> ().bounds)) {
					nailNum = nailNum - 1;
				}
				if (nailNum >= 0) {
					countTool.text = "x " + nailNum;

					nailClonedGameObject[nailNum] = GameObject.Instantiate (itemBeingDragged);

					itemBeingDragged = nailClonedGameObject [nailNum];
					itemBeingDragged.GetComponent<Image> ().rectTransform.sizeDelta = new Vector2 (nailWidth, nailHeight);
					itemBeingDragged.transform.position = inputPosition - pivotOffset;
					itemBeingDragged.transform.SetParent (GameObject.Find("Canvas").transform);
				}




				//*******************8


				var sceneCollider = itemBeingDragged.GetComponent<BoxCollider2D> ();
				sceneCollider.size = new Vector2 (nailWidth, nailHeight);
			}



				
			//***************
			if (itemBeingDragged == null) return;
			
			itemBeingDragged.GetComponent<Image> ().sprite = itemOutSidePanel;
			

			costSoFar += costDetails[itemBeingDragged.tag];
			costManager.SetCosts (costSoFar);
			Debug.Log ("~~~~~~~" + costSoFar);





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

//		itemBeingDragged.transform.SetParent (GameObject.Find ("Canvas").transform);


		checkNailLeaveTheBoard ();

	}


	private void checkNailLeaveTheBoard(){
		var boardObjects= GameObject.FindGameObjectsWithTag("Board");
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
					Debug.Log ("leave left nail's parent is" + board.name);
					board.dragNailNum -= 1;
					board.nailPositionsRecord [0] = emptyNailPos;
					if (board.dragNailNum == 0) {
						board.canBeDrag = true;
					}else if(board.dragNailNum == 1){
						foreach (Vector3 nailPos in board.nailPositionsRecord) {
							if (nailPos != emptyNailPos) {
								board.nailPosition = nailPos;
							}
						}
					}
//					Debug.Log ("nail leave the board" + board.dragNailNum);
				} else if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (NailDropAreaRight.GetComponent<Collider2D> ().bounds)) {
					//itemBeingDragged.transform.position = NailDropAreaLeft.transform.position;
					//Debug.Log ("in draggable     " + itemBeingDragged.transform.position);
					var board = NailDropAreaLeft.transform.parent.GetComponent<Draggable> ();
					Debug.Log ("leave right nail's parent is" + board.name);
					board.dragNailNum -= 1;
					board.nailPositionsRecord [2] = emptyNailPos;
					if (board.dragNailNum == 0) {
						board.canBeDrag = true;
					}else if(board.dragNailNum == 1){
						foreach (Vector3 nailPos in board.nailPositionsRecord) {
							if (nailPos != emptyNailPos) {
								board.nailPosition = nailPos;
							}
						}
					}
//					Debug.Log ("nail leave the board" + board.dragNailNum);
				} else if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (NailDropAreaMiddle.GetComponent<Collider2D> ().bounds)) {
					//itemBeingDragged.transform.position = NailDropAreaLeft.transform.position;
					//Debug.Log ("in draggable     " + itemBeingDragged.transform.position);
					var board = NailDropAreaLeft.transform.parent.GetComponent<Draggable> ();
					Debug.Log ("leave middle nail's parent is" + board.name);
					board.dragNailNum -= 1;
					board.nailPositionsRecord [1] = emptyNailPos;
					if (board.dragNailNum == 0) {
						board.canBeDrag = true;
					}else if(board.dragNailNum == 1){
						foreach (Vector3 nailPos in board.nailPositionsRecord) {
							if (nailPos != emptyNailPos) {
								board.nailPosition = nailPos;
							}
						}
					}
//					Debug.Log ("nail leave the board" + board.dragNailNum);
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
		if (canBeDrag && itemBeingDragged != null) {
//			if (boardNum == -1 || nailNum == -1)
//				return;
		
			inputPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

//			boardClonedGameObject[boardNum].transform.position = inputPosition - pivotOffset;
			itemBeingDragged.transform.position = inputPosition - pivotOffset;
//			itemBeingDragged.transform.position = new Vector3(itemBeingDragged.transform.position.x,
//				itemBeingDragged.transform.position.y,
//				200f);
//			Debug.Log (itemBeingDragged.transform.position);
//			Debug.Log ("shubiao" + inputPosition);
//			Debug.Log ("pivotoffset" + pivotOffset);
//			Debug.Log ("item" + itemBeingDragged.transform.position);
		}

	}
		

	public void OnEndDrag(PointerEventData eventData) {
		if (itemBeingDragged == null)
			return;

		itemBeingDragged.transform.position = new Vector3(itemBeingDragged.transform.position.x,
			itemBeingDragged.transform.position.y,
			200f);
		Debug.Log (itemBeingDragged.transform.position);
		if (itemBeingDragged.tag == "Nail") {
			var boardObjects= GameObject.FindGameObjectsWithTag("Board");
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
					Debug.Log ("pin left nail's parent is" + board.name);
					board.dragNailNum += 1;
					//dragNailNum += 1;

					board.nailPositionsRecord[0] = NailDropAreaLeft.transform.position;

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
					Debug.Log ("pin right nail's parent is" + board.name);
					board.dragNailNum += 1;

					Debug.Log ("nail pin on the board" + board.dragNailNum);

					board.nailPositionsRecord[2] = NailDropAreaRight.transform.position;
					board.nailPosition = NailDropAreaRight.transform.position;
					board.canBeDrag = false;
					flagFound = true;

//					Debug.Log ("nail pin on the board" + board.dragNailNum);
				} else if (itemBeingDragged.GetComponent<Collider2D> ().bounds.Intersects (NailDropAreaMiddle.GetComponent<Collider2D> ().bounds)) {
					itemBeingDragged.transform.position = NailDropAreaMiddle.transform.position;
					var board = NailDropAreaMiddle.transform.parent.GetComponent<Draggable> ();
					Debug.Log ("pin middle nail's parent is" + board.name);
					board.dragNailNum += 1;

					Debug.Log ("nail pin on the board" + board.dragNailNum);

					board.nailPositionsRecord[1] = NailDropAreaMiddle.transform.position;
					board.nailPosition = NailDropAreaMiddle.transform.position;
					board.canBeDrag = false;
					flagFound = true;

//					Debug.Log ("nail pin on the board" + board.dragNailNum);
				} 
			}
			if (!flagFound){
				//				nailNum = nailNum + 1;
				//				countTool.text = "x " + nailNum;
//				itemBeingDragged.transform.position = startPosition;

//				if (nailNum != 0 ) {
						Object.Destroy (itemBeingDragged);
						nailNum = nailNum + 1;
						countTool.text = "x " + nailNum;
						costSoFar -= costDetails[itemBeingDragged.tag];
						costManager.SetCosts (costSoFar);
//					}

			}
		}
		
//		Debug.Log (itemBeingDragged.GetComponent<Collider2D> ().bounds.ToString ());
//		Debug.Log (panel.GetComponent<Collider2D> ().bounds.ToString ());

//		var itemBeingDraggedBounds = new Bounds (new Vector3 (itemBeingDragged.GetComponent<Collider2D> ().bounds.center.x,
//			                             itemBeingDragged.GetComponent<Collider2D> ().bounds.center.y,
//			                             panel.GetComponent<Collider2D> ().bounds.center.z),
//										itemBeingDragged.GetComponent<Collider2D> ().bounds.extents);
		
		if (itemBeingDragged.GetComponent<Collider2D>().bounds.Intersects (panel.GetComponent<Collider2D> ().bounds)) {
			Debug.Log ("haha");

			if (itemBeingDragged.tag == "Board") {
				if (boardNum == -1)
					return;
				boardNum = boardNum + 1;
				countTool.text = "x " + boardNum;
				//reset the object rotate to 0
				itemBeingDragged.transform.rotation=Quaternion.identity;

				costSoFar -= costDetails[itemBeingDragged.tag];
				costManager.SetCosts (costSoFar);
				Debug.Log ("~~~~~~~`" + costSoFar);

				var children = itemBeingDragged.GetComponentsInChildren<Image> ();
				foreach (Image child in children) {
					//Debug.Log (child);
					if (child.name == "RotateButton-right"|| child.name == "RotateButton-left") {
						//Debug.Log ("haha drog");
						child.enabled = false;
					}
				}


				if (boardNum != 0 ) {
					Object.Destroy (itemBeingDragged);
				}
//				itemBeingDragged.transform.SetParent (GameObject.Find ("ToolBar-Board").transform);

			} else if (itemBeingDragged.tag == "Nail") {
				if (nailNum == -1)
					return;
//				nailNum = nailNum + 1;
//				countTool.text = "x " + nailNum;

				if (nailNum != 0 ) {
					Object.Destroy (itemBeingDragged);
				}
//				itemBeingDragged.transform.SetParent (GameObject.Find ("ToolBar-Nail").transform);
			}




//			CostSoFar -= costDetails[itemBeingDragged.tag];
//			Debug.Log ("~~~~~~~`" + CostSoFar);

//			image.sprite = originImageSprite;
//
//			image.rectTransform.sizeDelta = new Vector2 (originImageWidth, originImageHeight);
//			transform.position = startPosition;
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


//			itemBeingDragged.transform.SetParent (GameObject.Find ("Canvas").transform);
		}
	}



}
