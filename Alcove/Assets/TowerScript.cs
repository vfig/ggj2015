using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerScript : MonoBehaviour {
	
	private List<GameObject> m_segmentList;
	
	public GameObject m_gameObjectPrefab;
	
	// Use this for initialization
	void Start () {
		m_segmentList = new List<GameObject> ();
		AddTowerSegment ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void AddTowerSegment() {

		GameObject newSegment = Instantiate (m_gameObjectPrefab, Vector3.zero, Quaternion.identity) as GameObject;

		m_segmentList.Add (newSegment);
	}

	void RemoveTowerSegment() {

	}
}
