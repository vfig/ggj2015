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