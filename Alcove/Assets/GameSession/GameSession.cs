using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// GameSession handles pre- and post- core game stuff, and is responsible for kicking off the core game.
public class GameSession : MonoBehaviour {

	public enum GameplayState {
		LoadGameplayScene,
		Pregame,
		InProgress,
		Roundup
	}

	public GUIStyle guiStyle;
	public GameObject pregamePanel;
	public GameObject roundupPanel;
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
		case GameplayState.LoadGameplayScene:
			Update_LoadGameplayScene();
			break;
		case GameplayState.Pregame:
			Update_Pregame();
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
		case GameplayState.LoadGameplayScene:
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
		case GameplayState.LoadGameplayScene:
			break;
		case GameplayState.Pregame:
			Stop_Pregame();
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
		case GameplayState.LoadGameplayScene:
			Setup_LoadGameplayScene();
			break;
		case GameplayState.Pregame:
			Setup_Pregame();
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

		pregamePanel.SetActive(false);
		roundupPanel.SetActive(false);
		clickToContinueText.enabled = false;

		SetState(GameplayState.LoadGameplayScene);
	}
	
	// PREGAME //////////////////////////////

	void Setup_Pregame() {
		// This state's not for initialisation,
		// it's for any sort of intro we have.
		//Debug.Log("Setting up Pregame state.");
		pregamePanel.SetActive(true);
	}

	void Update_Pregame() {
		int displayTime = GameConstants.PREGAME_DISPLAY_TIME;
		if(gamestateCounter > displayTime) {
			SetState(GameplayState.InProgress);
		}
	}

	void Stop_Pregame() {
		pregamePanel.SetActive(false);
	}

	// LOADING LEVEL //////////////////////////////

	void Setup_LoadGameplayScene() {
		Application.LoadLevelAdditive("GameplaySubScene");
	}

	void Update_LoadGameplayScene() {
		if(GrabGameplayManagerReference()) {
			SetState(GameplayState.Pregame);
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
		roundupPanel.SetActive (true);
		//winnerText.text = "Winner: " + "Not specified";
		clickToContinueText.enabled = true;
	}

	void Update_Roundup() {
		if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel("StartScene");
		}
	}

	void Stop_Roundup() {
		roundupPanel.SetActive(false);
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
