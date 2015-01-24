using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class GameInput {
	public static float GetScrollAxis(int player) {
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
