using UnityEngine;
using System.Collections;

public class BaseTowerSegment : TowerSegment {
	public override bool OnIsActionable () {
		return false;
	}
}
