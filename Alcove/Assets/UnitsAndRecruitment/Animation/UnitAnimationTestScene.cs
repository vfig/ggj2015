using UnityEngine;
using System.Collections;

public class UnitAnimationTestScene : MonoBehaviour {

	public RecruitmentArea recruitmentArea;

	void Start() {
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			int unitCount = recruitmentArea.DestroyAllUnits();
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)) {
			int unitCount = recruitmentArea.DestroyAllUnitsOfColour(UnitColour.Green);
		}
	}
}
