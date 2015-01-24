using UnityEngine;
using System.Collections;

public class ShaunTempPlayer : MonoBehaviour {

	public ShaunTempTower tower;
	public Tribe[] tribes;
	public int nominatedTribeIndex;

	public void StartTemp() {
		tower = new ShaunTempTower();
		tower.StartTemp();

		CreateTribes();
	}

	void CreateTribes() {
		tribes = new Tribe[GameRulesManager.NUM_TRIBES_PER_PLAYER];
		int count = tribes.Length;
		for(int i=0; i<count; i++) {
			Tribe tribe = new Tribe();
			tribe.count = Random.Range(5, 10);
			Debug.Log("Creating random tribe count: " + tribe.count + " units.");
			tribes[i] = tribe;
		}
	}

	public void NominateTribeForActionSelection(int index) {
		nominatedTribeIndex = index;
	}

	public bool IsTribeNominated() {
		return nominatedTribeIndex >= 0;
	}

	public void PerformActionWithNominatedTribe(ActionType actionType) {
		Tribe tribe = tribes[nominatedTribeIndex];
		if(tribe.IsBusy) {
			return;
		}
		tribes[nominatedTribeIndex].StartBusy(5);
		nominatedTribeIndex = -1;
	}

	public void Reset() {

		nominatedTribeIndex = -1;
		tower.Reset();

		int count = tribes.Length;
		for(int i=0; i<count; i++) {
			tribes[i].Reset();
		}
	}

	void Update() {
	}

	public bool IsTribeAvailable(int index) {
		return !tribes[index].IsBusy;
	}

	public void UpdateTemp() {
		int count = tribes.Length;
		for(int i=0; i<count; i++) {
			tribes[i].StepOccupied(Time.deltaTime);
		}
	}

	public string GetTribesSummary() {
		string[] unitCounts = new string[tribes.Length];
		for(int i=0; i<tribes.Length; i++) {
			unitCounts[i] = GetSingleTribeSummary(tribes[i]);
		}
		return string.Join(",  ", unitCounts);
	}

	string GetSingleTribeSummary(Tribe tribe) {
		float busyFraction = (int)(tribe.BusyFraction * 100.0f) / 100.0f;
		string workingDescription = tribe.IsBusy ? ("work:" + busyFraction) : "idle";
		return "{" + tribe.count + "u, " + workingDescription + "}";
	}

	public void DoTestTask() {
		int random = Random.Range(0, GameRulesManager.NUM_TRIBES_PER_PLAYER);
		float time = Random.Range(5, 10);
		Debug.Log("Doing test task on tribe " + random + " for " + time + " seconds.");
		tribes[random].StartBusy(time);
	}

}
