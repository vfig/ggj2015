using UnityEngine;
using System.Collections;

// CoreGameSession starts by creating players, ends when it detects a win condition
public class GameplayManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject recruitmentAreaPrefab;

	private GameSession gameSession;
	private GameObject minimap;
	private Player[] players;
	private RecruitmentArea recruitmentArea;

	public void Start() {

		// Obtain a reference to the main game session script.
		GameObject gameSessionGameObject = GameObject.Find("GameSession") as GameObject;
		if(gameSessionGameObject) {
			gameSession = gameSessionGameObject.GetComponent<GameSession> ();
		}

		// Spawn players, allocate world and viewport space for each
		const float worldXSpace = 10.0f;
		const float viewportMiddleArea = 0.12f;
		const float halfMiddleArea = viewportMiddleArea * 0.5f;
		// Player viewports each occupy half the screen minus half of the middle area.
		const float viewportXSpace = 0.5f - halfMiddleArea;
		players = new Player[GameConstants.PLAYER_COUNT];
		for (int i = 0; i < players.Length; ++i) {
			GameObject playerObject = (GameObject)Instantiate(
				playerPrefab,
				new Vector3(i * worldXSpace, 0, 0),
				Quaternion.identity);
			Player player = playerObject.GetComponent<Player>();
			player.playerNumber = i;
			if(i == 0) {
				player.camera.rect = new Rect(0, 0, viewportXSpace, 1);
			} else {
				player.camera.rect = new Rect(0.5f + halfMiddleArea, 0, viewportXSpace, 1);
			}
			player.SetOwningGameplayManager(this);
			players[i] = player;
		}

		float xPosition = (players[0].transform.position.x + players[1].transform.position.x) / 2;
		CreateMinimapObject(xPosition);

		// Create recruitment area based on central position between the two towers.
		CreateRecruitmentArea(players[0].tower, players[1].tower);
	}

	void CreateRecruitmentArea(Tower tower1, Tower tower2) {
		GameObject recruitmentAreaGameObject = Instantiate(recruitmentAreaPrefab) as GameObject;
		recruitmentArea = recruitmentAreaGameObject.GetComponent<RecruitmentArea>();
		float xPosition = (tower1.transform.position.x + tower2.transform.position.x) / 2;
		xPosition -= GameConstants.RECRUITMENT_AREA_GROUND_WIDTH / 2;
		Vector3 position = new Vector3(xPosition, GameConstants.RECRUITMENT_AREA_GROUND_Y, 0.0f);
		recruitmentArea.gameObject.transform.position = position;
	}

	void CreateMinimapObject(float xFocalPosition) {
		minimap = new GameObject();
		Camera minimapCamera = minimap.AddComponent<Camera>();

		float widthFraction = 0.12f;
		minimapCamera.isOrthoGraphic = true;
		minimapCamera.rect = new Rect(0.5f - widthFraction/2, 0.0f, widthFraction, 1.0f);
		minimapCamera.backgroundColor = new Color(0.3f, 0.3f, 0.3f, 0.6f);
		minimapCamera.orthographicSize = 28.0f;
		minimap.camera.transform.position = new Vector3(xFocalPosition, 25.0f, -5.0f);
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
	
	public void DestroyOpponentsSegment(int playerIndex, int segmentIndex) {
		if (playerIndex == 0) {
			players[1].DestroyPlayersSegment(segmentIndex);
		} else if (playerIndex == 1) {
			players[0].DestroyPlayersSegment(segmentIndex);
		}
	}
	
	public void DestroyAllRecruits() {
		recruitmentArea.DestroyAllUnits();
	}
	
	public void CollectRecruits(Tribe tribe) {
		tribe.Recruit (recruitmentArea.DestroyAllUnitsOfColour(tribe.m_unitColour));
	}
	
	public TowerSegment GetOpponentTowerSegmentPrefab(int playerIndex, int segmentIndex) {
		if (playerIndex == 0) {
			return players[1].GetPlayersTowerSegmentPrefab(segmentIndex);
		} else if (playerIndex == 1) {
			return players[0].GetPlayersTowerSegmentPrefab(segmentIndex);
		}
		return null;
	}
}
