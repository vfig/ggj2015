using UnityEngine;
using System.Collections;

public class WorkshopTowerSegment : TowerSegment {
	private bool m_inUSe;
	
	public override bool OnIsActionable () {
		return (m_inUSe == false);
	}
	
	public override float NominalConstructionDurationSeconds() {
		return 20.0f;
	}
	
	public override float NominalActionDurationSeconds() {
		return 50.0f;
	}
	
	public override void OnBeginAction () {
		m_inUSe = true;
		m_owningTower.RegisterWorkshop();
	}
	
	public override void OnProgressAction (float secondsRemaining) {
	}
	
	public override void OnCompleteAction () {
		m_owningTower.UnRegisterWorkshop();
		this.Reset ();
		m_inUSe = false;
	}
}
