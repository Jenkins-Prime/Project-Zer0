using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	[SerializeField]
	float distanceAway;
	[SerializeField]
	float distanceUp;
	[SerializeField]
	float smooth;
	Transform follow;
	Vector3 targetPosition;

	void Start () {
		follow = GameObject.FindGameObjectWithTag ("CameraFollow").transform;
	}

	void FixedUpdate() { //Probably needs to be changed to LateUpdate
		targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;
		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * smooth);
		transform.LookAt(follow);
	}
}
