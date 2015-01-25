using UnityEngine;
using System.Collections;

public enum UnitColour {
	Blue,
	Green,
	Red,
	Yellow
}

public class UnitColourHelper {

	public static UnitColour GetColourForTribeType(int index) {
		switch (index) {
		case 0:
			return UnitColour.Blue;
			break;
		case 1:
			return UnitColour.Green;
			break;
		case 2:
			return UnitColour.Red;
			break;
		case 3:
			return UnitColour.Yellow;
			break;
		}

		return UnitColour.Blue;
	}
}