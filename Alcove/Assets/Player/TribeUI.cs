using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TribeUI : MonoBehaviour {
	public Tribe tribe;
	private CanvasGroup group;
	private Image image;
	private Text[] texts;

	public void Start() {
		group = GetComponentInChildren<CanvasGroup>();
		image = GetComponentInChildren<Image>();
		texts = GetComponentsInChildren<Text>();
	}

	public void Update() {
		string label = tribe.Count.ToString() + " / " + tribe.UnitLimit.ToString();
		foreach (Text text in texts) {
			text.text = label;
		}
		if (tribe.IsBusy) {
			group.alpha = 0.25f + (0.5f * tribe.BusyFraction);
			image.fillAmount = tribe.BusyFraction;
		} else {
			group.alpha = 1.0f;
			image.fillAmount = 1.0f;
		}
	}
}
