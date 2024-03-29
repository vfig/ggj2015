﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PrefabSelector : MonoBehaviour {
	private int m_selectedIndex;
	private Transform m_selectedSprite;
	private List<TowerSegment> m_towerSegments;
	private const float spriteSpacing = 5;

	public Text buildingCost;

	void Awake () {
		m_towerSegments = new List<TowerSegment>();
	}

	public void Show() {
		if (m_selectedSprite) {
			m_selectedSprite.GetComponent<SpriteRenderer>().enabled = true;
			GetComponent<Canvas>().enabled = true;
		}
	}

	public void Hide() {
		if (m_selectedSprite) {
			m_selectedSprite.GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<Canvas>().enabled = false;
		}
	}

	public void Start() {
		GameObject obj = new GameObject();
		SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
		spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.75f);
		spriteRenderer.sortingOrder = 4;
		obj.transform.localScale = 0.75f * Vector3.one;
		obj.transform.parent = transform;
		obj.transform.localPosition = Vector3.zero;
		m_selectedSprite = obj.transform;
		if (m_selectedSprite == null) {
			Debug.Log("m_selectedSprite is null: " + m_selectedSprite);
		}
	}

	public void AddSelection(TowerSegment segment) {
		m_towerSegments.Add(segment);
	}

	public void SetSelectedIndex(int index) {
		m_selectedIndex = index;
		TowerSegment segment = m_towerSegments[m_selectedIndex];
		Sprite sprite = segment.GetComponent<SpriteRenderer>().sprite;
		m_selectedSprite.GetComponent<SpriteRenderer>().sprite = sprite;
	}

	public void Update () {
		TowerSegment segment = m_towerSegments[m_selectedIndex];
		int size = segment.OnGetMinimumTribeSize();
		if (size == 0) {
			buildingCost.text = "";
		} else {
			buildingCost.text = size.ToString() + " workers from each tribe needed";
		}
	}
}
