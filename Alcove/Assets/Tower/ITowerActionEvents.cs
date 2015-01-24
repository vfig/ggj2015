using UnityEngine;
using System.Collections;

public interface ITowerActionEvents {
	void TowerActionStarted(TowerSegment segment);
	void TowerActionProgress(TowerSegment segment, float progress, float secondsRemaining);
	void TowerActionCompleted(TowerSegment segment);
}
