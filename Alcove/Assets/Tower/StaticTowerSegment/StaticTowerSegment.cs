using UnityEngine;
using System.Collections;

public class StaticTowerSegment : TowerSegment {
	public override float NominalConstructionDurationSeconds() {
		return 5;
	}
}
