using UnityEngine;
using System.Collections;

public class WizartowerTowerSegment : TowerSegment {

	public AudioClip wizardTowerClip;

	public override float OnGetConstructionDuration() {
		return GameConstants.WIZARDTOWER_TOWER_SEGMENT_BUILD_TIME;
	}
	
	public override float OnGetActionDuration() {
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
	
	public override void OnCompleteAction () {
		TowerSegment opponentTowerSegment = m_owningTower.GetOpponentTowerSegmentPrefab(1);
		if (opponentTowerSegment != null) {
			AudioSource.PlayClipAtPoint(wizardTowerClip, Vector3.zero);
			m_owningTower.DestroyOpponentsSegment(1);
			m_owningTower.SwapSegment(this, opponentTowerSegment);
		}
		this.Reset ();
	}
}
