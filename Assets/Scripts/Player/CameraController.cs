using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	private Transform player;
	private float cameraHeight;
	private float cameraDistance;
	private float smoothMove;
	private float smoothRotate;
	private float targetRotationX;
	private Vector3 cameraOffset;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start()
	{
		cameraHeight = 1.40f;
		cameraDistance = 1.0f;
		smoothMove = 1.0f;
		smoothRotate = 50.0f;
	}

	void LateUpdate()
	{
		FollowPlayer();
		OrbitPlayer ();
		LerpBackToOriginalPosition();
	}

	private void FollowPlayer()
	{
		//cameraOffset = new Vector3 (0, cameraHeight, -cameraDistance);
		transform.position = player.position;
	}

	private void OrbitPlayer()
	{
		if(Input.GetButton("Camera"))
		{
			transform.position = player.position + cameraOffset;
			targetRotationX -= Input.GetAxis("Camera") * smoothRotate * Time.smoothDeltaTime;
			transform.rotation = Quaternion.Euler(transform.rotation.x, targetRotationX, transform.rotation.z);
		}
	}

	private void LerpBackToOriginalPosition()
	{
		if(Input.GetButton ("Camera Origin"))
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, smoothMove * Time.fixedDeltaTime);
			targetRotationX = 0.0f;
		}
	}
}

