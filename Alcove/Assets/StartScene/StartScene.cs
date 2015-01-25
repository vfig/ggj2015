using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {

	void Start() {
		GameInput.ResetInput();
	}

	void Update() {
		GameInput.Update ();
		/*if(GameInput.GetStartButtonDownAnyPlayer()) {
			Application.LoadLevel("GameScene");
		}
		if(GameInput.GetBackButtonDownAnyPlayer()) {
			Application.LoadLevel("EndScene");
		}*/
	}
}
