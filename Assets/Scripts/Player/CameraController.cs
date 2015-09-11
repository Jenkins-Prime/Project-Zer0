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
	private Quaternion playerLook;
	private Vector3 playerPosition;
	private Vector3 cameraOffset;
	private Vector3 playerRotation;
	private bool isRotating;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start()
	{
		isRotating = false;
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
		cameraOffset = new Vector3 (0, cameraHeight, -cameraDistance);
		transform.position = player.position + cameraOffset;

		if(Input.GetButton("Camera") && !isRotating)
		{
			isRotating = true;
		}

		if(Input.GetButtonUp("Camera") && isRotating)
		{
			isRotating = false;
		}
	}

	private void OrbitPlayer()
	{
		if(isRotating)
		{
			playerRotation = player.rotation * cameraOffset;
			transform.position = player.position + cameraOffset;
			targetRotationX -= Input.GetAxis("Camera") * smoothRotate * Time.smoothDeltaTime;
			transform.rotation = Quaternion.Euler(0, targetRotationX, transform.rotation.z);


		}
	}

	private void LerpBackToOriginalPosition()
	{
		if(Input.GetButton ("Camera Origin"))
		{
			transform.localRotation = Quaternion.RotateTowards(transform.localRotation, player.localRotation, smoothRotate * Time.fixedDeltaTime);
			targetRotationX = 0.0f;

		}
	}
}

