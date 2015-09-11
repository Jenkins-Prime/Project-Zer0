using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	private Transform player;
	private float cameraHeight;
	private float cameraDistance;
	private float smoothMove;
	private float smoothRotate;
	private Quaternion playerLook;
	private Vector3 playerPosition;
	private Vector3 velocityReference;
	private Vector3 cameraOffset;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start()
	{
		cameraHeight = 1.40f;
		cameraDistance = 5.0f;
		smoothMove = 1.0f;
		smoothRotate = 100.0f;
	}

	void LateUpdate()
	{
		FollowPlayer();
	}

	private void FollowPlayer()
	{
		cameraOffset = new Vector3 (0.0f, cameraHeight, -cameraDistance);
		playerPosition = player.position + (player.rotation * cameraOffset);
		transform.position = Vector3.Lerp (transform.position, playerPosition, Time.deltaTime * smoothMove);

		playerLook = Quaternion.LookRotation (player.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, playerLook, smoothRotate);
		transform.LookAt (player);
	}
}

