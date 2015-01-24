﻿using UnityEngine;
using System.Collections;

// CoreGameSession starts by creating players, ends when it detects a win condition
public class CoreGameSession : MonoBehaviour {

	public GameObject playerPrefab;

	private GameSession gameSession;
	private GameRulesManager gameRulesManager;
	private Player[] players;

	public void Start () {
		// Spawn players, allocate world and viewport space for each

		GameObject gameRulesObject = new GameObject();
		gameRulesObject.AddComponent<GameRulesManager>();

		const float worldXSpace = 10.0f;
		const float viewportXSpace = 1.0f / (float)GameRulesManager.PLAYER_COUNT;
		players = new Player[GameRulesManager.PLAYER_COUNT];
		for (int i = 0; i < players.Length; ++i) {
			GameObject playerObject = (GameObject)Instantiate(
				playerPrefab,
				new Vector3(i * worldXSpace, 0, 0),
				Quaternion.identity);
			Player player = playerObject.GetComponent<Player>();
			player.playerNumber = i;
			player.camera.rect = new Rect(i * viewportXSpace, 0, viewportXSpace, 1);
		}
	}

	public void LateUpdate() {
		CheckForWinner();
	}

	public void CheckForWinner() {
		int winner = -1; // 0 or 1 for player (zero-indexed), -1 for no winner. 
		int p1Segments = players[0].tower.GetCompletedSegmentCount();
		int p2Segments = players[1].tower.GetCompletedSegmentCount();
		if(p1Segments > GameRulesManager.TOWER_SEGMENTS_TO_WIN_GAME) {
			winner = 0;
		}
		if(p2Segments > GameRulesManager.TOWER_SEGMENTS_TO_WIN_GAME) {
			winner = 1;
		}

		Application.LoadLevel("StartScene");
		// FIXME: Once we're using LoadLevelAdditive to merge the old GameSession and
		// CoreGameSession, we should be setting state to Roundup here.
		//gameSession.SetState(GameSession.GameplayState.Roundup);
	}
}
