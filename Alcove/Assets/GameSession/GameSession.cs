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

	ShaunTempPlayer player1;
	ShaunTempPlayer player2;

	void Start() {

		player1 = new ShaunTempPlayer();
		player2 = new ShaunTempPlayer();

		StartNewGame();
	}

	void Update() {
	}

	void OnGUI() {
		if(SHOW_DEBUG_TEXT) {
			int yPosition = 0;
			DebugLabel("Gameplay state: " + gameplayState, ref yPosition);
			DebugLabel("Player 1 Tower: " + player1, ref yPosition);
			DebugLabel("Player 2 Tower: " + player2, ref yPosition);
		}
	}

	void DebugLabel(string text, ref int yPosition) {
		GUI.Label(new Rect(10, yPosition, 200, 30), text);
		yPosition += 20;
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

		// This method's for initialisation of a game round,
		// and SetupPregameState can be used to introduce the game
		// (panning of camera or whatever).

		player1.Reset();
		player2.Reset();

		player1.tower.AddStartingSegments();
		player2.tower.AddStartingSegments();

		SetGameplayState(GameplayState.Pregame);
	}

	void SetupPregameState() {
		// This state's not for initialisation,
		// it's for any sort of intro we have.
		Debug.Log("Setting up Pregame state.");
	}

	void SetupInProgressState() {
		Debug.Log("Setting up InProgress state.");
	}

	void SetupRoundupState() {
		Debug.Log("Setting up Roundup state.");
	}


}
