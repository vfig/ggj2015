﻿using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {

	void Start() {
	
	}

	void Update() {
		if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel("StartScene");
		}
	}
}