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
		
		m_towerSegmentToBeConstructed = m_owningTower.m_staticTowerSegmentPrefab;
		
		SetAutoNextAction(true);
	}
	
	public override void OnProgressAction () {
		/* Complete instantly to move on to construction segment. */
		this.CompleteAction();
	}
	
	public override void OnCompleteAction () {
		this.SetNewTowerSegment(m_owningTower.m_constructionTowerSegmentPrefab);
	}
}
