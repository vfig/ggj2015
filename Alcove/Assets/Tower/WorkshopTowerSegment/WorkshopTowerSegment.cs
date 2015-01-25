using UnityEngine;
using System.Collections;

public class WorkshopTowerSegment : TowerSegment {
	private bool m_inUSe;
	
	public override bool OnIsActionable () {
		return (m_inUSe == false);
	}
	
	public override float OnGetConstructionDuration() {
		return GameConstants.WORKSHOP_TOWER_SEGMENT_BUILD_TIME;
	}
	
	public override float OnGetActionDuration() {
		return GameConstants.WORKSHOP_TOWER_SEGMENT_ACTION_TIME;
	}

	public override float OnGetActionWorkRate() {
		return 1.0f / m_workerCount;
	}

	public override int OnGetTribeCost() {
		return GameConstants.WORKSHOP_TOWER_SEGMENT_TRIBE_COST;
	}
	
	public override void OnBeginAction (float secondsRemaining) {
		m_inUSe = true;
		m_owningTower.RegisterWorkshop();
	}
	
	public override void OnCompleteAction () {
		m_owningTower.UnRegisterWorkshop();
		this.Reset ();
		m_inUSe = false;
	}
}
