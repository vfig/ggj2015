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
	public GameObject goPanel;
	
	GameplayManager gameplayManager;
	GameplayState state;
	float stateCounter;

	void Start() {
		StartNewGame();
	}

	void Update() {
		stateCounter += (Time.deltaTime * 60);
		switch(state) {
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
		switch(state) {
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
	
	public void SetState(GameplayState newState) {

		// Handle closure of previous state.
		switch(state) {
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

		stateCounter = 0;
		state = newState;

		switch(newState) {
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

		// This method's for initialisation of a game round,
		// and SetupPregameState can be used to introduce the game
		// (panning of camera or whatever).

		Player.canUpdate = false;
		pregamePanel.SetActive(false);
		roundupPanel.SetActive(false);
		goPanel.SetActive(false);

		SetState(GameplayState.LoadGameplayScene);
	}
	
	// PREGAME //////////////////////////////

	void Setup_Pregame() {
		// This state's not for initialisation,
		// it's for any sort of intro we have.
		pregamePanel.SetActive(true);
	}

	void Update_Pregame() {
		int displayTime = GameConstants.PREGAME_DISPLAY_TIME;
		if(stateCounter > displayTime) {
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
		Player.canUpdate = true;
		goPanel.SetActive(true);
		CanvasRenderer canvas = goPanel.GetComponent<CanvasRenderer>();
		canvas.SetAlpha(1.0f);
	}

	void Update_InProgress() {
		if(stateCounter > 40) {
			CanvasRenderer canvas = goPanel.GetComponent<CanvasRenderer> ();
			canvas.SetAlpha (canvas.GetAlpha () - 0.1f);
			if (canvas.GetAlpha () <= 0.0f) {
					goPanel.SetActive (false);
			}
		}
	}

	void Stop_InProgress() {
		goPanel.SetActive(false);
	}

	// ROUNDUP //////////////////////////////

	void Setup_Roundup() {
		// FIXME: Get the winner ID to this method somehow.
		Player.canUpdate = false;
		roundupPanel.SetActive(true);
		//winnerText.text = "Winner: " + "Not specified";
	}

	void Update_Roundup() {
		if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel("StartScene");
		}
	}

	void Stop_Roundup() {
		roundupPanel.SetActive(false);
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
