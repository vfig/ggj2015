using UnityEngine;
using System.Collections;

// CoreGameSession starts by creating players, ends when it detects a win condition
public class GameplayManager : MonoBehaviour {

	public GameObject playerPrefab;

	private GameSession gameSession;
	private GameObject minimap;
	private Player[] players;

	public void Start() {

		// Obtain a reference to the main game session script.
		GameObject gameSessionGameObject = GameObject.Find("GameSession") as GameObject;
		if(gameSessionGameObject) {
			gameSession = gameSessionGameObject.GetComponent<GameSession> ();
		}

		// Spawn players, allocate world and viewport space for each
		const float worldXSpace = 10.0f;
		const float viewportXSpace = 1.0f / (float)GameConstants.PLAYER_COUNT;
		players = new Player[GameConstants.PLAYER_COUNT];
		for (int i = 0; i < players.Length; ++i) {
			GameObject playerObject = (GameObject)Instantiate(
				playerPrefab,
				new Vector3(i * worldXSpace, 0, 0),
				Quaternion.identity);
			Player player = playerObject.GetComponent<Player>();
			player.playerNumber = i;
			player.camera.rect = new Rect(i * viewportXSpace, 0, viewportXSpace, 1);
			players[i] = player;
		}

		float xPosition = (players[0].transform.position.x + players[1].transform.position.x) / 2;
		CreateMinimapObject(xPosition);
	}

	void CreateMinimapObject(float xFocalPosition) {
		minimap = new GameObject();
		Camera minimapCamera = minimap.AddComponent<Camera>();

		float widthFraction = 0.1f;
		float heightFraction = 0.3f;
		minimapCamera.isOrthoGraphic = true;
		minimapCamera.rect = new Rect(0.5f - widthFraction/2, 0.5f - heightFraction/2, widthFraction, heightFraction);
		minimapCamera.backgroundColor = new Color(0.3f, 0.3f, 0.3f, 0.6f);
		minimapCamera.orthographicSize = 16.0f;
		minimap.camera.transform.position = new Vector3(xFocalPosition, 0.0f, -5.0f);
	}

	public void LateUpdate() {
		CheckForWinner();
	}

	public void CheckForWinner() {
		if(gameSession == null) {
			return;
		}
		int winner = -1; // 0 or 1 for player (zero-indexed), -1 for no winner. 
		RoundupInfo info = new RoundupInfo();
		int p1Segments = players[0].tower.GetCompletedSegmentCount();
		int p2Segments = players[1].tower.GetCompletedSegmentCount();
		if(p1Segments >= GameConstants.TOWER_SEGMENTS_TO_WIN_GAME) {
			winner = 0;
			info.winningPlayerText = "Player 1";
		}
		if(p2Segments >= GameConstants.TOWER_SEGMENTS_TO_WIN_GAME) {
			winner = 1;
			info.winningPlayerText = "Player 2";
		}

		if(winner != -1) {
			gameSession.SetState(GameSession.GameplayState.Roundup, info);
		}
	}

	public Player GetPlayer(int index) {
		return players[index];
	}
}
