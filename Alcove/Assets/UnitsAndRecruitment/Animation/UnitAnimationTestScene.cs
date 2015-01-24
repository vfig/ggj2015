using UnityEngine;
using System.Collections;

public class UnitAnimationTestScene : MonoBehaviour {

	public RecruitmentAreaUnit unit;

	void Start() {
		Vector3 scale = unit.transform.localScale;
		scale.x += 2.0f;
		scale.y += 2.0f;
		scale.z += 2.0f;
		unit.transform.localScale = scale;
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {

			int random = Random.Range(0, 4);
			switch(random) {
			case 0:
				unit.animator.SelectChannel("red");
				break;
			case 1:
				unit.animator.SelectChannel("green");
				break;
			case 2:
				unit.animator.SelectChannel("blue");
				break;
			case 3:
				unit.animator.SelectChannel("yellow");
				break;
			}
		}
	}
}
