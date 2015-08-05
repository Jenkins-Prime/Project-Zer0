using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	Transform player;	
	Quaternion targetLook;
	Vector3 targetMove;
	public float smoothLook = 7.0f;
	public float smoothMove = 4.0f;
	public float distanceFromPlayer = 3.0f;
	public float cameraHeight = 2.0f;

	void Awake() {

	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	void FixedUpdate () {
		targetLook = Quaternion.LookRotation (player.position -transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetLook, smoothLook * Time.deltaTime);

		targetMove = player.position + player.rotation * new Vector3 (0, cameraHeight, -distanceFromPlayer);
		transform.position = Vector3.Lerp(transform.position, targetMove, smoothMove * Time.deltaTime);
	}
}
