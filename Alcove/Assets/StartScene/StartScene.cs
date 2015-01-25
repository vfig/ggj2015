using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {

	void Start() {
		GameInput.ResetInput();
	}

	void Update() {
		GameInput.Update();

		if(GameInput.GetAnyStartButtonDown()) {
			Application.LoadLevel("GameScene");
		} else if(GameInput.GetAnyCancelButtonDown()) {
			Application.LoadLevel("EndScene");
		}
	}
}
