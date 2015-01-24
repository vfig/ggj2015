using UnityEngine;
using System.Collections;

public class RecruitmentArea : MonoBehaviour {

	[HideInInspector]
	public bool shouldUpdate = true;

	public Sprite greenSprite;
	public Sprite blueSprite;
	public Sprite redSprite;
	public Sprite yellowSprite;

	ArrayList units;
	float spawnMaxTime;
	float spawnCounter;
	float groundWidth;
	
	public GameObject unitPrefab;

	void Start() {
		units = new ArrayList();
		spawnMaxTime = GameConstants.RECRUITMENT_AREA_DEFAULT_SPAWN_TIME;
		groundWidth = GameConstants.RECRUITMENT_AREA_GROUND_WIDTH;
		spawnCounter = 0;
	}

	public void Reset() {
		spawnCounter = 0;
	}

	void Update() {
		if(!shouldUpdate) {
			return;
		}
		UpdateSpawning();
		UpdateUnits();
	}

	void UpdateSpawning() {
		spawnCounter += Time.deltaTime;
		//Debug.Log("spawnCounter: " + spawnCounter);
		if(spawnCounter >= spawnMaxTime) {
			spawnCounter = 0.0f;
			SpawnUnit();
		}
	}

	public void DestroyAllUnits() {
		for(int i=0; i<units.Count; i++) {
			RecruitmentAreaUnit unit = units[i] as RecruitmentAreaUnit;
			DestroyObject(unit.gameObject);
		}
		units.Clear();
	}

	public void DestroyAllUnitsOfColour(UnitColour colour) {
		int numRemoved = 0;
		for(int i=units.Count-1; i>=0; i--) {
			RecruitmentAreaUnit unit = units[i] as RecruitmentAreaUnit;
			if(unit.GetColour() == colour) {
				DestroyObject(unit.gameObject);
				units.RemoveAt(i);
			}
		}
		Debug.Log("Removed " + numRemoved + " units of colour '" + colour + "' from recruitment area.");
	}

	private RecruitmentAreaUnit SpawnUnit() {
		GameObject unitGameObject = Instantiate(unitPrefab) as GameObject;
		unitGameObject.transform.position = Vector3.zero;
		RecruitmentAreaUnit unit = unitGameObject.AddComponent<RecruitmentAreaUnit>();
		unit.transform.parent = transform;
		UnitColour colour = GetRandomColour();
		unit.recruitmentArea = this;
		unit.SetColour(colour);

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

	UnitColour GetRandomColour() {
		int result = Random.Range(0, 4);
		switch(result) {
			case 0: return UnitColour.Blue;
			case 1: return UnitColour.Green;
			case 2: return UnitColour.Red;
			case 3: return UnitColour.Yellow;
		}
		return UnitColour.Red;
	}

	void UpdateUnits() {
		/*for(int i=0; i<units.Count; i++) {
			RecruitmentAreaUnit unit = units[i];
		}*/
	}
}
