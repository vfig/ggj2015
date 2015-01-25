using UnityEngine;
using System.Collections;

public class WorkingArea : MonoBehaviour {

	[HideInInspector]
	public static bool canUpdate = true;

	ArrayList units;
	float spawnMaxTime;
	float despawnMaxTime;
	int maxUnitCount;
	float spawnCounter;
	float groundWidth;
	bool spawning = false;
	bool despawning = false;
	UnitColour unitColor;

	public GameObject unitPrefab;

	public void SetCountTimeAndColor(int unitCount, float time, UnitColour color) {
		spawnMaxTime = time * 0.1f;
		despawnMaxTime = time * 0.9f;
		unitColor = color;
		maxUnitCount = unitCount;
	}

	public void Start() {
		units = new ArrayList();
		groundWidth = GameConstants.WORKING_AREA_GROUND_WIDTH;
		spawnCounter = 0;
		spawning = true;
		despawning = false;
	}

	public void Reset() {
		spawnCounter = 0;
	}

	public void Update() {
		if(!canUpdate) {
			return;
		}
		UpdateSpawning();
	}

	void UpdateSpawning() {
		if (maxUnitCount == 0) return;
		float spawnInterval = spawnMaxTime / (float)maxUnitCount;
		float despawnInterval = despawnMaxTime / (float)maxUnitCount;
		spawnCounter += Time.deltaTime;
		if (spawning) {
			if(spawnCounter >= spawnInterval) {
				spawnCounter = 0.0f;
				SpawnUnit();
			}
			if (units.Count == maxUnitCount) {
				spawning = false;
				despawning = true;
			}
		} else if (despawning) {
			if(spawnCounter >= despawnInterval) {
				spawnCounter = 0.0f;
				DespawnUnit();
			}
			if (units.Count == 0) {
				canUpdate = false;
				despawning = false;
			}
		}
	}

	private RecruitmentAreaUnit SpawnUnit() {
		GameObject unitGameObject = Instantiate(unitPrefab) as GameObject;
		unitGameObject.transform.position = Vector3.zero;
		RecruitmentAreaUnit unit = unitGameObject.GetComponent<RecruitmentAreaUnit>();
		unit.transform.parent = transform;
		unit.SetColour(unitColor);
		unit.minX = -GameConstants.WORKING_AREA_GROUND_WIDTH / 2.0f;
		unit.maxX = GameConstants.WORKING_AREA_GROUND_WIDTH / 2.0f;

		float startingXPosition = 0.0f;
		RecruitmentAreaUnit.UnitDirection direction;
		if(Random.Range (0, 2) == 1) {
			startingXPosition += 0.1f;
			direction = RecruitmentAreaUnit.UnitDirection.Right;
		} else {
			startingXPosition -= 0.1f;
			direction = RecruitmentAreaUnit.UnitDirection.Left;
		}
		unit.direction = direction;

		unit.transform.position = Vector3.zero;
		unit.transform.localPosition = new Vector3(startingXPosition, 0.0f, 0.0f);
		float scale = 1.2f;
		unit.transform.localScale = new Vector3(scale, scale, scale);
		units.Add(unit);
		return unit;
	}

	private void DespawnUnit() {
		int index = Random.Range(0, units.Count);
		RecruitmentAreaUnit unit = (RecruitmentAreaUnit)units[index];
		units.RemoveAt(index);
		Destroy(unit.gameObject);
	}
}
