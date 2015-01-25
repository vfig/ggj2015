using UnityEngine;
using System.Collections;

public class WizartowerTowerSegment : TowerSegment {
	public override float NominalConstructionDurationSeconds() {
		return GameConstants.WIZARDTOWER_TOWER_SEGMENT_BUILD_TIME;
	}
	
	public override float NominalActionDurationSeconds() {
		return GameConstants.WIZARDTOWER_TOWER_SEGMENT_ACTION_TIME;
	}
	
	public override int OnGetTribeCost() {
		return GameConstants.WIZARDTOWER_TOWER_SEGMENT_TRIBE_COST;
	}
	
	public override bool OnIsActionable () {
		return true;
	}
	
	public override bool OnIsComplete () {
		return true;
	}
	
	public override void OnBeginAction () {
	}
	
	public override void OnProgressAction (float secondsRemaining) {
		// FIXME - animate arrows?
	}
	
	public override void OnCompleteAction () {
		TowerSegment opponentTowerSegment = m_owningTower.GetOpponentTowerSegmentPrefab(this);
		m_owningTower.DestroyOpponentsSegment(this);
		m_owningTower.SwapSegment(this, opponentTowerSegment);
		this.Reset ();
	}
}
