using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	private Transform player;
	private float smoothMove;
	private float smoothRotate;
	private float targetRotationX;
	private bool isRotating;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start()
	{
		smoothMove = 70.0f;
		smoothRotate = 50.0f;
		isRotating = false;
	}

	void LateUpdate()
	{
		FollowPlayer();
		OrbitPlayer ();
		LerpBackToOriginalPosition();

		if(isRotating)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, player.rotation, smoothMove * Time.fixedDeltaTime);
			targetRotationX = 0.0f;
		}
		
		if(transform.rotation == player.rotation)
		{
			isRotating = false;
		}
	}

	private void FollowPlayer()
	{
		transform.position = player.position;
	}

	private void OrbitPlayer()
	{
		if(Input.GetButton("Camera"))
		{
			targetRotationX -= Input.GetAxis("Camera") * smoothRotate * Time.smoothDeltaTime;
			transform.rotation = Quaternion.Euler(0, targetRotationX, 0);
		}
	}

	private void LerpBackToOriginalPosition()
	{
		if(Input.GetButton("Camera Origin"))
		{
			isRotating = true;
		}
	}
}

