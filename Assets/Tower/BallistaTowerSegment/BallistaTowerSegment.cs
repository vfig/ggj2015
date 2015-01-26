using UnityEngine;
using System.Collections;

public class BallistaTowerSegment : TowerSegment {

	public AudioClip ballistaClip;
	private bool m_inUse;
	
	public override float OnGetConstructionDuration() {
		return GameConstants.BALLISTA_TOWER_SEGMENT_BUILD_TIME;
	}
	
	public override float OnGetActionDuration() {
		return GameConstants.BALLISTA_TOWER_SEGMENT_ACTION_TIME;
	}
	
	public override int OnGetTribeCost() {
		return GameConstants.BALLISTA_TOWER_SEGMENT_TRIBE_COST;
	}
	
	public override bool OnIsActionable () {
		return (!m_inUse);
	}
	
	public override bool OnIsComplete () {
		return true;
	}
	
	public override void OnBeginAction (float secondsRemaining) {
		m_inUse = true;
	}
	
	public override void OnProgressAction (float secondsRemaining) {
		// FIXME - animate arrows?
	}
	
	public override void OnCompleteAction () {
		m_owningTower.DestroyOpponentsSegment(this);
		AudioSource.PlayClipAtPoint(ballistaClip, Vector3.zero);
		this.Reset ();
		m_inUse = false;
	}
}
