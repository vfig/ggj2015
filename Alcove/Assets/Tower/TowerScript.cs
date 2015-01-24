using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerScript : MonoBehaviour {
	
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
		SegmentScript segmentScript = m_towerSegmentList[m_numTowerSegments - 1].GetComponent<SegmentScript>();
		
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
	
	public void PerformAction()
	{
		SegmentScript segmentScript = m_towerSegmentList[m_cursorPosition - 1].GetComponent<SegmentScript>();
	
		switch (segmentScript.GetState())
		{
			case SegmentState.Empty:
				segmentScript.StartBuilding(0.01f);
				break;
				
			case SegmentState.Complete:
				segmentScript.PerformAction();
				break;
				
			default:
				break;
		}
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
