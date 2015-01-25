using UnityEngine;
using System.Collections;

public class FollowCursor : MonoBehaviour {
	new private Camera camera;
	private Transform cameraTransform;
	public Transform cursorTransform;
	public const float viewportMargin = 0.05f;

	public void Start () {
		camera = GetComponent<Camera>();
		cameraTransform = camera.transform;
	}
	
	public void Update () {
		Vector3 cameraPosition = cameraTransform.position;
		Vector3 cursorPosition = cursorTransform.position;

		// Find the min world space X and Y for the cursor that will keep it inside the viewport margin.
		const float halfCursorHeight = 1.0f;
		Vector3 cursorBottom = camera.WorldToViewportPoint(cursorPosition + Vector3.down * halfCursorHeight);
		Vector3 cursorTop = camera.WorldToViewportPoint(cursorPosition + Vector3.up * halfCursorHeight);
		cursorBottom.y = Mathf.Clamp(cursorBottom.y, viewportMargin, 1.0f - viewportMargin);
		cursorBottom.z = -cameraPosition.z;
		cursorTop.y = Mathf.Clamp(cursorTop.y, viewportMargin, 1.0f - viewportMargin);
		cursorTop.z = -cameraPosition.z;
		float minY = camera.ViewportToWorldPoint(cursorBottom).y;
		float maxY = camera.ViewportToWorldPoint(cursorTop).y;

		minY = Mathf.Max(minY, 3.5f);

		// Lerp the camera to the desired position if possible, yet clamp to ensure it is always on screen.
		Vector3 idealCameraPosition = new Vector3(cursorPosition.x, cursorPosition.y, cameraPosition.z);
		Vector3 lerpedCameraPosition = Vector3.Lerp(cameraPosition, idealCameraPosition, Time.deltaTime);
		lerpedCameraPosition.y = Mathf.Clamp(lerpedCameraPosition.y, minY, maxY);
		camera.transform.position = lerpedCameraPosition;
	}
}
