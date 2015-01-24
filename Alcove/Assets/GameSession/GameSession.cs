using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

	public enum GameplayState {
		Pregame,
		InProgress,
		Roundup
	}

	public GUIStyle guiStyle;
	public GameRulesManager gameRulesManager;
	public Text getReadyText;
	public Text winnerText;
	public Text clickToContinueText;

	GameplayState gameplayState;
	float gamestateCounter;
	ShaunTempPlayer player1;
	ShaunTempPlayer player2;

	void Start() {

		player1 = new ShaunTempPlayer();
		player1.StartTemp();
		player2 = new ShaunTempPlayer();
		player2.StartTemp();

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
			DrawInGameTestingUi();
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

	void AddDebugLabel(string text, ref int yPosition) {
		GUI.Label(new Rect(10, yPosition, 400, 30), text, guiStyle);
		yPosition += 20;
	}

	public void SetGameplayState(GameplayState state) {

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

		player1.Reset();
		player2.Reset();

		player1.tower.AddStartingSegments();
		player2.tower.AddStartingSegments();

		SetGameplayState(GameplayState.Pregame);
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
			SetGameplayState(GameplayState.InProgress);
		}
	}

	void StopPregameState() {
		getReadyText.enabled = false;
	}

	// IN PROGRESS //////////////////////////////

	void SetupInProgressState() {
		Debug.Log("Setting up InProgress state.");
	}
	
	void UpdateInProgressState() {
		
	}

	void StopInProgressState() {

	}

	// ROUNDUP //////////////////////////////

	void SetupRoundupState() {
		Debug.Log("Setting up Roundup state.");
		winnerText.text = "Winner: " + (gameRulesManager.latestWinnerIsPlayer1 ? "P1" : "P2");
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





	//////////////////////
	// TESTING STUFF /////
	//////////////////////
	
	string destroyP1SegmentValue = "0";
	string destroyP2SegmentValue = "0";

	void DrawInGameTestingUi() {

		int yPosition = 0;
		AddDebugLabel("Gameplay state: " + gameplayState, ref yPosition);
		AddDebugLabel("", ref yPosition);
		AddDebugLabel("Player 1 Tower: " + player1.tower.GetTowerAscii(), ref yPosition);
		AddDebugLabel("Player 2 Tower: " + player2.tower.GetTowerAscii(), ref yPosition);
		AddDebugLabel("", ref yPosition);
		AddDebugLabel("Player 1 Tribes: " + player1.GetTribesSummary(), ref yPosition);
		AddDebugLabel("Player 2 Tribes: " + player2.GetTribesSummary(), ref yPosition);

		int buttonWidth = 200;
		int buttonHeight = 40;
		int p1XPosition = 250;

		//
		// Player 1 UI
		//
		if(GUI.Button(new Rect(p1XPosition, 200, buttonWidth, buttonHeight), "P1 Add segment")) {
			Test_AddP1Segment();
		}
		if(GUI.Button(new Rect(p1XPosition, 290, buttonWidth, buttonHeight), "Destroy segment (at index)")) {
			Test_DestroyP1Segment();
		}
		destroyP1SegmentValue = GUI.TextField(new Rect(p1XPosition, 330, buttonWidth, buttonHeight), destroyP1SegmentValue.ToString());
		if(GUI.Button(new Rect(p1XPosition, 380, buttonWidth, buttonHeight), "Start a task")) {
			player1.DoTestTask();
		}

		//
		// Player 2 UI
		//
		int p2XPosition = 450;
		if(GUI.Button(new Rect(p2XPosition, 200, buttonWidth, buttonHeight), "P2 Add segment")) {
			Test_AddP2Segment();
		}
		if(GUI.Button(new Rect(p2XPosition, 290, buttonWidth, buttonHeight), "Destroy segment (at index)")) {
			Test_DestroyP2Segment();
		}
		destroyP2SegmentValue = GUI.TextField(new Rect(p2XPosition, 330, buttonWidth, buttonHeight), destroyP2SegmentValue.ToString());
		if(GUI.Button(new Rect(p2XPosition, 380, buttonWidth, buttonHeight), "Start a task")) {
			player2.DoTestTask();
		}
	}

	void Test_AddP1Segment() {
		player1.tower.AddSegment();
		gameRulesManager.CheckForWinner(player1, player2);
	}

	void Test_AddP2Segment() {
		player2.tower.AddSegment();
		gameRulesManager.CheckForWinner(player1, player2);
	}

	void Test_DestroyP1Segment() {
		int intSegmentValue = int.Parse(destroyP1SegmentValue);
		player1.tower.DestroySegment(intSegmentValue);
	}

	void Test_DestroyP2Segment() {
		int intSegmentValue = int.Parse(destroyP2SegmentValue);
		player2.tower.DestroySegment(intSegmentValue);
	}
}
