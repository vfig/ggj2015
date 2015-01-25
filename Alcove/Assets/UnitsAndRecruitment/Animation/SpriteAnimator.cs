using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteAnimator : MonoBehaviour {

	[HideInInspector]
	public bool isPlaying;
	[HideInInspector]
	private float time;
	[HideInInspector]
	public int currentFrame;
	[HideInInspector]
	private int totalFrames;
	[HideInInspector]
	private AnimationChannel currentChannel;

	public float animationSpeed = 4.0f;
	public Dictionary<string, AnimationChannel> channels;
	public SpriteRenderer renderer;

	void Awake() {
		channels = new Dictionary<string, AnimationChannel>();
	}

	void Start() {
		isPlaying = true;
		Reset();
	}

	void Reset() {
		time = 0.0f;
		currentFrame = 0;
	}

	public void AddChannel(AnimationChannel channel, string name) {
		channels[name] = channel;
	}

	public void SelectChannel(string name) {
		//Debug.Log ("Selecting channel. Available channels: " + channels.Keys.Count);
		currentChannel = channels[name];
		totalFrames = currentChannel.sprites.Length;
		//Reset();
	}

	void Update() {
		if(isPlaying && currentChannel != null) {
			time += Time.deltaTime * animationSpeed;
			if(time >= totalFrames) {
				time -= totalFrames;
			}
			int intTime = (int)Mathf.Floor(time);
			currentFrame = Mathf.Clamp(intTime, 0, totalFrames-1);
			//Debug.Log("time: " + time + ", currentFrame: " + currentFrame);
			renderer.sprite = (currentChannel as AnimationChannel).sprites[currentFrame];
		}
	}
}
