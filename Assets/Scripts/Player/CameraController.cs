using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	private Transform player;
	private float smoothMove;
	private float smoothRotate;
	private float targetRotationX;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start()
	{
		smoothMove = 30.0f;
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
		if(Input.GetButton ("Camera Origin"))
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, player.rotation, smoothMove * Time.fixedDeltaTime);
			targetRotationX = 0.0f;
		}
	}
}

