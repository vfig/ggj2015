using UnityEngine;
using System.Collections;

public class Tribe : MonoBehaviour, ITowerSegmentCallback {
	private int m_count;
	private int m_unitLimit;
	private bool busy;
	private float busyFraction;
	private float busyRemaining;

	public UnitColour m_unitColour;

	public int Count { get { return m_count; } set { m_count = value; } }
	public int UnitLimit { get { return m_unitLimit; } }

	// Return true if the tribe is busy
	public bool IsBusy {
		get {
			return busy;
		}
	}

	// Return remaining busy amount as float in range 0..1
	public float BusyFraction {
		get {
			if (busy) {
				return busyFraction;
			} else {
				return 0.0f;
			}
		}
	}

	// Return remaining busy time as whole number of seconds
	public int BusySeconds {
		get {
			if (busy) {
				return (int)Mathf.Ceil(busyRemaining);
			} else {
				return 0;
			}
		}
	}

	public void OnBeginAction(TowerSegment segment) {
		busy = true;
	}

	public void OnProgressAction(TowerSegment segment, float progress, float secondsRemaining) {
		busyFraction = progress;
		busyRemaining = secondsRemaining;
	}

	public void OnCompleteAction(TowerSegment segment) {
		busy = false;
	}
	
	public void UpdateUnitLimit(int unitLimit) {
		m_unitLimit = unitLimit;
		if (m_count > m_unitLimit) {
			m_count = m_unitLimit;
		}
	}
	
	public void Recruit(int delta) {
		m_count += delta;
		if (m_count > m_unitLimit) {
			m_count = m_unitLimit;
		}
	}
}
