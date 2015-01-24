using UnityEngine;
using System.Collections;

public class CannonTowerSegment : TowerSegment {

	public override bool OnIsActionable () {
		return true;
	}
	
	public override bool OnIsComplete () {
		return true;
	}
	
	public override void OnBeginAction () {
		m_owningTower.DestroyOpponentsSegment(this);
	}
	
	public override void OnProgressAction (float secondsRemaining) {
	}
	
	public override void OnCompleteAction () {
	}
}
