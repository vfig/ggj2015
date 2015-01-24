using UnityEngine;
using System.Collections;

public enum SegmentState
{
	Empty,
	Building,
	Complete,
	Damaged
}

public class SegmentScript : MonoBehaviour
{
	private SegmentState m_segmentState;
	private float m_completion;
	private float m_workRate;

	public GameObject m_emptySegmentPrefab;
	public GameObject m_buildingSegmentPrefab;
	public GameObject m_completeSegmentPrefab;

	private GameObject m_emptySegmentObject;
	private GameObject m_buildingSegmentObject;
	private GameObject m_completeSegmentObject;

	// Use this for initialization
	void Start ()
	{
		m_segmentState = SegmentState.Empty;
		m_completion = 0.0f;
		m_workRate = 0.0f;

		m_emptySegmentObject = Instantiate (m_emptySegmentPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		m_buildingSegmentObject = Instantiate (m_buildingSegmentPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		m_completeSegmentObject = Instantiate (m_completeSegmentPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;

		m_buildingSegmentObject.SetActive (false);
		m_completeSegmentObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_completion += m_workRate;
		if (m_completion > 1.0f)
		{
			m_completion = 1.0f;
			ChangeState(SegmentState.Complete);
		}
	}

	public SegmentState GetState ()
	{
		return m_segmentState;
	}

	public void StartBuilding(float workRate)
	{
		m_workRate = workRate;
		ChangeState(SegmentState.Building);
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

	public virtual void PerformAction()
	{
	}
}
