using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {

	void Start() {
		GameInput.ResetInput();
	}

	void Update() {
		GameInput.Update();
		if(GameInput.GetAnyButtonDown()) {
			Application.LoadLevel("GameScene");
		}
	}
}
