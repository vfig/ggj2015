using UnityEngine;
using System.Collections;

public enum UnitColour {
	Blue = 0,
	Green = 1,
	Red = 2,
	Yellow = 3
}

public class UnitColourHelper {

	public static UnitColour GetColourForTribeType(int index) {
		switch (index) {
		case 0:
			return UnitColour.Blue;
		case 1:
			return UnitColour.Green;
		case 2:
			return UnitColour.Red;
		case 3:
			return UnitColour.Yellow;
		}

		return UnitColour.Blue;
	}
}