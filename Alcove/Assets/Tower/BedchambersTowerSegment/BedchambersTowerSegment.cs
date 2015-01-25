using UnityEngine;
using System.Collections;

public class BedchambersTowerSegment : TowerSegment {
	public override float NominalConstructionDurationSeconds() {
		return GameConstants.BEDCHAMBERS_TOWER_SEGMENT_BUILD_TIME;
	}
}
