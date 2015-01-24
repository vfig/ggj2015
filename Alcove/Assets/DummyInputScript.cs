using UnityEngine;
using System.Collections;

public class DummyInputScript : MonoBehaviour {

	public GameObject m_camera;
	public GameObject m_cursor;
	public GameObject m_playerOneObject;
	public GameObject m_playerTwoObject;
	
	private PlayerScript m_playerOneScript;
	private PlayerScript m_playerTwoScript;

	private
	// public GameObject m_cursor;

	// Use this for initialization
	void Start () {
		m_playerOneScript = m_playerOneObject.GetComponent<PlayerScript>();
		m_playerTwoScript = m_playerTwoObject.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if (GUI.Button (new Rect (60, 60, 40, 40), "Action")) {
			m_playerOneScript.Click();
		}
		
		if (GUI.Button (new Rect (60, 10, 40, 40), "Up")) {
			m_playerOneScript.AnalogueUp(1.0f);
		}

		if (GUI.Button (new Rect (60, 110, 40, 40), "Down")) {
			m_playerOneScript.AnalogueDown(1.0f);
		}

		if (GUI.Button (new Rect (10, 60, 40, 40), "Left")) {
			m_playerOneScript.AnalogueLeft(1.0f);
		}

		if (GUI.Button (new Rect (110, 60, 40, 40), "Right")) {
			m_playerOneScript.AnalogueRight(1.0f);
		}
		
		if (GUI.Button (new Rect (660, 60, 40, 40), "Action")) {
			m_playerTwoScript.Click();
		}
		
		if (GUI.Button (new Rect (660, 10, 40, 40), "Up")) {
			m_playerTwoScript.AnalogueUp(1.0f);
		}
		
		if (GUI.Button (new Rect (660, 110, 40, 40), "Down")) {
			m_playerTwoScript.AnalogueDown(1.0f);
		}
		
		if (GUI.Button (new Rect (610, 60, 40, 40), "Left")) {
			m_playerTwoScript.AnalogueLeft(1.0f);
		}
		
		if (GUI.Button (new Rect (710, 60, 40, 40), "Right")) {
			m_playerTwoScript.AnalogueRight(1.0f);
		}
	}
}
