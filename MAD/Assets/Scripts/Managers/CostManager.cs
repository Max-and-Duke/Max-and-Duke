using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CostManager : MonoBehaviour {
	public GameObject filler;
	private Image image;

	private Dictionary<string, Color32> palette = new Dictionary<string, Color32> {
		{"3-star", new Color32(21, 159, 51, 255)},
		{"2-star", new Color32(255, 170, 15, 255)},
		{"1-star", new Color32(249, 64, 0, 255)}
	};

	private float[] thresholds = new float[] { 0.10f, 0.55f };


	public int currentCost;
	private float maxCost;

	private float Value {
		get { 
			if (image != null) {
				return image.fillAmount;
			} else {
				return 0.0f;
			}
		}
		set { 
			if (image != null) {
				UpdateImage (value);
			}
		}
	}
		
	private void UpdateProgress(float from, float to) {
		Hashtable param = new Hashtable();
		param.Add ("from", from);
		param.Add ("to", to);
		param.Add ("time", GetTimeSpan(from, to));
		param.Add ("onupdate", "TweenedSomeValue");
		iTween.ValueTo (gameObject, param);
	}

	private void TweenedSomeValue(float val) {
		Value = val;
	}

	void Awake () {
		currentCost = 0;
		maxCost = 100.0f;

		image = filler.GetComponent<Image>();
	}
		
	private void UpdateImage(float fillAmount) {
		image.fillAmount = fillAmount;
		image.color = GetColor (fillAmount);
	}

	public void SetCosts(int currentCost) {
		int previousCost = this.currentCost;
		this.currentCost = currentCost;
		UpdateProgress (GetFillAmount (previousCost), GetFillAmount (currentCost));
	}

	void Start () {
		UpdateImage (1.0f);
	}

	public int GetStarNumbers() {
		var fillAmount = Value;
		if (fillAmount > thresholds[1]) {
			return 3;
		} else if (fillAmount > thresholds[0]) {
			return 2;
		} else {
			return 1;
		}
	}

	// helpers =============================

	private float GetFillAmount(int cost) {
		return (maxCost - cost) / maxCost;
	}

	private Color32 GetColor(float fillAmout) {
		if (fillAmout > thresholds[1]) {
			return palette ["3-star"];
		} else if (fillAmout > thresholds[0]) {
			return palette ["2-star"];
		} else {
			return palette ["1-star"];
		}
	}

	private float GetTimeSpan(float from, float to) {
		return Mathf.Abs( from - to ) * 1.0f;
	}
}
