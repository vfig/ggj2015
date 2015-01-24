using UnityEngine;
using System.Collections;

public class ShaunTempTower : MonoBehaviour {

	int completedSegments;

	public void StartTemp() {
	}

	public void Reset() {
		completedSegments = 0;
	}

	public void AddStartingSegments() {
		// Add the 2 or 3 starting segments we decide upon.
		completedSegments = 3;
	}

	public void AddSegment() {
		completedSegments++;
	}

	public void DestroySegment(int index) {
		completedSegments--;
		if(completedSegments < 0) {
			completedSegments = 0;
		}
	}

	public int GetCompletedSegmentCount() {
		return completedSegments;
	}

	void Update() {
	}
}
