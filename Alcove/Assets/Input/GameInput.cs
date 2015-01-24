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
	private static ScrollState[] vertScrollState;
	private static ScrollState[] horzScrollState;

	public static void ResetInput() {
		vertScrollState = null;
		horzScrollState = null;
	}

	public static void Update() {
		if (currentFrameCount == Time.frameCount) return;
		currentFrameCount = Time.frameCount;

		if (vertScrollState == null) {
			vertScrollState = new ScrollState[GameConstants.PLAYER_COUNT];
		}
		if (horzScrollState == null) {
			horzScrollState = new ScrollState[GameConstants.PLAYER_COUNT];
		}

		for (int player = 0; player < GameConstants.PLAYER_COUNT; ++player) {
			float vertAxis = GetVertScrollAxis(player);
			if (vertAxis > -0.25 && vertAxis < 0.25) {
				vertScrollState[player].tick = false;
				vertScrollState[player].direction = 0;
				vertScrollState[player].count = 0;
				vertScrollState[player].lastTime = 0;
				vertScrollState[player].lastDirection = 0;
			} else {
				int direction = (vertAxis > 0 ? 1 : -1);
				vertScrollState[player].direction = direction;
				if (direction != vertScrollState[player].lastDirection) {
					vertScrollState[player].tick = true;
					vertScrollState[player].count = 1;
					vertScrollState[player].lastDirection = direction;
					vertScrollState[player].lastTime = Time.time;
				} else if (vertScrollState[player].count <= GameInput.SCROLL_INITIAL_COUNT
						&& (Time.time - vertScrollState[player].lastTime) >= GameInput.SCROLL_INITIAL_REPEAT_INTERVAL) {
					vertScrollState[player].tick = true;
					++vertScrollState[player].count;
					vertScrollState[player].lastTime = Time.time;
				} else if (vertScrollState[player].count > GameInput.SCROLL_INITIAL_COUNT
						&& (Time.time - vertScrollState[player].lastTime) >= GameInput.SCROLL_FINAL_REPEAT_INTERVAL) {
					vertScrollState[player].tick = true;
					++vertScrollState[player].count;
					vertScrollState[player].lastTime = Time.time;
				} else {
					vertScrollState[player].tick = false;
				}
			}

			float horzAxis = GetHorzScrollAxis(player);
			if (horzAxis > -0.25 && horzAxis < 0.25) {
				horzScrollState[player].tick = false;
				horzScrollState[player].direction = 0;
				horzScrollState[player].count = 0;
				horzScrollState[player].lastTime = 0;
				horzScrollState[player].lastDirection = 0;
			} else {
				int direction = (horzAxis > 0 ? 1 : -1);
				horzScrollState[player].direction = direction;
				if (direction != horzScrollState[player].lastDirection) {
					horzScrollState[player].tick = true;
					horzScrollState[player].count = 1;
					horzScrollState[player].lastDirection = direction;
					horzScrollState[player].lastTime = Time.time;
				} else if (horzScrollState[player].count <= GameInput.SCROLL_INITIAL_COUNT
						&& (Time.time - horzScrollState[player].lastTime) >= GameInput.SCROLL_INITIAL_REPEAT_INTERVAL) {
					horzScrollState[player].tick = true;
					++horzScrollState[player].count;
					horzScrollState[player].lastTime = Time.time;
				} else if (horzScrollState[player].count > GameInput.SCROLL_INITIAL_COUNT
						&& (Time.time - horzScrollState[player].lastTime) >= GameInput.SCROLL_FINAL_REPEAT_INTERVAL) {
					horzScrollState[player].tick = true;
					++horzScrollState[player].count;
					horzScrollState[player].lastTime = Time.time;
				} else {
					horzScrollState[player].tick = false;
				}
			}
		}
	}

	public static bool GetScrollUpButtonDown(int player) {
		GameInput.Update();
		return (GameInput.vertScrollState[player].tick && GameInput.vertScrollState[player].direction == -1);
	}

	public static bool GetScrollDownButtonDown(int player) {
		GameInput.Update();
		return (GameInput.vertScrollState[player].tick && GameInput.vertScrollState[player].direction == 1);
	}

	public static bool GetScrollLeftButtonDown(int player) {
		GameInput.Update();
		return (GameInput.horzScrollState[player].tick && GameInput.horzScrollState[player].direction == -1);
	}

	public static bool GetScrollRightButtonDown(int player) {
		GameInput.Update();
		return (GameInput.horzScrollState[player].tick && GameInput.horzScrollState[player].direction == 1);
	}

	private static float GetVertScrollAxis(int player) {
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

	private static float GetHorzScrollAxis(int player) {
		if (XCI.GetNumPluggedCtrlrs() > player) {
			bool left = XCI.GetButton(XboxButton.LeftBumper, player + 1);
			bool right = XCI.GetButton(XboxButton.RightBumper, player + 1);
			if (left && !right) {
				return -1;
			} else if (!left && right) {
				return 1;
			} else {
				return XCI.GetAxis(XboxAxis.LeftStickX, player + 1);
			}
		} else if (player == 0) {
			bool left = Input.GetKey("a");
			bool right = Input.GetKey("d");
			if (left && !right) {
				return -1;
			} else if (!left && right) {
				return 1;
			} else {
				return 0;
			}
		} else if (player == 1) {
			bool left = Input.GetKey("left");
			bool right = Input.GetKey("right");
			if (left && !right) {
				return -1;
			} else if (!left && right) {
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

	public static bool GetAnyTribeButtonDown(int player) {
		for(int i=0; i<4; i++) {
			if(GetTribeButtonUp(i, player)) {
				return true;
			}
		}
		return false;
	}

	public static bool GetAnyTribeButtonDownForAnyPlayer() {
		for(int i=0; i<4; i++) {
			if(GetTribeButtonUp(i, 0)) {
				return true;
			}
		}
		for(int i=0; i<4; i++) {
			if(GetTribeButtonUp(i, 1)) {
				return true;
			}
		}
		return false;
	}
}
