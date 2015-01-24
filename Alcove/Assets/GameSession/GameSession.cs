using UnityEngine;
using System.Collections;

public class GameSession : MonoBehaviour {

	public enum GameplayState {
		Pregame,
		InProgress,
		Roundup
	}

	GameplayState gameplayState;

	Object player1;
	Object player2;

	void Start() {
		SetGameplayState(GameplayState.Pregame);
	}

	void Update() {

	}

	void SetGameplayState(GameplayState state) {
		Debug.Log("Setting gameplay state to: " + state);
		gameplayState = state;
	}
}
