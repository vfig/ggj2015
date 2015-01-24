using UnityEngine;
using System.Collections;

public class GameRulesManager : MonoBehaviour {
	
	const int TOWER_SEGMENTS_TO_WIN_GAME = 10;

	public GameSession gameSession;

	public void CheckForWinner(ShaunTempPlayer player1, ShaunTempPlayer player2) {
		if(player1.tower.GetCompletedSegmentCount() >= TOWER_SEGMENTS_TO_WIN_GAME) {
			AnnounceWinner(true);
		} else if(player2.tower.GetCompletedSegmentCount () >= TOWER_SEGMENTS_TO_WIN_GAME) {
			AnnounceWinner(false);
		}
	}

	public void AnnounceWinner(bool winnerIsPlayer1) {
		if(winnerIsPlayer1) {
			Debug.Log("Player 1 wins.");
		} else {
			Debug.Log("Player 2 wins.");
		}
		gameSession.SetGameplayState(GameSession.GameplayState.Roundup);
	}
}
