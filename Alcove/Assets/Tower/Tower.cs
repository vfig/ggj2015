using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {
	
	private List<GameObject> m_towerSegmentList;
	public int m_numTowerSegments;
	public int m_cursorPosition;

	public GameObject m_towerBase;
	public GameObject m_selector;

	public GameObject m_towerSegmentPrefab;
	
	// Use this for initialization
	void Start () {
		m_towerSegmentList = new List<GameObject> ();
		m_numTowerSegments = 0;
		m_cursorPosition = 0;
		AddEmptyTowerSegment ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		TowerSegment segmentScript = m_towerSegmentList[m_numTowerSegments - 1].GetComponent<TowerSegment>();
		
		if (segmentScript.GetState() == SegmentState.Complete)
		{
			AddEmptyTowerSegment ();
		}
	}
	
	public void AddEmptyTowerSegment()
	{
		m_numTowerSegments++;
	
		GameObject newSegment = Instantiate (m_towerSegmentPrefab, m_towerBase.transform.position + new Vector3(0.0f, (float)(m_numTowerSegments) * 2.6f, 0.0f), Quaternion.identity) as GameObject;

		m_towerSegmentList.Add (newSegment);
	}

	public void PerformAction(Tribe tribe)
	{
		TowerSegment segment = m_towerSegmentList[m_cursorPosition - 1].GetComponent<TowerSegment>();

		if (tribe.IsBusy || tribe.count == 0) return;
		if (segment.GetState() != SegmentState.Empty) return;

		// FIXME - should replace empty segment here with new building segment, and put new empty segment at
		// the top of the tower.

		Debug.Log("Allocate tribe " + tribe + " to segment " + segment + ".");

		// FIXME - need to decide between predefined actions, each with their own duration.
		GameObject actionObject = new GameObject();
		TowerAction action = actionObject.AddComponent<TowerAction>();
		action.durationSecondsAtNominalWorkRate = 10.0f;
		action.Notify(tribe);
		action.Notify(segment);
		action.StartAction(tribe.count);
	}

	public void MoveUp(float delta)
	{
		if (m_cursorPosition < (m_numTowerSegments))
		{
			m_cursorPosition++;
		}
		m_selector.transform.position = m_towerBase.transform.position + new Vector3 (0.0f, (float)(m_cursorPosition) * 2.6f, 0.0f);
	}
	
	public void MoveDown(float delta)
	{
		if (m_cursorPosition > 0)
		{
			m_cursorPosition--;
		}
		m_selector.transform.position = m_towerBase.transform.position + new Vector3 (0.0f, (float)(m_cursorPosition) * 2.6f, 0.0f);
	}
}
