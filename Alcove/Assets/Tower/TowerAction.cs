using UnityEngine;
using System.Collections.Generic;

public class TowerAction : MonoBehaviour {
	public float durationSecondsAtNominalWorkRate;
	private List<ITowerActionEvents> notifyList;
	private bool isRunning;
	private int workRate;
	private float completion;

	public void Awake() {
		isRunning = false;
		notifyList = new List<ITowerActionEvents>();
	}

	public void Notify(ITowerActionEvents notify) {
		notifyList.Add(notify);
	}

	public float Duration(int workRate) {
		return durationSecondsAtNominalWorkRate / (float)workRate;
	}

	public void StartAction(int workRate) {
		if (!isRunning) {
			Debug.Log("Action started at rate " + workRate);
			this.workRate = workRate;
			completion = 0.0f;
			float secondsRemaining = Duration(workRate);
			foreach (ITowerActionEvents notify in notifyList) {
				notify.TowerActionStarted(this);
				notify.TowerActionProgress(this, completion, secondsRemaining);
			}
			isRunning = true;
		}
	}

	public void Update () {
		if (isRunning) {
			completion = Mathf.Clamp01(completion + (float)workRate / durationSecondsAtNominalWorkRate * Time.deltaTime);
			float secondsRemaining = (1.0f - completion) * Duration(workRate);
			foreach (ITowerActionEvents notify in notifyList) {
				notify.TowerActionProgress(this, completion, secondsRemaining);
			}

			if (completion == 1.0f) {
				CompleteAction();
			}
		}
	}

	private void CompleteAction() {
		if (isRunning) {
			Debug.Log("Action completed");
			foreach (ITowerActionEvents notify in notifyList) {
				notify.TowerActionCompleted(this);
			}
			isRunning = false;
		}
		Destroy(gameObject);
	}
}
