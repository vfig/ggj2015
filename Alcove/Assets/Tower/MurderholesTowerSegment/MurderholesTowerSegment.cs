using UnityEngine;
using System.Collections;

public class MurderholesTowerSegment : TowerSegment {
	public override float NominalConstructionDurationSeconds() {
		return 100.0f;
	}
	
	public override float NominalActionDurationSeconds() {
		return 100.0f;
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
		m_owningTower.DestroyAllRecruits();
		this.Reset ();
	}
}
