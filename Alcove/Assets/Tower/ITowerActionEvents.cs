using UnityEngine;
using System.Collections;

public interface ITowerActionEvents {
	void TowerActionStarted(TowerAction action);
	void TowerActionProgress(TowerAction action, float progress, float secondsRemaining);
	void TowerActionCompleted(TowerAction action);
}
