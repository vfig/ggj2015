using UnityEngine;
using System.Collections;

public enum SegmentState
{
	Empty,
	Building,
	Complete,
	Damaged
}

public class TowerSegment : MonoBehaviour, ITowerActionEvents
{
	private SegmentState m_segmentState;

	public GameObject m_emptySegmentPrefab;
	public GameObject m_buildingSegmentPrefab;
	public GameObject m_completeSegmentPrefab;

	private GameObject m_emptySegmentObject;
	private GameObject m_buildingSegmentObject;
	private GameObject m_completeSegmentObject;

	void Start ()
	{
		m_segmentState = SegmentState.Empty;

		m_emptySegmentObject = Instantiate (m_emptySegmentPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		m_buildingSegmentObject = Instantiate (m_buildingSegmentPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		m_completeSegmentObject = Instantiate (m_completeSegmentPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;

		m_buildingSegmentObject.SetActive (false);
		m_completeSegmentObject.SetActive (false);
	}
	
	public SegmentState GetState ()
	{
		return m_segmentState;
	}

	public void TowerActionStarted(TowerAction action) {
		ChangeState(SegmentState.Building);
	}

	public void TowerActionProgress(TowerAction action, float progress, float secondsRemaining) {
		// FIXME - update the sprite to show building progress
	}

	public void TowerActionCompleted(TowerAction action) {
		ChangeState(SegmentState.Complete);
	}

	public void ChangeState (SegmentState state)
	{
		m_segmentState = state;
	
		m_emptySegmentObject.SetActive (false);
		m_buildingSegmentObject.SetActive (false);
		m_completeSegmentObject.SetActive (false);

		if (m_segmentState == SegmentState.Empty) {
			m_emptySegmentObject.SetActive (true);
		}
		if (m_segmentState == SegmentState.Building) {
			m_buildingSegmentObject.SetActive (true);
		}
		if (m_segmentState == SegmentState.Complete) {
			m_completeSegmentObject.SetActive (true);
		}
	}
}
