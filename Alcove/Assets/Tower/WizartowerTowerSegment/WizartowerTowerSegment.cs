using UnityEngine;
using System.Collections;

public class WizartowerTowerSegment : TowerSegment {
	public override float NominalConstructionDurationSeconds() {
		return 30.0f;
	}
	
	public override float NominalActionDurationSeconds() {
		return 30.0f;
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
