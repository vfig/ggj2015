using UnityEngine;
using System.Collections;

public interface ITowerSegmentCallback {
	void OnBeginAction(TowerSegment segment);
	void OnProgressAction(TowerSegment segment, float progress, float secondsRemaining);
	void OnCompleteAction(TowerSegment segment);
}
