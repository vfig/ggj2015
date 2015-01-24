using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TribeUI : MonoBehaviour {
	public Tribe tribe;
	private CanvasGroup group;
	private Image image;
	private Text text;

	public void Start() {
		group = GetComponentInChildren<CanvasGroup>();
		image = GetComponentInChildren<Image>();
		text = GetComponentInChildren<Text>();
	}

	public void Update() {
		text.text = tribe.count.ToString();
		if (tribe.IsBusy) {
			group.alpha = 0.25f + (0.5f * tribe.BusyFraction);
			image.fillAmount = tribe.BusyFraction;
		} else {
			group.alpha = 1.0f;
			image.fillAmount = 1.0f;
		}
	}
}
