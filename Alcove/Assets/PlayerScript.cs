using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public GameObject m_towerObject;
	public TowerScript m_towerScript;
	
	// Use this for initialization
	void Start () {
		m_towerScript = m_towerObject.GetComponent<TowerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
