using UnityEngine;
using System.Collections;

public class DummyGameInputGUI : MonoBehaviour {
	public void OnGUI() {
		for (int player = 0; player <= 1; ++player) {
			float x = (player == 0 ? 10 : 210);
			GUI.Label(new Rect(x, 10, 200, 20), "P" + player + " Scroll up: " + GameInput.GetScrollUpButtonDown(player));
			GUI.Label(new Rect(x, 30, 200, 20), "P" + player + " Scroll down: " + GameInput.GetScrollDownButtonDown(player));
			GUI.Label(new Rect(x, 50, 200, 20), "P" + player + " A tribe: " + GameInput.GetTribeButton(0, player));
			GUI.Label(new Rect(x, 70, 200, 20), "P" + player + " B tribe: " + GameInput.GetTribeButton(1, player));
			GUI.Label(new Rect(x, 90, 200, 20), "P" + player + " X tribe: " + GameInput.GetTribeButton(2, player));
			GUI.Label(new Rect(x, 110, 200, 20), "P" + player + " Y tribe: " + GameInput.GetTribeButton(3, player));

			GUI.Label(new Rect(x, 130, 200, 20), "P" + player + " Scroll left: " + GameInput.GetScrollLeftButtonDown(player));
			GUI.Label(new Rect(x, 150, 200, 20), "P" + player + " Scroll right: " + GameInput.GetScrollRightButtonDown(player));
		}
	}
}
