using UnityEngine;
using System.Collections;

public class StaticTowerSegment : TowerSegment {
	
	public override bool OnIsActionable () {
		return true;
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
