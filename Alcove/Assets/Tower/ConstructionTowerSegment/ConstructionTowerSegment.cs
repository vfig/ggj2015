using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConstructionTowerSegment : TowerSegment {
	private TowerSegment m_towerSegmentToBeConstructed;
	private Image m_constructionImage;

	[HideInInspector]
	public AudioSource buildingLoop;
	
	public AudioClip buildingLoopClip1;
	public AudioClip buildingLoopClip2;
	public AudioClip buildingCompleteClip;
	public AudioClip startBuildingClip;

	public void SetTowerSegmentToBeConstructed(TowerSegment prefab) {
		m_towerSegmentToBeConstructed = prefab;
	}

	public override float OnGetActionDuration() {
		return m_towerSegmentToBeConstructed.OnGetConstructionDuration();
	}
	
	public override int OnGetTribeCost() {
		return GameConstants.CONSTRUCTION_TOWER_SEGMENT_TRIBE_COST;
	}

	public override bool OnIsComplete () {
		return false;
	}

	public override void OnBeginAction(float secondsRemaining) {

		buildingLoop = GetComponent<AudioSource>();
		buildingLoop.clip = Random.Range(0, 2) == 1 ? buildingLoopClip1 : buildingLoopClip2;
		buildingLoop.Play();

		AudioSource.PlayClipAtPoint(startBuildingClip, Vector3.zero);

		SpriteRenderer spriteRenderer = m_towerSegmentToBeConstructed.gameObject.GetComponent<SpriteRenderer>();
		m_constructionImage = GetComponentInChildren<Image>();
		m_constructionImage.sprite = spriteRenderer.sprite;
		
		// Add a new empty one ready to build
		if (!m_towerSegmentToBeConstructed.IsFinalSegment()) {
			m_owningTower.AddTowerSegment(m_owningTower.m_emptyTowerSegmentPrefab);
		}
	}

	public override void OnProgressAction(float secondsRemaining) {
		m_constructionImage.fillAmount = m_completion;
	}

	public override void OnCompleteAction () {
		// Swap the segment with a new one
		buildingLoop.Stop();
		AudioSource.PlayClipAtPoint(buildingCompleteClip, Vector3.zero);
		m_owningTower.SwapSegment(this, m_towerSegmentToBeConstructed);
	}

	public override bool ShowsWorkingArea() {
		return false;
	}

	public override bool CanBeStolen() {
		return false;
	}
}
