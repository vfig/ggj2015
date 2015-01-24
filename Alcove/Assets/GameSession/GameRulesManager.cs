using UnityEngine;
using System.Collections;

public class GameRulesManager : MonoBehaviour {
	public const int PLAYER_COUNT = 2;
	public const int TOWER_SEGMENTS_TO_WIN_GAME = 15;
	public const int NUM_TRIBES_PER_PLAYER = 4;
	public const int TRIBE_STARTING_UNIT_COUNT = 8;

	public GameSession gameSession;
}
