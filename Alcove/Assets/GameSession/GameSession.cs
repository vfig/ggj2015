using UnityEngine;
using System.Collections;

public class GameSession : MonoBehaviour {
	
	const bool SHOW_DEBUG_TEXT = true;

	public enum GameplayState {
		Pregame,
		InProgress,
		Roundup
	}
	
	public GameRulesManager gameRulesManager;
	
	GameplayState gameplayState;
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
	}

	void OnGUI() {
		if(SHOW_DEBUG_TEXT) {
			int yPosition = 0;
			DebugLabel("Gameplay state: " + gameplayState, ref yPosition);
			DebugLabel("Player 1 Tower: " + player1.tower.GetCompletedSegmentCount(), ref yPosition);
			DebugLabel("Player 2 Tower: " + player2.tower.GetCompletedSegmentCount(), ref yPosition);
		}
		DrawTestingUI();
	}

	void DebugLabel(string text, ref int yPosition) {
		GUI.Label(new Rect(10, yPosition, 200, 30), text);
		yPosition += 20;
	}

	public void SetGameplayState(GameplayState state) {

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





	//////////////////////
	// TESTING STUFF /////
	//////////////////////
	
	string destroyP1SegmentValue = "0";
	string destroyP2SegmentValue = "0";

	void DrawTestingUI() {

		int buttonWidth = 200;
		int buttonHeight = 40;
		int p1XPosition = 250;

		//
		// Player 1 UI
		//
		if(GUI.Button(new Rect(p1XPosition, 100, buttonWidth, buttonHeight), "P1 Add segment")) {
			Test_AddP1Segment();
		}
		if(GUI.Button(new Rect(p1XPosition, 190, buttonWidth, buttonHeight), "Destroy segment (at index)")) {
			Test_DestroyP1Segment();
		}
		destroyP1SegmentValue = GUI.TextField(new Rect(p1XPosition, 230, buttonWidth, buttonHeight), destroyP1SegmentValue.ToString());

		//
		// Player 2 UI
		//
		int p2XPosition = 450;
		if(GUI.Button(new Rect(p2XPosition, 100, buttonWidth, buttonHeight), "P2 Add segment")) {
			Test_AddP2Segment();
		}
		if(GUI.Button(new Rect(p2XPosition, 190, buttonWidth, buttonHeight), "Destroy segment (at index)")) {
			Test_DestroyP2Segment();
		}
		destroyP2SegmentValue = GUI.TextField(new Rect(p2XPosition, 230, buttonWidth, buttonHeight), destroyP2SegmentValue.ToString());
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
