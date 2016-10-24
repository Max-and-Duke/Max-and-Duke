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

	private float[] thresholds = new float[] { 0.33f, 0.67f };

	public float currentCost = 10f;
	public float maxCost = 100f;

	// Use this for initialization
	void Awake () {
		image = filler.GetComponent<Image>();
	}

	private float getFillAmount() {
		return (maxCost - currentCost) / maxCost;
	}

	private Color32 getColor(float fillAmout) {
		if (fillAmout > thresholds[1]) {
			return palette ["3-star"];
		} else if (fillAmout > thresholds[0]) {
			return palette ["2-star"];
		} else {
			return palette ["1-star"];
		}
	}

	private void SetStatus(float fillAmount) {
		image.fillAmount = fillAmount;
		image.color = getColor (fillAmount);
	}

	void Start () {
		SetStatus (1.0f);
	}

	void Update () {
		SetStatus (getFillAmount());
	}
}
