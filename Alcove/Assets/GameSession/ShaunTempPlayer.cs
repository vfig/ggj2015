using UnityEngine;
using System.Collections;

public class ShaunTempPlayer : MonoBehaviour {

	public ShaunTempTower tower;

	public void StartTemp() {
		tower = new ShaunTempTower();
		tower.StartTemp();
	}

	public void Reset() {
		tower.Reset();
	}

	void Update() {
	}
}
