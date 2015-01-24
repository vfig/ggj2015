﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour, ITowerActionEvents {
	private List<TowerSegment> segments;
	public int m_cursorPosition;

	public GameObject m_selector;

	public TowerSegment m_emptySegmentPrefab;
	public TowerSegment m_constructionSegmentPrefab;
	public TowerSegment m_plainSegmentPrefab;
	public TowerSegment m_baseSegmentPrefab;
	
	void Start () {
		segments = new List<TowerSegment> ();
		m_cursorPosition = 0;
		AddSegment(m_baseSegmentPrefab);
		AddSegment(m_emptySegmentPrefab);
	}
	
	private TowerSegment AddSegment(TowerSegment prefab)
	{
		TowerSegment segment = (TowerSegment)Instantiate(prefab);
		segment.transform.parent = transform;
		segment.transform.localPosition = Vector3.up * (float)segments.Count * TowerSegment.HEIGHT;
		Debug.Log("Adding new segment " + segment + " at index " + segments.Count);
		segments.Add(segment);
		return segment;
	}

	private TowerSegment SwapSegment(int index, TowerSegment prefab) {
		TowerSegment oldSegment = segments[index];
		TowerSegment newSegment = (TowerSegment)Instantiate(prefab);
		newSegment.transform.parent = oldSegment.transform.parent;
		newSegment.transform.localPosition = oldSegment.transform.localPosition;
		segments[index] = newSegment;
		Debug.Log("Destroying old segment " + oldSegment + " from index " + index + ", replaced by " + newSegment);
		Destroy(oldSegment.gameObject);
		return newSegment;
	}

	public void PerformAction(Tribe tribe)
	{
		TowerSegment segment = segments[m_cursorPosition].GetComponent<TowerSegment>();

		if (!segment.CanStartAction) return;
		if (tribe.IsBusy || tribe.count == 0) return;

		// FIXME - deal with non-empty segments! the stuff below assumes the only action is to build on an empty segment

		// Under-construction segment immediately replaces the empty segment
		TowerSegment constructionSegment = SwapSegment(m_cursorPosition, m_constructionSegmentPrefab);
		// Final plain segment to eventually replace the under-construction segment
		constructionSegment.constructingTowerSegmentPrefab = m_plainSegmentPrefab;
		// Add a new empty segment so we can begin building immediately
		AddSegment(m_emptySegmentPrefab);

		// Start the construction
		constructionSegment.Notify(tribe);
		constructionSegment.Notify(this);
		Debug.Log("Allocate tribe " + tribe + " to segment " + constructionSegment + ".");
		constructionSegment.StartAction(tribe.count);
	}

	public void TowerActionStarted(TowerSegment segment) {
	}

	public void TowerActionProgress(TowerSegment segment, float progress, float secondsRemaining) {
	}

	public void TowerActionCompleted(TowerSegment segment) {
		int index = segments.IndexOf(segment);
		if (index >= 0 && segment.constructingTowerSegmentPrefab != null) {
			// Swap the segment with a new one
			SwapSegment(index, segment.constructingTowerSegmentPrefab);
		}
	}

	public void MoveUp(float delta)
	{
		if (m_cursorPosition < (segments.Count - 1))
		{
			m_cursorPosition++;
		}
		m_selector.transform.localPosition = Vector3.up * (float)m_cursorPosition * TowerSegment.HEIGHT;
	}
	
	public void MoveDown(float delta)
	{
		if (m_cursorPosition > 0)
		{
			m_cursorPosition--;
		}
		m_selector.transform.position = transform.position + new Vector3 (0.0f, (float)(m_cursorPosition) * 2.6f, 0.0f);
	}
	
	public Vector3 GetSelectorPosition()
	{
		return m_selector.transform.position;
	}
}