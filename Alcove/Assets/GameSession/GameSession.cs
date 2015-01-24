using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// GameSession handles pre- and post- core game stuff, and is responsible for kicking off the core game.
public class GameSession : MonoBehaviour {

	public enum GameplayState {
		Pregame,
		InProgress,
		Roundup
	}

	public GUIStyle guiStyle;
	public Text getReadyText;
	public Text winnerText;
	public Text clickToContinueText;

	GameplayState gameplayState;
	float gamestateCounter;

	void Start() {
		StartNewGame();
	}

	void Update() {
		gamestateCounter += (Time.deltaTime * 60);
		switch(gameplayState) {
		case GameplayState.Pregame:
			UpdatePregameState();
			break;
		case GameplayState.InProgress:
			UpdateInProgressState();
			break;
		case GameplayState.Roundup:
			UpdateRoundupState();
			break;
		}
	}

	void OnGUI() {
		switch(gameplayState) {
		case GameplayState.Pregame:
			DrawPregameUi();
			break;
		case GameplayState.InProgress:
			break;
		case GameplayState.Roundup:
			DrawRoundupUi();
			break;
		}
	}

	void DrawPregameUi() {
	}

	void DrawRoundupUi() {

	}

	void QuickTopLeftText(string text, ref int yPosition) {
		GUI.Label(new Rect(10, yPosition, 400, 30), text, guiStyle);
		yPosition += 20;
	}

	void QuickText(string text, int xPosition, ref int yPosition) {
		GUI.Label(new Rect(xPosition, yPosition, 300, 40), text);
		yPosition += 25;
	}

	bool QuickButton(string text, int xPosition, ref int yPosition, int width=200, bool show=true) {
		if(!show) {
			yPosition += 45;
			return false;
		}
		bool pressed = GUI.Button(new Rect(xPosition, yPosition, width, 40), text);
		yPosition += 45;
		return pressed;
	}

	string QuickTextField(string text, int xPosition, ref int yPosition) {
		string textFieldValue = GUI.TextField(new Rect(xPosition, yPosition, 200, 40), text);
		yPosition += 45;
		return textFieldValue;
	}
	
	public void SetState(GameplayState state) {

		// Handle closure of previous state.
		switch (gameplayState) {
		case GameplayState.Pregame:
			StopPregameState();
			break;
		case GameplayState.InProgress:
			StopInProgressState();
			break;
		case GameplayState.Roundup:
			StopRoundupState();
			break;
		}

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

		getReadyText.enabled = false;
		winnerText.enabled = false;
		clickToContinueText.enabled = false;

		SetState(GameplayState.Pregame);
	}
	
	// PREGAME //////////////////////////////

	void SetupPregameState() {
		// This state's not for initialisation,
		// it's for any sort of intro we have.
		Debug.Log("Setting up Pregame state.");
		getReadyText.enabled = true;
	}

	void UpdatePregameState() {

		int displayTime = 40;

		if(gamestateCounter > displayTime) {
			SetState(GameplayState.InProgress);
		}
	}

	void StopPregameState() {
		getReadyText.enabled = false;
	}

	// IN PROGRESS //////////////////////////////

	void SetupInProgressState() {
		Debug.Log("Setting up InProgress state.");
		Application.LoadLevel("GameplaySubScene");
	}
	
	void UpdateInProgressState() {
		// Wait for the CoreGameSession to tell us of a win
	}

	void StopInProgressState() {

	}

	// ROUNDUP //////////////////////////////

	void SetupRoundupState() {
		Debug.Log("Setting up Roundup state.");
		// FIXME: Get the winner ID to this method somehow.
		winnerText.text = "Winner: " + "Not specified";
		winnerText.enabled = true;
		clickToContinueText.enabled = true;
	}

	void UpdateRoundupState() {
		if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel("StartScene");
		}
	}

	void StopRoundupState() {
		winnerText.enabled = false;
		clickToContinueText.enabled = false;
	}
}
