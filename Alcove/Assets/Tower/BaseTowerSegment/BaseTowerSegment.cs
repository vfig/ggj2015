using UnityEngine;
using System.Collections;

public class BaseTowerSegment : TowerSegment {
	public override bool OnIsActionable () {
		return true;
	}
	
	public override float NominalActionDurationSeconds() {
		return 10.0f;
	}
	
	public override void OnBeginAction () {
	}
	
	public override void OnProgressAction (float secondsRemaining) {
	}
	
	public override void OnCompleteAction () {
	}
}
