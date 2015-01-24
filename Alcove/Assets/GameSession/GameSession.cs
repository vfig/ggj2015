using UnityEngine;
using System.Collections;

public class GameSession : MonoBehaviour {

	const bool SHOW_DEBUG_TEXT = true;

	public enum GameplayState {
		Pregame,
		InProgress,
		Roundup
	}

	GameplayState gameplayState;

	// On 'IPlayer' etc.
	// These interface types are just put in place until we have more vision of each other's code and are ready to integrate more.
	Object player1;
	Object player2;

	void Start() {
		//player1.GetTower().AddComponent("")
		StartNewGame();
	}

	void Update() {
	}

	void OnGUI() {
		if(SHOW_DEBUG_TEXT) {
			GUI.Label(new Rect(10, 10, 200, 30), "Gameplay state: " + gameplayState);
		}
	}

	void SetGameplayState(GameplayState state) {

		Debug.Log("Setting gameplay state to: " + state);
		gameplayState = state;

		switch(state) {
		case GameplayState.Pregame:
			SetupPregameState();
			break;
		case GameplayState.InProgress:
			SetupInProgressState();
			break;
		case GameplayState.Roundup:
			SetupRoundupState();
			break;
		}
	}

	void StartNewGame() {
		Debug.Log("Starting new game.");

		//player1.Reset();
		//player2.Reset();

		SetGameplayState(GameplayState.Pregame);
	}

	void SetupPregameState() {
		Debug.Log("Setting up Pregame state.");
	}

	void SetupInProgressState() {
		Debug.Log("Setting up InProgress state.");
	}

	void SetupRoundupState() {
		Debug.Log("Setting up Roundup state.");
	}


}
