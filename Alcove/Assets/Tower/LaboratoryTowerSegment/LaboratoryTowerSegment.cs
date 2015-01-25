using UnityEngine;
using System.Collections;

public class LaboratoryTowerSegment : TowerSegment {
	private bool m_inUSe;
	
	public override bool OnIsActionable () {
		return (m_inUSe == false);
	}
	
	public override float OnGetConstructionDuration() {
		return GameConstants.LABORATORY_TOWER_SEGMENT_BUILD_TIME;
	}
	
	public override float OnGetActionDuration() {
		return GameConstants.LABORATORY_TOWER_SEGMENT_ACTION_TIME;
	}

	public override float OnGetActionWorkRate() {
		return 1.0f / m_workerCount;
	}

	public override int OnGetTribeCost() {
		return GameConstants.LABORATORY_TOWER_SEGMENT_TRIBE_COST;
	}
	
	public override void OnBeginAction (float secondsRemaining) {
		m_inUSe = true;
		m_owningTower.RegisterLaboratory();
	}
	
	public override void OnCompleteAction () {
		m_owningTower.UnRegisterLaboratory();
		this.Reset ();
		m_inUSe = false;
	}
}
