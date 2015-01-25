using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TowerSegment : MonoBehaviour
{	
	/* Segment height */
	public const float HEIGHT = 2.0f;
	
	protected float m_workerCount;
	protected float m_completion;

	/* Event listener list */
	private List<ITowerSegmentCallback> m_listenerList;
	
	/* True if the segment has an active action. */
	private bool m_actionActive;
	
	/* The tribe that is currently in the segment */
	protected Tribe m_currentTribe;

	protected Tower m_owningTower;

	private TowerSegment m_towerSegmentPrefab;

	private GameObject m_tribeSign;
	private GameObject m_workingArea;

	public Tower OwningTower { get { return m_owningTower; } }

	public Tribe CurrentTribe { get { return m_currentTribe; } }

	public TowerSegment TowerSegmentPrefab { get { return m_towerSegmentPrefab; } set { m_towerSegmentPrefab = value; } }

	public GameObject TribeSign { get { return m_tribeSign; } set { m_tribeSign = value; } }

	/* Called on initialisation */
	public void Awake() {
		m_listenerList = new List<ITowerSegmentCallback>();
		m_currentTribe = null;
		m_actionActive = false;
		m_workerCount = 0.0f;
	}
	
	/* Add a listener for the tower segment events */
	public void AddListener(ITowerSegmentCallback listener) {
		m_listenerList.Add(listener);
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

	public void Update () {
		if (m_actionActive) {
			m_completion = Mathf.Clamp01(m_completion + (1.0f / Duration()) * Time.deltaTime);
			float secondsRemaining = (1.0f - m_completion) * Duration();
			
			this.OnProgressAction(secondsRemaining);
			foreach (ITowerSegmentCallback listener in m_listenerList) {
				listener.OnProgressAction(this, m_completion, secondsRemaining);
			}
			
			if (m_completion == 1.0f) {
				foreach (ITowerSegmentCallback notify in m_listenerList) {
					notify.OnCompleteAction(this);
				}
				m_listenerList = new List<ITowerSegmentCallback>();
				
				m_currentTribe.Count -= OnGetTribeCost();
				
				this.OnCompleteAction();
				
				if (m_tribeSign) {
					Destroy(m_tribeSign.gameObject);
				}
				if (m_workingArea) {
					Destroy(m_workingArea.gameObject);
				}
			}
		}
	}

	public void PerformAction(Tower owningTower, Tribe tribe) {
		AddListener(owningTower);
		AddListener(tribe);
		m_owningTower = owningTower;
		m_currentTribe = tribe;
		m_actionActive = true;
		m_completion = 0.0f;
		m_workerCount = tribe.Count;
		float secondsRemaining = Duration();
		Debug.Log(this + " duration: " + secondsRemaining + " at work rate: " + OnGetActionWorkRate());
		if (ShowsWorkingArea()) {
			m_workingArea = CreateWorkingArea(tribe, secondsRemaining);
		}
		m_tribeSign = CreateTribeSign(tribe);
		m_tribeSign.transform.position = transform.position + new Vector3(2.0f, 0.5f, 0.0f);
		this.OnBeginAction(secondsRemaining);
		foreach (ITowerSegmentCallback listener in m_listenerList) {
			listener.OnBeginAction(this);
		}
		this.OnProgressAction(secondsRemaining);
		foreach (ITowerSegmentCallback listener in m_listenerList) {
			listener.OnProgressAction(this, m_completion, secondsRemaining);
		}
	}
	
	public void CancelAction() {
		this.OnCancelAction ();
		foreach (ITowerSegmentCallback listener in m_listenerList) {
			listener.OnCancelAction(this);
		}
	}

	public GameObject CreateWorkingArea(Tribe tribe, float seconds) {
		GameObject obj = Instantiate(GameSession.GrabGameplayManagerReference().workingAreaPrefab) as GameObject;
		WorkingArea workingArea = obj.GetComponent<WorkingArea>();
		obj.transform.parent = transform;
		obj.transform.position = transform.position + Vector3.up * GameConstants.WORKING_AREA_GROUND_Y;
		workingArea.SetCountTimeAndColor(tribe.Count, seconds, tribe.m_unitColour);
		return obj;
	}
	
	private float Duration() {
		return OnGetActionDuration() / OnGetActionWorkRate();
	}
	
	public GameObject CreateTribeSign(Tribe tribe) {
		GameObject obj;
		if (tribe.m_unitColour == UnitColour.Blue) {
			obj = Instantiate(m_owningTower.m_tribeXSignPrefab) as GameObject;
		}
		else if (tribe.m_unitColour == UnitColour.Red) {
			obj = Instantiate(m_owningTower.m_tribeBSignPrefab) as GameObject;
		}
		else if (tribe.m_unitColour == UnitColour.Yellow) {
			obj = Instantiate(m_owningTower.m_tribeYSignPrefab) as GameObject;
		}
		else if (tribe.m_unitColour == UnitColour.Green) {
			obj = Instantiate(m_owningTower.m_tribeASignPrefab) as GameObject;
		}
		else {
			return null;
		}
		TribeSignUI ui = obj.GetComponent<TribeSignUI>();
		ui.tribe = tribe;
		return obj;
	}

	/* Virtual Methods */

	public virtual float OnGetActionDuration() {
		return 0.0f;
	}

	public virtual float OnGetActionWorkRate() {
		return m_workerCount * (m_owningTower.ActiveLaboratories + 1);
	}

	public virtual float OnGetConstructionDuration() {
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

	public virtual bool ShowsWorkingArea() {
		return true;
	}
	
	public virtual void OnBeginAction(float secondsRemaining){
	}
	
	public virtual void OnProgressAction(float secondsRemaining) {
	}
	
	public virtual void OnCompleteAction() {
	}
	
	public virtual void OnCancelAction() {	
	}
}
