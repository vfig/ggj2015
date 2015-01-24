using UnityEngine;
using System.Collections;

public class EmptyTowerSegment : TowerSegment {

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public virtual bool IsActionable () {
		return true;
	}
	
	public virtual void PerformAction () {
		
		/* TODO: Select New Segment Prefab  */
		
		
	}
}
