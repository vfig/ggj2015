using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerScript : MonoBehaviour {
	
	private List<GameObject> m_towerSegmentList;
	private int m_numTowerSegments;

	public GameObject m_towerBase;

	public GameObject m_towerSegmentPrefab;
	
	// Use this for initialization
	void Start () {
		m_towerSegmentList = new List<GameObject> ();
		m_numTowerSegments = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public void AddTowerSegment() {

		GameObject newSegment = Instantiate (m_towerSegmentPrefab, gameObject.transform.position), Quaternion.identity) as GameObject;
		m_numTowerSegments++;

		m_towerSegmentList.Add (newSegment);
	}

	public void RemoveTowerSegment() {

	}
}
