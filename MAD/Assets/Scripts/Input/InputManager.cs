using UnityEngine;
using System.Collections;

public enum Buttons{
	Right,
	Left,
	Up,
	Down,
	A,
	B,
	D
}

public enum Dog{
	Max,
	Duke
}
public enum Condition{
	GreaterThan,
	LessThan
}

[System.Serializable]
public class InputAxisState{
	public string axisName;
	public float offValue;
	public Buttons button;
	public Condition condition;

	public bool value{

		get{
			var val = Input.GetAxis(axisName);

			switch(condition){
			case Condition.GreaterThan:
				return val > offValue;
			case Condition.LessThan:
				return val < offValue;
			}

			return false;
		}

	}
}

public class InputManager : MonoBehaviour {

	public InputAxisState[] inputs;
	public InputState inputState;
//	public Dog dog = Dog.Max;
//	public int count = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var input in inputs) {
//			if (input.button == Buttons.D && input.value && count > 100) {
//				count = 0;
//
//				if (dog == Dog.Max) {
//					dog = Dog.Duke;
//				} else {
//					dog = Dog.Max;
//				}
//			} else
//				count++;
//
//			if(inputState.dog == dog)
				inputState.SetButtonValue(input.button, input.value);
		}
	}
}
