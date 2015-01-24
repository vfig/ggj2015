using UnityEngine;
using System.Collections;

public class ShaunTempPlayer : MonoBehaviour {

	public ShaunTempTower tower;
	public Tribe[] tribes;

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

	public void Reset() {

		tower.Reset();

		int count = tribes.Length;
		for(int i=0; i<count; i++) {
			//tribes[i].Reset();
		}
	}

	void Update() {
		int count = tribes.Length;
		for(int i=0; i<count; i++) {
			//tribes[i].Update();
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
		string workingDescription = tribe.IsBusy ? ("work:" + tribe.BusyFraction) : "idle";
		return "{" + tribe.count + "u, " + workingDescription + "}";
	}

	public void DoTestTask() {
		int random = Random.Range(0, GameRulesManager.NUM_TRIBES_PER_PLAYER);
		float time = Random.Range(5, 10);
		Debug.Log("Doing test task on tribe " + random + " for " + time + " seconds.");
		tribes[random].StartBusy(time);
	}

}
