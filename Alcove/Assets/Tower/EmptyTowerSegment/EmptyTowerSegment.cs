using UnityEngine;
using System.Collections;

public class EmptyTowerSegment : TowerSegment {

	private TowerSegment m_towerSegmentToBeConstructed;
	
	public TowerSegment GetTowerSegmentToBeConstructed() {
		return m_towerSegmentToBeConstructed;
	}
	
	public override bool OnIsActionable () {
		return true;
	}
	
	public override bool OnIsComplete () {
		return false;
	}
	
	public override void OnBeginAction () {
		/* TODO: Allow player to select new tower segment */
		m_towerSegmentToBeConstructed = m_owningTower.m_cannonTowerSegmentPrefab;
	}
	
	public override void OnCompleteAction () {
		// Swap the segment with a new construction one, and immediately begin its action
		ConstructionTowerSegment newSegment =
			(m_owningTower.SwapSegment(this, m_owningTower.m_constructionTowerSegmentPrefab)
			as ConstructionTowerSegment);
		newSegment.SetTowerSegmentToBeConstructed(GetTowerSegmentToBeConstructed());
		newSegment.PerformAction(GetOwningTower(), GetOwningTribe());
	}
}
