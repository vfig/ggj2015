using UnityEngine;
using System.Collections;

public class InstructionsScene : MonoBehaviour {
	
	void Start() {
		GameInput.ResetInput();
	}
	
	void Update() {
		if(GameInput.GetAnyButtonDown()) {
			Application.LoadLevel("StartScene");
		}
	}
}
