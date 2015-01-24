using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {

	void Start() {
		GameInput.ResetInput();
	}

	void Update() {
		if(GameInput.GetAnyTribeButtonDownForAnyPlayer()) {
			Application.LoadLevel("StartScene");
		}
	}
}
