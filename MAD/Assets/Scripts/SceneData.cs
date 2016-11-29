using UnityEngine;
using System.Collections;

[System.Serializable]
public class SceneData {
	public Vector3 maxPosition; // {x, y, 350}
	public Vector3 dukePosition; // {x, y, 350}
	public int numBoard;
	public int numNail;
	public int numBox;
	public float[] thresholds; // { 0.10f, 0.55f }
}