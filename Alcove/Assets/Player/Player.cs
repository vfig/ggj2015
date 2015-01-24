using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static bool canUpdate;

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
			tribe.count = GameConstants.TRIBE_STARTING_UNIT_COUNT;
		}
	}

	public void Update() {

		if(!canUpdate) {
			return;
		}

		GameInput.Update();

		// Move the cursor
		if (GameInput.GetScrollUpButtonDown(playerNumber)) {
			tower.MoveUp();
		} else if (GameInput.GetScrollDownButtonDown(playerNumber)) {
			tower.MoveDown();
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
