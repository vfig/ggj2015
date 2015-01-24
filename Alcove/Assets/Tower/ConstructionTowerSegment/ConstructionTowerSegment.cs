using UnityEngine;
using System.Collections;

public class ConstructionTowerSegment : TowerSegment {

	private float m_constructionRate;
	private float m_completion;
	private float m_target;
	
	public override bool OnIsActionable () {
		return false;
	}
	
	public override bool OnIsComplete () {
		return (m_completion == 1.0f);
	}
	
	public override void OnBeginAction () {
		m_constructionRate = 0.02f;
		m_completion = 0.0f;
		m_target = 1.0f;
	}
	
	public override void OnProgressAction () {
		m_completion += m_constructionRate;
		if (m_completion >= m_target) {
			m_completion = m_target;
			this.CompleteAction();
		}
	}
	
	public override void OnCompleteAction () {
		this.SetNewTowerSegment(m_owningTower.m_staticTowerSegmentPrefab);
	}
}
