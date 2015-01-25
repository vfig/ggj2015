using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConstructionTowerSegment : TowerSegment {
	private TowerSegment m_towerSegmentToBeConstructed;
	private Image m_constructionImage;

	public void SetTowerSegmentToBeConstructed(TowerSegment prefab) {
		m_towerSegmentToBeConstructed = prefab;
	}

	public override float OnGetActionDuration() {
		return m_towerSegmentToBeConstructed.OnGetConstructionDuration();
	}

	public override bool OnIsComplete () {
		return false;
	}

	public override void OnBeginAction(float secondsRemaining) {
		SpriteRenderer spriteRenderer = m_towerSegmentToBeConstructed.gameObject.GetComponent<SpriteRenderer>();
		m_constructionImage = GetComponentInChildren<Image>();
		m_constructionImage.sprite = spriteRenderer.sprite;
		
		// Add a new empty one ready to build
		m_owningTower.AddTowerSegment(m_owningTower.m_emptyTowerSegmentPrefab);
	}

	public override void OnProgressAction(float secondsRemaining) {
		m_constructionImage.fillAmount = m_completion;
	}

	public override void OnCompleteAction () {
		// Swap the segment with a new one
		m_owningTower.SwapSegment(this, m_towerSegmentToBeConstructed);
	}

	public override bool ShowsWorkingArea() {
		return false;
	}
}
