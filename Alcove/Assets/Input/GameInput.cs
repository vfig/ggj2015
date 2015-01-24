using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class GameInput {
	struct ScrollState {
		public bool tick;
		public int direction;
		public int count;
		public float lastTime;
		public int lastDirection;
	}

	private const float SCROLL_INITIAL_REPEAT_INTERVAL = 0.3f;
	private const float SCROLL_FINAL_REPEAT_INTERVAL = 0.1f;
	private const int SCROLL_INITIAL_COUNT = 1;

	private static int currentFrameCount;
	private static ScrollState[] scrollState;

	public static void ResetInput() {
		scrollState = null;
	}

	public static void Update() {
		if (currentFrameCount == Time.frameCount) return;
		currentFrameCount = Time.frameCount;

		if (scrollState == null) {
			scrollState = new ScrollState[GameRulesManager.PLAYER_COUNT];
		}

		for (int player = 0; player < GameRulesManager.PLAYER_COUNT; ++player) {
			float axis = GetScrollAxis(player);
			if (axis > -0.25 && axis < 0.25) {
				scrollState[player].tick = false;
				scrollState[player].direction = 0;
				scrollState[player].count = 0;
				scrollState[player].lastTime = 0;
				scrollState[player].lastDirection = 0;
			} else {
				int direction = (axis > 0 ? 1 : -1);
				scrollState[player].direction = direction;
				if (direction != scrollState[player].lastDirection) {
					scrollState[player].tick = true;
					scrollState[player].count = 1;
					scrollState[player].lastDirection = direction;
					scrollState[player].lastTime = Time.time;
				} else if (scrollState[player].count <= GameInput.SCROLL_INITIAL_COUNT
						&& (Time.time - scrollState[player].lastTime) >= GameInput.SCROLL_INITIAL_REPEAT_INTERVAL) {
					scrollState[player].tick = true;
					++scrollState[player].count;
					scrollState[player].lastTime = Time.time;
				} else if (scrollState[player].count > GameInput.SCROLL_INITIAL_COUNT
						&& (Time.time - scrollState[player].lastTime) >= GameInput.SCROLL_FINAL_REPEAT_INTERVAL) {
					scrollState[player].tick = true;
					++scrollState[player].count;
					scrollState[player].lastTime = Time.time;
				} else {
					scrollState[player].tick = false;
				}
			}
		}
	}

	public static bool GetScrollUpButtonDown(int player) {
		GameInput.Update();
		return (GameInput.scrollState[player].tick && GameInput.scrollState[player].direction == -1);
	}

	public static bool GetScrollDownButtonDown(int player) {
		GameInput.Update();
		return (GameInput.scrollState[player].tick && GameInput.scrollState[player].direction == 1);
	}

	private static float GetScrollAxis(int player) {
		if (XCI.GetNumPluggedCtrlrs() > player) {
			return -XCI.GetAxis(XboxAxis.LeftStickY, player + 1);
		} else if (player == 0) {
			bool up = Input.GetKey("w");
			bool down = Input.GetKey("s");
			if (up && !down) {
				return -1;
			} else if (!up && down) {
				return 1;
			} else {
				return 0;
			}
		} else if (player == 1) {
			bool up = Input.GetKey("up");
			bool down = Input.GetKey("down");
			if (up && !down) {
				return -1;
			} else if (!up && down) {
				return 1;
			} else {
				return 0;
			}
		} else {
			return 0;
		}
	}

	private static XboxButton TribeButton(int tribe) {
		switch(tribe) {
		case 0: return XboxButton.A;
		case 1: return XboxButton.B;
		case 2: return XboxButton.X;
		default: return XboxButton.Y;
		}
	}

	private static string TribePlayerOneKey(int tribe) {
		switch(tribe) {
		case 0: return "1";
		case 1: return "2";
		case 2: return "3";
		default: return "4";
		}
	}

	private static string TribePlayerTwoKey(int tribe) {
		switch(tribe) {
		case 0: return "9";
		case 1: return "0";
		case 2: return "-";
		default: return "=";
		}
	}

	public static bool GetTribeButton(int tribe, int player) {
		if (XCI.GetNumPluggedCtrlrs() > player) {
			return XCI.GetButton(TribeButton(tribe), player + 1);
		} else if (player == 0) {
			return Input.GetKey(TribePlayerOneKey(tribe));
		} else if (player == 1) {
			return Input.GetKey(TribePlayerTwoKey(tribe));
		} else {
			return false;
		}
	}

	public static bool GetTribeButtonDown(int tribe, int player) {
		if (XCI.GetNumPluggedCtrlrs() > player) {
			return XCI.GetButtonDown(TribeButton(tribe), player + 1);
		} else if (player == 0) {
			return Input.GetKeyDown(TribePlayerOneKey(tribe));
		} else if (player == 1) {
			return Input.GetKeyDown(TribePlayerTwoKey(tribe));
		} else {
			return false;
		}
	}

	public static bool GetTribeButtonUp(int tribe, int player) {
		if (XCI.GetNumPluggedCtrlrs() > player) {
			return XCI.GetButtonUp(TribeButton(tribe), player + 1);
		} else if (player == 0) {
			return Input.GetKeyUp(TribePlayerOneKey(tribe));
		} else if (player == 1) {
			return Input.GetKeyUp(TribePlayerTwoKey(tribe));
		} else {
			return false;
		}
	}
}
