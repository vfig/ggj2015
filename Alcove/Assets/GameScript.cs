using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	public GameObject m_playerOneObject;
	public GameObject m_playerTwoObject;

	private PlayerScript m_playerOneScript;
	private PlayerScript m_playerTwoScript;

	// Use this for initialization
	void Start () {
		m_playerOneScript = m_playerOneObject.GetComponent<PlayerScript>();
		m_playerTwoScript = m_playerTwoObject.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			m_playerOneScript.Click();
		}
		if (Input.GetMouseButtonDown (0)) {
			m_playerTwoScript.Click();
		}
	}
}
