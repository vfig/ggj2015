using UnityEngine;
using System.Collections;

public class BaseTowerSegment : TowerSegment {
	public override bool OnIsActionable () {
		return true;
	}
	
	public override float NominalActionDurationSeconds() {
		return 100.0f;
	}
	
	public override void OnBeginAction () {
	}
	
	public override void OnProgressAction (float secondsRemaining) {
	}
	
	public override void OnCompleteAction () {
		m_owningTower.CollectRecruits(m_currentTribe);
		this.Reset ();
	}
}
