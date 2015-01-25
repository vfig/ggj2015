using UnityEngine;
using System.Collections;

public class BedchambersTowerSegment : TowerSegment {
	public override float OnGetConstructionDuration() {
		return GameConstants.BEDCHAMBERS_TOWER_SEGMENT_BUILD_TIME;
	}
}
