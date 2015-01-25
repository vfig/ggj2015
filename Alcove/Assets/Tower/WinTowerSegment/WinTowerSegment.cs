using UnityEngine;
using System.Collections;

public class WinTowerSegment : TowerSegment {
	public override float OnGetConstructionDuration() {
		return GameConstants.WIN_TOWER_SEGMENT_BUILD_TIME;
	}
	
	public override int OnGetMinimumTribeSize() {
		return GameConstants.WIN_TOWER_SEGMENT_TRIBE_SIZE;
	}

	public override bool OnIsActionable () {
		return false;
	}
	
	public override bool OnIsComplete () {
		return true;
	}

	public override bool IsFinalSegment() {
		return true;
	}
}
