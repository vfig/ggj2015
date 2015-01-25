using UnityEngine;
using System.Collections;

public class RecruitmentAreaUnit : MonoBehaviour {

	public enum UnitDirection {
		Left,
		Right
	}

	[HideInInspector]
	private UnitColour colour;
	[HideInInspector]
	public UnitDirection direction;
	[HideInInspector]
	public SpriteAnimator animator;

	public AnimationChannel blueChannel;
	public AnimationChannel greenChannel;
	public AnimationChannel redChannel;
	public AnimationChannel yellowChannel;

	public float minX;
	public float maxX;

	void Awake() {
		colour = GetRandomColour();
		direction = UnitDirection.Left;
		minX = 0;
		maxX = 0;
	}

	void Start() {
		animator = GetComponent<SpriteAnimator>();
		animator.animationSpeed = GameConstants.RECRUITMENT_UNIT_ANIMATION_SPEED;
		animator.AddChannel(redChannel, "red");
		animator.AddChannel(blueChannel, "blue");
		animator.AddChannel(greenChannel, "green");
		animator.AddChannel(yellowChannel, "yellow");

		ApplyColour();
	}

	void Update() {
		UpdateWalking();
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


	void UpdateWalking() {
		switch(direction) {
		case UnitDirection.Left:
			SetDeltaX(-GameConstants.RECRUITMENT_UNIT_WALK_SPEED * Time.deltaTime);
			SetScaleXSign(-1);
			if(GetX() <= minX) {
				SetX(minX);
				direction = UnitDirection.Right;
			}
			break;
		case UnitDirection.Right:
			SetDeltaX(GameConstants.RECRUITMENT_UNIT_WALK_SPEED * Time.deltaTime);
			SetScaleXSign(1);
			if(GetX() >= maxX) {
				SetX(maxX);
				direction = UnitDirection.Left;
			}
			break;
		}
	}

	void SetScaleXSign(int sign) {
		Vector3 scale = transform.localScale;
		scale.x = sign * Mathf.Abs(scale.x);
		transform.localScale = scale;
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
	}

	public void ApplyColour() {
		switch(this.colour) {
		case UnitColour.Blue:
			animator.SelectChannel("blue");
			break;
		case UnitColour.Green:
			animator.SelectChannel("green");
			break;
		case UnitColour.Red:
			animator.SelectChannel("red");
			break;
		case UnitColour.Yellow:
			animator.SelectChannel("yellow");
			break;
		}
	}

	public UnitColour GetColour() {
		return colour;
	}
}
