using UnityEngine;
using System.Collections;

public class DummyTribeGUI : MonoBehaviour {
	public Tribe tribe;

	public void OnGUI() {
		if (tribe) {
			Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
			pos.y = Screen.height - pos.y;
			string label = tribe.name + ": " + tribe.count;
			if (tribe.IsBusy) {
				label += " (" + tribe.BusySeconds + "s)";
			}
			GUI.Label(new Rect(pos.x, pos.y, 100, 20), label);
			if (!tribe.IsBusy && GUI.Button(new Rect(pos.x, pos.y + 20, 50, 20), "Work")) {
				float time = Random.Range(1, 15);
				tribe.StartBusy(time);
			}
		}
	}
}
