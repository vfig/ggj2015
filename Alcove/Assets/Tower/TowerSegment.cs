using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerSegment : MonoBehaviour
{
	/* Segment height */
	public const float HEIGHT = 2.6f;
	
	public bool hasAction;
	public float durationSecondsAtNominalWorkRate;

	/* Event listener list */
	private List<ITowerSegmentCallback> m_listenerList;
	
	/* True if the segment is under construction */
	private bool m_underConstruction;
	
	/* The rate at which the segment is constructed */
	private float m_constructionRate;
	
	/* The current completion of the segment */
	private float m_completion;
	
	/* Tribe that is currently in the segment */
	private Tribe m_currentTribe;

	/*  */
	public TowerSegment m_newTowerSegmentPrefab;

	/* Called on initialisation */
	public void Awake() {
		m_listenerList = new List<ITowerSegmentCallback>();
		m_currentTribe = null;
		m_underConstruction = false;
		m_completion = 0.0f;
		m_constructionRate = 0.0f;
	}
	
	/* Add a listener for the tower segment events. */
	public void AddListener(ITowerSegmentCallback listener) {
		m_listenerList.Add(listener);
	}

	/* Is the tower segment finished construction. */
	public bool IsComplete() {
		return (m_completion ==  1.0f);
	}
	
	/* Is the tower segment waiting for an action. */
	public bool IsIdle() {
		return m_underConstruction;
	}
	
	public void PerformAction(Tower parent, Tribe tribe) {
		m_listenerList.Add (parent);
		m_listenerList.Add(tribe);
		m_currentTribe = tribe;
	}
	
	
	/* Virtual Methods */
	
	// Add virtual methods for segment.
	
	public bool CanStartAction {
		get {
			return (hasAction && !m_underConstruction && m_completion == 0.0f);
		}
	}

	public float Duration(float constructionRate) {
		return durationSecondsAtNominalWorkRate / constructionRate;
	}

	public void StartAction(int constructionRate) {
		if (CanStartAction) {
			this.m_constructionRate = constructionRate;
			m_completion = 0.0f;
			float secondsRemaining = Duration(constructionRate);
			foreach (ITowerSegmentCallback listener in m_listenerList) {
				listener.TowerSegmentActionStarted(this);
				listener.TowerSegmentActionProgress(this, m_completion, secondsRemaining);
			}
			m_underConstruction = true;
		}
	}

	public void Update () {
		if (m_underConstruction) {
			m_completion = Mathf.Clamp01(m_completion + (float)m_constructionRate / durationSecondsAtNominalWorkRate * Time.deltaTime);
			float secondsRemaining = (1.0f - m_completion) * Duration(m_constructionRate);
			foreach (ITowerSegmentCallback listener in m_listenerList) {
				listener.TowerSegmentActionProgress(this, m_completion, secondsRemaining);
			}

			if (m_completion == 1.0f) {
				CompleteAction();
			}
		}
	}

	private void CompleteAction() {
		if (m_underConstruction) {
			foreach (ITowerSegmentCallback notify in m_listenerList) {
				notify.TowerSegmentActionCompleted(this);
			}
			m_underConstruction = false;
		}
		Destroy(gameObject);
	}

}
