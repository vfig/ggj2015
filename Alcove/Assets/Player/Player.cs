using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	new public Camera camera;
	public int playerNumber;
	private Tribe[] tribes;
	public Tower tower;
	
	public void Awake() {
		camera = GetComponentInChildren<Camera>();
		tribes = GetComponentsInChildren<Tribe>();
		tower = GetComponentInChildren<Tower>();
	}

	public void Start() {
		Debug.Log("Player " + playerNumber + " Start");

		// Set initial tribe counts
		foreach (Tribe tribe in tribes) {
			tribe.count = GameRulesManager.TRIBE_STARTING_UNIT_COUNT;
		}
	}

	public void Update() {
		// Move the cursor
		float cursorSpeed = GameInput.GetScrollAxis(playerNumber);
		if (cursorSpeed < 0) {
			tower.MoveUp(cursorSpeed * Time.deltaTime);
		} else if (cursorSpeed > 0) {
			tower.MoveDown(cursorSpeed * Time.deltaTime);
		}

		// Check for actions
		for (int i = 0; i < tribes.Length; ++i) {
			if (GameInput.GetTribeButtonDown(i, playerNumber)) {
				tower.PerformAction(tribes[i]);
			}
		}
		
		// update camera
		Vector3 selectorPosition = tower.GetSelectorPosition();
		camera.transform.position = new Vector3(selectorPosition.x, selectorPosition.y, -10.0f);
	}
}
