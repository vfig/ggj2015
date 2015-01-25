﻿using UnityEngine;
using System.Collections;

public class BaseTowerSegment : TowerSegment {

	private bool m_inUSe;

	public override bool OnIsActionable () {
		return (m_inUSe == false);
	}
	
	public override float NominalActionDurationSeconds() {
		return 50.0f;
	}
	
	public override void OnBeginAction () {
		m_inUSe = true;
	}
	
	public override void OnProgressAction (float secondsRemaining) {
	}
	
	public override void OnCompleteAction () {
		m_owningTower.CollectRecruits(m_currentTribe);
		this.Reset ();
		m_inUSe = false;
	}
}
