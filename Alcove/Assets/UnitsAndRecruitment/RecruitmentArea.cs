﻿using UnityEngine;
using System.Collections;

public class RecruitmentArea : MonoBehaviour {

	[HideInInspector]
	public static bool canUpdate = true;

	ArrayList units;
	float spawnMaxTime;
	float spawnCounter;
	float groundWidth;

	public GameObject unitPrefab;

	void Start() {
		units = new ArrayList();
		spawnMaxTime = GameConstants.RECRUITMENT_AREA_DEFAULT_SPAWN_TIME;
		groundWidth = GameConstants.RECRUITMENT_AREA_GROUND_WIDTH;
		spawnCounter = spawnMaxTime;
	}

	public void Reset() {
		spawnCounter = spawnMaxTime;
	}

	void Update() {
		if(!canUpdate) {
			return;
		}
		UpdateSpawning();
	}

	void UpdateSpawning() {
		spawnCounter += Time.deltaTime;
		if(spawnCounter >= spawnMaxTime) {
			spawnCounter = 0.0f;
			SpawnUnit();
		}
	}

	public int DestroyAllUnits() {
		int count = units.Count;
		for(int i=0; i<count; i++) {
			RecruitmentAreaUnit unit = units[i] as RecruitmentAreaUnit;
			DestroyObject(unit.gameObject);
		}
		units.Clear();
		return count;
	}

	public int DestroyAllUnitsOfColour(UnitColour colour) {
		int numRemoved = 0;
		for(int i=units.Count-1; i>=0; i--) {
			RecruitmentAreaUnit unit = units[i] as RecruitmentAreaUnit;
			if(unit.GetColour() == colour) {
				DestroyObject(unit.gameObject);
				units.RemoveAt(i);
				numRemoved++;
			}
		}
		return numRemoved;
	}

	private RecruitmentAreaUnit SpawnUnit() {
		GameObject unitGameObject = Instantiate(unitPrefab) as GameObject;
		unitGameObject.transform.position = Vector3.zero;
		RecruitmentAreaUnit unit = unitGameObject.GetComponent<RecruitmentAreaUnit>();
		unit.transform.parent = transform;
		unit.recruitmentArea = this;

		float startingXPosition;
		if(Random.Range (0, 2) == 1) {
			startingXPosition = 0;
			// Direction should be is rightward
		} else {
			startingXPosition = groundWidth;
			// Direction should be leftward
		}

		unit.transform.position = Vector3.zero;
		unit.transform.localPosition = new Vector3(startingXPosition, 0.0f, 0.0f);
		float scale = 1.2f;
		unit.transform.localScale = new Vector3(scale, scale, scale);
		units.Add(unit);
		return unit;
	}
}
