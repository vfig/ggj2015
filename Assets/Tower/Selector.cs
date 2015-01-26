using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Selector : MonoBehaviour {
	public string buildingCostLabel;
	public Text buildingCostText;

	public void Update () {
		buildingCostText.text = buildingCostLabel;
	}
}
