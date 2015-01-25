using UnityEngine;
using System.Collections;

public class CannonTowerSegment : TowerSegment {
	private bool m_fired = false;

	public override float NominalConstructionDurationSeconds() {
		return 200.0f;
	}

	public override float NominalActionDurationSeconds() {
		return 500.0f;
	}

	public override bool OnIsActionable () {
		return !m_fired;
	}
	
	public override bool OnIsComplete () {
		return true;
	}
	
	public override void OnBeginAction () {
		m_fired = true;
	}
	
	public override void OnProgressAction (float secondsRemaining) {
		// FIXME - animate cannon fire?
	}
	
	public override void OnCompleteAction () {
		m_owningTower.DestroyOpponentsSegment(this);
		this.Reset ();
	}
}
