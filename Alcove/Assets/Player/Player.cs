﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private GameplayManager m_owningGameplayManager;

	public static bool canUpdate = true;

	new public Camera camera;
	public int playerNumber;
	private Tribe[] tribes;
	public Tower tower;

	public void Awake() {
		camera = GetComponentInChildren<Camera>();
		tribes = GetComponentsInChildren<Tribe>();
		tower = GetComponentInChildren<Tower>();
		tower.SetOwningPlayer(this);
	}

	public void Start() {
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
		if (GameInput.GetScrollLeftButtonDown(playerNumber)) {
			tower.MoveLeft();
		} else if (GameInput.GetScrollRightButtonDown(playerNumber)) {
			tower.MoveRight();
		}

		// Check for actions
		for (int i = 0; i < tribes.Length; ++i) {
			if (GameInput.GetTribeButtonDown(i, playerNumber)) {
				tower.PerformAction(tribes[i]);
			}
		}
	}
	
	public void SetOwningGameplayManager(GameplayManager owningGameplayManager) {
		m_owningGameplayManager = owningGameplayManager;
	}
	
	public void DestroyOpponentsSegment(int segmentIndex) {
		m_owningGameplayManager.DestroyOpponentsSegment(playerNumber, segmentIndex);
	}
	
	public void DestroyPlayersSegment(int segmentIndex) {
		tower.DestroyPlayersSegment(segmentIndex);
	}
}
