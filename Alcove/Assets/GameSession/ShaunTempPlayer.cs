using UnityEngine;
using System.Collections;

public class ShaunTempPlayer : MonoBehaviour {

	public ShaunTempTower tower;

	void Start() {
		tower = new ShaunTempTower();
	}

	public void Reset() {
		tower.Reset();
	}

	

	void Update() {
	}
}
