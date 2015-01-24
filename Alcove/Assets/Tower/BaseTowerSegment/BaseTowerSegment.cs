using UnityEngine;
using System.Collections;

public class BaseTowerSegment : TowerSegment {
	
	public override bool OnIsActionable () {
		return false;
	}
	
	public override bool OnIsComplete () {
		return true;
	}
	
	public override void OnBeginAction () {
	}
	
	public override void OnProgressAction () {
	}
	
	public override void OnCompleteAction () {
	}
}
