using UnityEngine;
using System.Collections;

public class ConstructionTowerSegment : TowerSegment {
	private TowerSegment m_towerSegmentToBeConstructed;
	
	public void SetTowerSegmentToBeConstructed(TowerSegment prefab) {
		m_towerSegmentToBeConstructed = prefab;
	}

	public override float NominalActionDurationSeconds() {
		return m_towerSegmentToBeConstructed.NominalConstructionDurationSeconds();
	}

	public override bool OnIsComplete () {
		return false;
	}

	public override void OnCompleteAction () {
		// Swap the segment with a new one
		m_owningTower.SwapSegment(this, m_towerSegmentToBeConstructed);
		// Add a new empty one ready to build
		m_owningTower.AddTowerSegment(m_owningTower.m_emptyTowerSegmentPrefab);
	}
}
