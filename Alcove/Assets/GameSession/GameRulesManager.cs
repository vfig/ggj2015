using UnityEngine;
using System.Collections;

public class GameRulesManager : MonoBehaviour {
	
	const int TOWER_SEGMENTS_TO_WIN_GAME = 10;

	public GameSession gameSession;

	public bool latestWinnerIsPlayer1 = false;

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
			latestWinnerIsPlayer1 = true;
		} else {
			Debug.Log("Player 2 wins.");
			latestWinnerIsPlayer1 = false;
		}
		gameSession.SetGameplayState(GameSession.GameplayState.Roundup);
	}
}
