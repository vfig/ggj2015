using UnityEngine;
using System.Collections;

public class Tribe : MonoBehaviour {
	public int count;
	private float busyTime;
	private float busyRemaining;

	// Return true if the tribe is busy
	public bool IsBusy {
		get {
			return (busyRemaining > 0);
		}
	}

	// Return remaining busy amount as float in range 0..1
	public float BusyFraction {
		get {
			if (busyTime > 0) {
				return (busyRemaining / busyTime);
			} else {
				return 0;
			}
		}
	}

	// Return remaining busy time as whole number of seconds
	public int BusySeconds {
		get {
			return (int)Mathf.Ceil(busyRemaining);
		}
	}

	// Make the tribe busy
	public void StartBusy(float time) {
		if (count == 0) {
			Debug.Log("Ignoring StartBusy for empty Tribe", this);
			return;
		}
		if (busyRemaining > 0) {
			Debug.Log("Ignoring StartBusy for already busy Trueb", this);
			return;
		}
		busyTime = time;
		busyRemaining = time;
	}

	void Start () {
	}

	void Update () {
		StepOccupied(Time.deltaTime);
	}

	private void StepOccupied(float time) {
		busyRemaining -= time;
		if (busyRemaining <= 0 || count == 0) {
			busyRemaining = 0;
			busyTime = 0;
		}
	}
}
