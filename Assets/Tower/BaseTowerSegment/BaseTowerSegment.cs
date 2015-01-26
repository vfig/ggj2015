using UnityEngine;
using System.Collections;

public class BaseTowerSegment : TowerSegment {

	private bool m_inUSe;
	public AudioClip recruitClip;

	public override bool OnIsActionable () {
		return (m_inUSe == false);
	}
	
	public override float OnGetActionDuration() {
		return GameConstants.BASE_TOWER_SEGMENT_ACTION_TIME;
	}

	public override float OnGetActionWorkRate() {
		// No speed boosts for laboratories or tribe size
		return 1.0f;
	}

	public override void OnBeginAction (float secondsRemaining) {
		m_inUSe = true;
		AudioSource.PlayClipAtPoint(recruitClip, Vector3.zero);
		m_owningTower.BeginCollectRecruits(m_currentTribe, secondsRemaining);
	}

	public override void OnCompleteAction () {
		m_owningTower.CollectRecruits(m_currentTribe);
		this.Reset ();
		m_inUSe = false;
	}

	public override bool ShowsWorkingArea() {
		return false;
	}
}
