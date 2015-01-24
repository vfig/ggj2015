using UnityEngine;
using System.Collections;

public class ConstructionTowerSegment : TowerSegment {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public virtual bool OnIsActionable () {
		return false;
	}
	
	public virtual void OnBeginAction (Tribe tribe) {
		// Start construction ...
	}
	
	public virtual void OnCompleteAction () {
	}
}
