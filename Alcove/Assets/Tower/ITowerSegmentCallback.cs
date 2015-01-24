using UnityEngine;
using System.Collections;

public interface ITowerSegmentCallback {
	void TowerSegmentActionStarted(TowerSegment segment);
	void TowerSegmentActionProgress(TowerSegment segment, float progress, float secondsRemaining);
	void TowerSegmentActionCompleted(TowerSegment segment);
}
