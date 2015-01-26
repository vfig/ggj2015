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

	private float speed;
	private bool walkingToGoal;
	private float goalX;

	void Awake() {
		colour = GetRandomColour();
		direction = UnitDirection.Left;
		minX = 0;
		maxX = 0;
		walkingToGoal = false;
		speed = GameConstants.RECRUITMENT_UNIT_WALK_SPEED;
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
			SetDeltaX(-speed * Time.deltaTime);
			SetScaleXSign(-1);
			if(GetX() <= minX) {
				SetX(minX);
				direction = UnitDirection.Right;
			}
			break;
		case UnitDirection.Right:
			SetDeltaX(speed * Time.deltaTime);
			SetScaleXSign(1);
			if(GetX() >= maxX) {
				SetX(maxX);
				direction = UnitDirection.Left;
			}
			break;
		}

		if (walkingToGoal) {
			if ((direction == UnitDirection.Left && transform.position.x <= goalX)
				|| (direction == UnitDirection.Right && transform.position.x >= goalX)) {
				walkingToGoal = false;
				renderer.enabled = false;
				speed = 0;
			}
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

	public void WalkToGoal(float x, float time) {
		walkingToGoal = true;
		goalX = x;
		if (transform.position.x > x) {
			direction = UnitDirection.Left;
		} else {
			direction = UnitDirection.Right;
		}
		speed = Mathf.Max(GameConstants.RECRUITMENT_UNIT_RUN_SPEED,
			Mathf.Abs(transform.position.x - x) / time);
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
