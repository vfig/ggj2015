using UnityEngine;
using System.Collections;

public class Tribe : MonoBehaviour {
	public int count;
	private float busyTime;
	private float busyRemaining;

	public bool IsBusy {
		get {
			return (busyRemaining > 0);
		}
	}

	public float BusyFraction {
		get {
			if (busyTime > 0) {
				return (busyRemaining / busyTime);
			} else {
				return 0;
			}
		}
	}

	public int BusySeconds {
		get {
			return (int)Mathf.Ceil(busyRemaining);
		}
	}

	public void StartBusy(float time) {
		busyTime = time;
		busyRemaining = time;
	}

	void Start () {
	}

	void Update () {
		StepOccupied(Time.deltaTime);
	}

	private void StepOccupied(float time) {
		busyRemaining = Mathf.Max(0, busyRemaining - time);
		if (busyRemaining == 0) {
			busyTime = 0;
		}
	}
}
