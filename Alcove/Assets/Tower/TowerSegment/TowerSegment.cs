using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TowerSegment : MonoBehaviour
{	
	/* Segment height */
	public const float HEIGHT = 2.0f;
	
	public bool hasAction;
	protected float m_workRate;
	protected float m_completion;

	/* Event listener list */
	private List<ITowerSegmentCallback> m_listenerList;
	
	/* True if the segment has an active action. */
	private bool m_actionActive;
	
	/* The tribe that is currently in the segment */
	protected Tribe m_currentTribe;

	protected Tower m_owningTower;

	private TowerSegment m_towerSegmentPrefab;

	/* Called on initialisation */
	public void Awake() {
		m_listenerList = new List<ITowerSegmentCallback>();
		m_currentTribe = null;
		m_actionActive = false;
	}
	
	/* Add a listener for the tower segment events */
	public void AddListener(ITowerSegmentCallback listener) {
		m_listenerList.Add(listener);
	}

	public Tower GetOwningTower() {
		return m_owningTower;
	}
	
	public Tribe GetOwningTribe() {
		return m_currentTribe;
	}
	
	public void SetTowerSegmentPrefab(TowerSegment towerSegmentPrefab) {
		m_towerSegmentPrefab = towerSegmentPrefab;
	}
	
	public TowerSegment GetTowerSegmentPrefab() {
		return m_towerSegmentPrefab;
	}

	/* Is the tower segment finished construction */
	public bool IsComplete() {
		return this.OnIsComplete();
	}

	/* Can an action be taken on this segment? */
	public bool IsActionable() {
		return this.OnIsActionable();
	}
	
	public void Reset() {
		this.m_completion = 0.0f;
		this.m_actionActive = false;
	}

	public void PerformAction(Tower owningTower, Tribe tribe) {
		AddListener(owningTower);
		AddListener(tribe);
		m_owningTower = owningTower;
		m_currentTribe = tribe;
		m_actionActive = true;
		m_completion = 0.0f;
		m_workRate = tribe.Count;
		float secondsRemaining = Duration(m_workRate);
		this.OnBeginAction();
		foreach (ITowerSegmentCallback listener in m_listenerList) {
			listener.OnBeginAction(this);
		}
		this.OnProgressAction(secondsRemaining);
		foreach (ITowerSegmentCallback listener in m_listenerList) {
			listener.OnProgressAction(this, m_completion, secondsRemaining);
		}
	}
	
	public float Duration(float workRate) {
		return NominalActionDurationSeconds() / workRate;
	}

	public void Update () {
		if (m_actionActive) {
			m_completion = Mathf.Clamp01(m_completion + (float)m_workRate / NominalActionDurationSeconds() * Time.deltaTime);
			float secondsRemaining = (1.0f - m_completion) * Duration(m_workRate);

			this.OnProgressAction(secondsRemaining);
			foreach (ITowerSegmentCallback listener in m_listenerList) {
				listener.OnProgressAction(this, m_completion, secondsRemaining);
			}

			if (m_completion == 1.0f) {
				foreach (ITowerSegmentCallback notify in m_listenerList) {
					notify.OnCompleteAction(this);
				}
				m_listenerList = new List<ITowerSegmentCallback>();
				OnCompleteAction();
			}
		}
	}
	
	/* Virtual Methods */

	public virtual float NominalActionDurationSeconds() {
		return 0.0f;
	}

	public virtual float NominalConstructionDurationSeconds() {
		return 0.0f;
	}
	
	public virtual int OnGetMinimumTribeSize() {
		return 1;
	}
	
	public virtual int OnGetTribeCost() {
		return 0;
	}
	
	public virtual bool OnIsActionable() {
		return false;
	}
	
	public virtual bool OnIsComplete() {
		return true;
	}
	
	public virtual void OnBeginAction() {}
	
	public virtual void OnProgressAction(float secondsRemaining) {}
	
	public virtual void OnCompleteAction() {}
}
