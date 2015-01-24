using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour, ITowerSegmentCallback {
	private List<TowerSegment> segments;
	public int m_cursorPosition;

	public GameObject m_selector;

	public TowerSegment m_emptyTowerSegmentPrefab;
	public TowerSegment m_constructionTowerSegmentPrefab;
	public TowerSegment m_staticTowerSegmentPrefab;
	public TowerSegment m_baseTowerSegmentPrefab;
	
	void Start () {
		segments = new List<TowerSegment> ();
		m_cursorPosition = 0;
		AddTowerSegment(m_baseTowerSegmentPrefab);
		AddTowerSegment(m_emptyTowerSegmentPrefab);
	}
	
	private TowerSegment AddTowerSegment(TowerSegment towerSegmentPrefab) {
		TowerSegment newSegment = (TowerSegment)Instantiate(towerSegmentPrefab);
		newSegment.transform.parent = transform;
		newSegment.transform.localPosition = Vector3.up * (float)segments.Count * TowerSegment.HEIGHT;
		segments.Add(newSegment);
		return newSegment;
	}

	private TowerSegment SwapSegment(int index, TowerSegment prefab, bool autoNextAction) {
		TowerSegment oldSegment = segments[index];
		TowerSegment newSegment = (TowerSegment)Instantiate(prefab);
		newSegment.transform.parent = oldSegment.transform.parent;
		newSegment.transform.localPosition = oldSegment.transform.localPosition;
		segments[index] = newSegment;
		Debug.Log("Destroying old segment " + oldSegment + " from index " + index + ", replaced by " + newSegment);
		Destroy(oldSegment.gameObject);
		if (autoNextAction) {
			newSegment.PerformAction(oldSegment.GetOwningTower(), oldSegment.GetOwningTribe());
		}
		return newSegment;
	}

	public void PerformAction(Tribe tribe)
	{
		TowerSegment segment = segments[m_cursorPosition].GetComponent<TowerSegment>();

		if (!segment.IsActionable()) {
			/* TODO: Add some kind of notification that the segment cannot be actioned on */
			return;
		}
		
		segment.PerformAction(this, tribe);

		// if (!segment.CanStartAction) return;
		// if (tribe.IsBusy || tribe.count == 0) return;

		// FIXME - deal with non-empty segments! the stuff below assumes the only action is to build on an empty segment

		/*
		// Under-construction segment immediately replaces the empty segment
		TowerSegment constructionSegment = SwapSegment(m_cursorPosition, m_constructionSegmentPrefab);
		// Final plain segment to eventually replace the under-construction segment
		constructionSegment.m_newTowerSegmentPrefab = m_plainSegmentPrefab;
		// Add a new empty segment so we can begin building immediately
		AddSegment(m_emptySegmentPrefab);

		Debug.Log("Allocate tribe " + tribe + " to segment " + constructionSegment + ".");

		constructionSegment.StartAction(tribe.count);
		*/
	}

	public void OnBeginAction(TowerSegment segment) {
	}

	public void OnProgressAction(TowerSegment segment, float progress, float secondsRemaining) {
	}

	public void OnCompleteAction(TowerSegment segment) {
		int index = segments.IndexOf(segment);
		if (index >= 0 && segment.GetNewTowerSegmentPrefab() != null) {
			// Swap the segment with a new one
			SwapSegment(index, segment.GetNewTowerSegmentPrefab(), segment.GetAutoNextAction());
			
			if (segment.IsComplete()) {
				AddTowerSegment(m_emptyTowerSegmentPrefab);
			}
		}
	}

	public void MoveUp()
	{
		if (m_cursorPosition < (segments.Count - 1))
		{
			m_cursorPosition++;
		}
		m_selector.transform.localPosition = Vector3.up * (float)m_cursorPosition * TowerSegment.HEIGHT;
	}
	
	public void MoveDown()
	{
		if (m_cursorPosition > 0)
		{
			m_cursorPosition--;
		}
		m_selector.transform.position = transform.position + new Vector3 (0.0f, (float)(m_cursorPosition) * 2.6f, 0.0f);
	}

	public int GetCompletedSegmentCount()
	{
		int completedSegmentCount = 0;
		for(int i=0; i<segments.Count; i++)
		{
			if(segments[i].IsComplete()) {
				completedSegmentCount++;
			}
		}
		return completedSegmentCount;
	}
}
