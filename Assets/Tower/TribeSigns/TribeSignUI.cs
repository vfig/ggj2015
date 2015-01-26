using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TribeSignUI : MonoBehaviour {
	public Tribe tribe;
	private Image image;
	private Text text;

	public void Start() {
		image = GetComponentInChildren<Image>();
		text = GetComponentInChildren<Text>();
	}

	public void Update() {
		if (tribe.IsBusy) {
			text.text = tribe.BusySeconds.ToString();
			image.fillAmount = tribe.BusyFraction;
		} else {
			text.text = "";
			image.fillAmount = 0.0f;
		}
	}
}
