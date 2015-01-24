using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// GameSession handles pre- and post- core game stuff, and is responsible for kicking off the core game.
public class GameSession : MonoBehaviour {

	public enum GameplayState {
		Pregame,
		LoadingLevel,
		InProgress,
		Roundup
	}

	public GUIStyle guiStyle;
	public Text getReadyText;
	public Text winnerText;
	public Text clickToContinueText;
	
	GameplayManager gameplayManager;
	GameplayState gameplayState;
	float gamestateCounter;

	void Start() {
		StartNewGame();
	}

	void Update() {
		gamestateCounter += (Time.deltaTime * 60);
		switch(gameplayState) {
		case GameplayState.Pregame:
			Update_Pregame();
			break;
		case GameplayState.LoadingLevel:
			Update_LoadingLevel();
			break;
		case GameplayState.InProgress:
			Update_InProgress();
			break;
		case GameplayState.Roundup:
			Update_Roundup();
			break;
		}
	}

	void OnGUI() {
		switch(gameplayState) {
		case GameplayState.Pregame:
			DrawPregameUi();
			break;
		case GameplayState.LoadingLevel:
			break;
		case GameplayState.InProgress:
			DrawInProgressUi();
			break;
		case GameplayState.Roundup:
			DrawRoundupUi();
			break;
		}
	}
	
	void DrawPregameUi() {
	}

	void DrawInProgressUi() {
		int yPosition = 10;
		int p1Segments = gameplayManager.GetPlayer(0).tower.GetCompletedSegmentCount();
		int p2Segments = gameplayManager.GetPlayer(1).tower.GetCompletedSegmentCount();
		QuickTopLeftText("P1 Tower: " +  p1Segments, ref yPosition);
		QuickTopLeftText("P2 Tower: " +  p2Segments, ref yPosition);
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
		switch(gameplayState) {
		case GameplayState.Pregame:
			Stop_Pregame();
			break;
		case GameplayState.LoadingLevel:
			break;
		case GameplayState.InProgress:
			Stop_InProgress();
			break;
		case GameplayState.Roundup:
			Stop_Roundup();
			break;
		}

		//Debug.Log("Setting gameplay state to: " + state);
		gameplayState = state;

		switch(state) {
		case GameplayState.Pregame:
			Setup_Pregame();
			break;
		case GameplayState.LoadingLevel:
			Setup_LoadingLevel();
			break;
		case GameplayState.InProgress:
			Setup_InProgress();
			break;
		case GameplayState.Roundup:
			Setup_Roundup();
			break;
		}
	}

	void StartNewGame() {
		//Debug.Log("Starting new game.");

		// This method's for initialisation of a game round,
		// and SetupPregameState can be used to introduce the game
		// (panning of camera or whatever).

		getReadyText.enabled = false;
		winnerText.enabled = false;
		clickToContinueText.enabled = false;

		SetState(GameplayState.Pregame);
	}
	
	// PREGAME //////////////////////////////

	void Setup_Pregame() {
		// This state's not for initialisation,
		// it's for any sort of intro we have.
		//Debug.Log("Setting up Pregame state.");
		getReadyText.enabled = true;
	}

	void Update_Pregame() {

		int displayTime = 40;

		if(gamestateCounter > displayTime) {
			SetState(GameplayState.LoadingLevel);
		}
	}

	void Stop_Pregame() {
		getReadyText.enabled = false;
	}

	// LOADING LEVEL //////////////////////////////

	void Setup_LoadingLevel() {
		Application.LoadLevelAdditive("GameplaySubScene");
	}

	void Update_LoadingLevel() {
		if(GrabGameplayManagerReference ()) {
			SetState(GameplayState.InProgress);
		}
	}

	// IN PROGRESS //////////////////////////////

	void Setup_InProgress() {
		//Debug.Log("Setting up InProgress state.");
	}

	void Update_InProgress() {
		// Wait for the CoreGameSession to tell us of a win
	}

	void Stop_InProgress() {
		
	}

	// ROUNDUP //////////////////////////////

	void Setup_Roundup() {
		//Debug.Log("Setting up Roundup state.");
		// FIXME: Get the winner ID to this method somehow.
		winnerText.text = "Winner: " + "Not specified";
		winnerText.enabled = true;
		clickToContinueText.enabled = true;
	}

	void Update_Roundup() {
		if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel("StartScene");
		}
	}

	void Stop_Roundup() {
		winnerText.enabled = false;
		clickToContinueText.enabled = false;
	}




	// MISC HELPER METHODS ///////////////

	bool GrabGameplayManagerReference() {
		GameObject gameplayManagerObject = GameObject.Find("GameplayManager") as GameObject;
		if(gameplayManagerObject) {
			gameplayManager = gameplayManagerObject.GetComponent<GameplayManager>();
			return true;
		}
		return false;
	}
}
