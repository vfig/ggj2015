using UnityEngine;
using System.Collections;

public class RecruitmentArea : MonoBehaviour {
	struct CollectingState {
		public bool collecting;
		public float doorPosition;
		public float timeRemaining;
	}

	[HideInInspector]
	public static bool canUpdate = true;

	ArrayList units;
	float spawnMaxTime;
	float spawnCounter;
	float groundWidth;

	private CollectingState[] collectingState;

	public GameObject unitPrefab;

	public void Awake() {
		collectingState = new CollectingState[GameConstants.NUM_TRIBES_PER_PLAYER];
	}

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
		UpdateCollecting();
	}

	void UpdateSpawning() {
		spawnCounter += Time.deltaTime;
		if(spawnCounter >= spawnMaxTime) {
			spawnCounter = 0.0f;
			SpawnUnit();
		}
	}

	void UpdateCollecting() {
		int count = units.Count;
		for(int j=0; j<count; j++) {
			RecruitmentAreaUnit unit = units[j] as RecruitmentAreaUnit;
			int i = (int)unit.GetColour();
			if (collectingState[i].collecting) {
				unit.WalkToGoal(collectingState[i].doorPosition, collectingState[i].timeRemaining);
			}
		}
		for (int i = 0; i < collectingState.Length; ++i) {
			collectingState[i].timeRemaining -= Time.deltaTime;
			if (collectingState[i].timeRemaining <= 0) {
				collectingState[i].collecting = false;
			}
		}
	}

	public int DestroyAllUnits() {
		int count = units.Count;
		for(int i=0; i<count; i++) {
			RecruitmentAreaUnit unit = units[i] as RecruitmentAreaUnit;
			DestroyObject(unit.gameObject);
		}
		units.Clear();
		for (int i = 0; i < collectingState.Length; ++i) {
			collectingState[i].collecting = false;
		}
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
		collectingState[(int)colour].collecting = false;
		return numRemoved;
	}

	public void CollectUnitsOfColour(UnitColour color, float doorPosition, float time) {
		int index = (int)color;
		collectingState[index].collecting = true;
		collectingState[index].doorPosition = doorPosition;
		collectingState[index].timeRemaining = time;
	}

	private RecruitmentAreaUnit SpawnUnit() {
		GameObject unitGameObject = Instantiate(unitPrefab) as GameObject;
		unitGameObject.transform.position = Vector3.zero;
		RecruitmentAreaUnit unit = unitGameObject.GetComponent<RecruitmentAreaUnit>();
		unit.transform.parent = transform;
		unit.minX = 0.0f;
		unit.maxX = GameConstants.RECRUITMENT_AREA_GROUND_WIDTH;

		float startingXPosition;
		RecruitmentAreaUnit.UnitDirection direction;
		if(Random.Range (0, 2) == 1) {
			startingXPosition = 0;
			direction = RecruitmentAreaUnit.UnitDirection.Right;
		} else {
			startingXPosition = groundWidth;
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

	public static UnitColour GetRandomColour() {
		int result = Random.Range(0, 4);
		switch(result) {
			case 0: return UnitColour.Blue;
			case 1: return UnitColour.Green;
			case 2: return UnitColour.Red;
			case 3: return UnitColour.Yellow;
		}
		return UnitColour.Red;
	}
}
