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
	public TowerSegment m_cannonTowerSegmentPrefab;
	public TowerSegment m_baseTowerSegmentPrefab;
	
	void Awake () {
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
			EmptyTowerSegment emptySegment = oldSegment as EmptyTowerSegment;
			if (emptySegment) {
				newSegment.SetNewTowerSegment(emptySegment.GetTowerSegmentToBeConstructed());
			}
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
		m_selector.transform.localPosition = Vector3.up * (float)m_cursorPosition * TowerSegment.HEIGHT;
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
