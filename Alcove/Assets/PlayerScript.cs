using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public GameObject m_towerObject;
	private TowerScript m_towerScript;
	
	// Use this for initialization
	void Start () {
		m_towerScript = m_towerObject.GetComponent<TowerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Click() {
		m_towerScript.AddTowerSegment ();
	}
}
