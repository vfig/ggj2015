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
		player1.UpdateTemp();
		player2.UpdateTemp();
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
		QuickTopLeftText("", ref yPosition);
		QuickTopLeftText("Gameplay state: " + gameplayState, ref yPosition);

		//
		// Player 1 UI
		//
		int p1XPosition = 200;
		yPosition = 220;

		QuickText("Player 1", p1XPosition, ref yPosition);
		QuickText(player1.GetTribesSummary(), p1XPosition, ref yPosition);
		QuickText("Tower: " + player1.tower.GetTowerAscii(), p1XPosition, ref yPosition);
		if(QuickButton("Destroy segment (at index)", p1XPosition, ref yPosition)) {
			Test_DestroyP1Segment();
		}
		destroyP1SegmentValue = QuickTextField (destroyP1SegmentValue, p1XPosition, ref yPosition);
		if(QuickButton("Tribe 1", p1XPosition, ref yPosition, 100, player1.IsTribeAvailable(0))) {
			player1.NominateTribeForActionSelection(0);
		}
		if(QuickButton("Tribe 2", p1XPosition, ref yPosition, 100, player1.IsTribeAvailable(1))) {
			player1.NominateTribeForActionSelection(1);
		}
		if(QuickButton("Tribe 3", p1XPosition, ref yPosition, 100, player1.IsTribeAvailable(2))) {
			player1.NominateTribeForActionSelection(2);
		}
		if(QuickButton("Tribe 4", p1XPosition, ref yPosition, 100, player1.IsTribeAvailable(3))) {
			player1.NominateTribeForActionSelection(3);
		}
		yPosition -= 180;
		if(QuickButton("(Up) Build", p1XPosition+100, ref yPosition, 100, player1.IsTribeNominated())) {
			player1.PerformActionWithNominatedTribe(ActionType.Build);
		}
		if(QuickButton("(Left) Defend", p1XPosition+100, ref yPosition, 100, player1.IsTribeNominated())) {
			player1.PerformActionWithNominatedTribe(ActionType.Defend);
		}
		if(QuickButton("(Right) Recruit", p1XPosition+100, ref yPosition, 100, player1.IsTribeNominated())) {
			player1.PerformActionWithNominatedTribe(ActionType.Recruit);
		}
		if(QuickButton("(Down) Occupy", p1XPosition+100, ref yPosition, 100, player1.IsTribeNominated())) {
			player1.PerformActionWithNominatedTribe(ActionType.Occupy);
		}

		//
		// Player 2 UI
		//
		yPosition = 220;
		int p2XPosition = 540;

		QuickText("Player 2", p2XPosition, ref yPosition);
		QuickText(player2.GetTribesSummary(), p2XPosition, ref yPosition);
		QuickText("Tower: " + player2.tower.GetTowerAscii(), p2XPosition, ref yPosition);
		if(QuickButton("Destroy segment (at index)", p2XPosition, ref yPosition)) {
			Test_DestroyP2Segment();
		}
		destroyP2SegmentValue = QuickTextField (destroyP1SegmentValue, p2XPosition, ref yPosition);
		if(QuickButton("Tribe 1", p2XPosition, ref yPosition, 100, player2.IsTribeAvailable(0))) {
			player2.NominateTribeForActionSelection(0);
		}
		if(QuickButton("Tribe 2", p2XPosition, ref yPosition, 100, player2.IsTribeAvailable(1))) {
			player2.NominateTribeForActionSelection(1);
		}
		if(QuickButton("Tribe 3", p2XPosition, ref yPosition, 100, player2.IsTribeAvailable(2))) {
			player2.NominateTribeForActionSelection(2);
		}
		if(QuickButton("Tribe 4", p2XPosition, ref yPosition, 100, player2.IsTribeAvailable(3))) {
			player2.NominateTribeForActionSelection(3);
		}
		yPosition -= 180;
		if(QuickButton("(Up) Build", p2XPosition+100, ref yPosition, 100, player2.IsTribeNominated())) {
			player2.PerformActionWithNominatedTribe(ActionType.Build);
		}
		if(QuickButton("(Right) Defend", p2XPosition+100, ref yPosition, 100, player2.IsTribeNominated())) {
			player2.PerformActionWithNominatedTribe(ActionType.Defend);
		}
		if(QuickButton("(Left) Recruit", p2XPosition+100, ref yPosition, 100, player2.IsTribeNominated())) {
			player2.PerformActionWithNominatedTribe(ActionType.Recruit);
		}
		if(QuickButton("(Down) Occupy", p2XPosition+100, ref yPosition, 100, player2.IsTribeNominated())) {
			player2.PerformActionWithNominatedTribe(ActionType.Occupy);
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
