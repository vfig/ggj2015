using UnityEngine;
using System.Collections;

public class StaticTowerSegment : TowerSegment {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public virtual bool OnIsActionable () {
		return true;
	}
	
	public virtual void OnBeginAction (Tribe tribe) {
	}
	
	public virtual void OnCompleteAction () {
	}
}
