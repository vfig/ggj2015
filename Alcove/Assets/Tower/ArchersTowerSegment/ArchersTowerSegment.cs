using UnityEngine;
using System.Collections;

public class ArchersTowerSegment : TowerSegment {

	private bool m_inUse;
	
	public override float NominalConstructionDurationSeconds() {
		return GameConstants.ARCHERS_TOWER_SEGMENT_BUILD_TIME;
	}
	
	public override float NominalActionDurationSeconds() {
		return GameConstants.ARCHERS_TOWER_SEGMENT_ACTION_TIME;
	}
	
	public override int OnGetTribeCost() {
		return GameConstants.ARCHERS_TOWER_SEGMENT_TRIBE_COST;
	}
	
	public override bool OnIsActionable () {
		return (!m_inUse);
	}
	
	public override bool OnIsComplete () {
		return true;
	}
	
	public override void OnBeginAction () {
		m_inUse = true;
	}
	
	public override void OnProgressAction (float secondsRemaining) {
		// FIXME - animate arrows?
	}
	
	public override void OnCompleteAction () {
		m_owningTower.DestroyOpponentsSegment(this);
		this.Reset ();
		m_inUse = false;
	}
}
