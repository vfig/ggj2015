using UnityEngine;
using System.Collections;

public class RecruitmentAreaUnit : MonoBehaviour {

	public enum UnitDirection {
		Left,
		Right
	}

	private UnitColour colour;

	public RecruitmentArea recruitmentArea;
	public UnitDirection direction;

	void Start() {
		direction = UnitDirection.Left;
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
	}

	void Update() {
		UpdateWalking();
	}

	void UpdateWalking() {
		switch(direction) {
		case UnitDirection.Left:
			SetDeltaX(-GameConstants.RECRUITMENT_UNIT_WALK_SPEED * Time.deltaTime);
			if(GetX() <= 0.0f) {
				SetX(0.0f);
				direction = UnitDirection.Right;
			}
			break;
		case UnitDirection.Right:
			SetDeltaX(GameConstants.RECRUITMENT_UNIT_WALK_SPEED * Time.deltaTime);
			if(GetX() >= GameConstants.RECRUITMENT_AREA_GROUND_WIDTH) {
				SetX(GameConstants.RECRUITMENT_AREA_GROUND_WIDTH);
				direction = UnitDirection.Left;
			}
			break;
		}
	}

	void SetDeltaX(float x) {
		Vector3 position = transform.localPosition;
		position.x += x;
		transform.localPosition = position;
	}

	void SetX(float x) {
		Vector3 position = transform.localPosition;
		position.x = x;
		transform.localPosition = position;
	}

	float GetX() {
		return transform.localPosition.x;
	}

	public void SetColour(UnitColour colour) {
		this.colour = colour;

		SpriteRenderer renderer = GetComponent<SpriteRenderer>();

		switch(colour) {
		case UnitColour.Blue:
			renderer.sprite = recruitmentArea.blueSprite;
			break;
		case UnitColour.Green:
			renderer.sprite = recruitmentArea.greenSprite;
			break;
		case UnitColour.Red:
			renderer.sprite = recruitmentArea.redSprite;
			break;
		case UnitColour.Yellow:
			renderer.sprite = recruitmentArea.yellowSprite;
			break;
		}
	}

	public UnitColour GetColour() {
		return colour;
	}
}
