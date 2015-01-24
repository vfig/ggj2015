using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	new public Camera camera;
	public int playerNumber;
	private ShaunTempTower tower;
	private Tribe[] tribes;
	private TowerScript m_towerScript;
	
	public void Awake() {
		camera = GetComponentInChildren<Camera>();
		tribes = GetComponentsInChildren<Tribe>();
		// FIXME: need to resolve/merge ShaunTempTower vs TowerScript
		tower = GetComponentInChildren<ShaunTempTower>();
		m_towerScript = GetComponentInChildren<TowerScript>();
	}

	public void Start() {
		Debug.Log("Player " + playerNumber + " Start");
		// tower = new ShaunTempTower();
		// tower.StartTemp();

		// FIXME: need to initialize tribe counts
	}

	public void Update() {
		// Move the cursor
		float cursorSpeed = GameInput.GetScrollAxis(playerNumber);
		if (cursorSpeed < 0) {
			m_towerScript.MoveUp(cursorSpeed * Time.deltaTime);
		} else if (cursorSpeed > 0) {
			m_towerScript.MoveDown(cursorSpeed * Time.deltaTime);
		}

		// Check for actions
		for (int i = 0; i < tribes.Length; ++i) {
			if (GameInput.GetTribeButtonDown(i, playerNumber)) {
				Debug.Log("Allocate tribe " + i + " to selected segment.");
				// FIXME - need to check for tribe busy
				// FIXME - need to make tribe busy for the same time as the action
				m_towerScript.PerformAction();
			}
		}
	}
}
