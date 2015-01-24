using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TowerSegment : MonoBehaviour
{	
	/* Segment height */
	public const float HEIGHT = 2.6f;
	
	public bool hasAction;
	public float durationSecondsAtNominalWorkRate;

	/* Event listener list */
	private List<ITowerSegmentCallback> m_listenerList;
	
	/* True if the segment has an active action. */
	private bool m_actionActive;
	
	private bool m_autoNextAction;
	
	/* The tribe that is currently in the segment */
	private Tribe m_currentTribe;

	/* New tower segment to replace at the end of the action */
	protected TowerSegment m_newTowerSegmentPrefab;
	
	protected Tower m_owningTower;

	/* Called on initialisation */
	public void Awake() {
		m_listenerList = new List<ITowerSegmentCallback>();
		m_currentTribe = null;
		m_actionActive = false;
		m_autoNextAction = false;
	}
	
	/* Add a listener for the tower segment events */
	public void AddListener(ITowerSegmentCallback listener) {
		m_listenerList.Add(listener);
	}

	public void SetAutoNextAction(bool autoNextAction) {
		m_autoNextAction = autoNextAction;
	}
	
	public bool GetAutoNextAction() {
		return m_autoNextAction;
	}
	
	public Tower GetOwningTower() {
		return m_owningTower;
	}
	
	public Tribe GetOwningTribe() {
		return m_currentTribe;
	}

	/* Is the tower segment finished construction */
	public bool IsComplete() {
		return this.OnIsComplete();
	}
	
	public bool IsActionable() {
		return this.OnIsActionable();
	}
	
	public void SetNewTowerSegment(TowerSegment towerSegmentPrefab) {
		m_newTowerSegmentPrefab = towerSegmentPrefab;
	}
	
	public TowerSegment GetNewTowerSegmentPrefab() {
		return m_newTowerSegmentPrefab;
	}
	
	public void PerformAction(Tower owningTower, Tribe tribe) {
		m_listenerList.Add(owningTower);
		m_listenerList.Add(tribe);
		m_owningTower = owningTower;
		m_currentTribe = tribe;
		m_actionActive = true;
		this.OnBeginAction();
		foreach (ITowerSegmentCallback listener in m_listenerList) {
			listener.OnBeginAction(this);
		}
	}
	
	public void CompleteAction() {
		this.OnCompleteAction();
		foreach (ITowerSegmentCallback notify in m_listenerList) {
			notify.OnCompleteAction(this);
		}
	}

	public void Update () {
		if (m_actionActive) {
			this.OnProgressAction();
			foreach (ITowerSegmentCallback listener in m_listenerList) {
				listener.OnProgressAction(this, 0.0f, 0.0f);
			}
		}
	}
	
	/* Virtual Methods */
	
	public abstract bool OnIsActionable();
	
	public abstract bool OnIsComplete();
	
	public abstract void OnBeginAction();
	
	public abstract void OnProgressAction();
	
	public abstract void OnCompleteAction();
}
