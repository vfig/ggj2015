using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabSelector : MonoBehaviour {
	private List<Transform> m_selections;
	private const float spriteSpacing = 5;

	void Awake () {
		m_selections = new List<Transform>();
	}

	public void Start() {
	}

	public void AddSelection(Sprite sprite) {
		GameObject obj = new GameObject();
		SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite;
		spriteRenderer.enabled = false;
		spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.75f);
		spriteRenderer.sortingOrder = 4;
		obj.transform.localScale = 0.75f * Vector3.one;
		obj.transform.parent = transform;
		obj.transform.localPosition = Vector3.zero;
		m_selections.Add(obj.transform);
	}

	public void SetSelectedIndex(int index) {
		for (int i = 0; i < m_selections.Count; ++i) {
			SpriteRenderer spriteRenderer = m_selections[i].gameObject.GetComponent<SpriteRenderer>();
			spriteRenderer.enabled = (i == index);
		}
	}

	public void Update () {
	}
}
