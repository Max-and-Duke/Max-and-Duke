using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{	
	void Start(){
		iTween.MoveBy(gameObject, iTween.Hash("z", 20, "easeType", "easeInOutExpo", "loopType", "loop", "delay", 1.0));
	}
}

