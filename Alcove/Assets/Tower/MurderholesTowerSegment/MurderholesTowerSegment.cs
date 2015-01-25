using UnityEngine;
using System.Collections;

public class MurderholesTowerSegment : TowerSegment {

	public AudioClip murderHoleClip;

	public override float OnGetConstructionDuration() {
		return GameConstants.MURDERHOLES_TOWER_SEGMENT_BUILD_TIME;
	}
	
	public override float OnGetActionDuration() {
		return GameConstants.MURDERHOLES_TOWER_SEGMENT_ACTION_TIME;
	}
	
	public override int OnGetTribeCost() {
		return GameConstants.MURDERHOLES_TOWER_SEGMENT_TRIBE_COST;
	}
	
	public override bool OnIsActionable () {
		return true;
	}
	
	public override bool OnIsComplete () {
		return true;
	}
	
	public override void OnCompleteAction () {
		m_owningTower.DestroyAllRecruits();
		AudioSource.PlayClipAtPoint(murderHoleClip, Vector3.zero);
		this.Reset ();
	}
}
