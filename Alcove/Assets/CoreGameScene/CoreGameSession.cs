using UnityEngine;
using System.Collections;

// CoreGameSession starts by creating players, ends when it detects a win condition
public class CoreGameSession : MonoBehaviour {
	public GameObject playerPrefab;

	private Player[] players;

	public void Start () {
		// Spawn players, allocate world and viewport space for each
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

	public void LateUpdate () {
		if (CheckForWin()) {
			// FIXME:
			// Disable player actions
			// Tell GameState manager that this session is done
		}
	}

	private bool CheckForWin() {
		// FIXME: Look for a win
		return false;
	}
}
