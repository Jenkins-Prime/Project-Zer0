using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	private Transform player;
	private float smoothMove;
	private float smoothRotate;

	public float cameraHeight;
	public float cameraDistance;
	public float desiredCameraHeight;
	public float desiredCameraDistance;

	private float rayDistance;
	private Vector3 desiredPosition;
	private Vector3 originalPosition;
	private bool isRotating;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start()
	{
		smoothMove = 1.0f;
		smoothRotate = 100.0f;
		cameraHeight = 1.0f;
		desiredCameraHeight = 1.0f;
		cameraDistance = 3.0f;
		desiredCameraDistance = 2.0f;
		rayDistance = 1.0f;
		isRotating = false;


	}

	void Update()
	{
		originalPosition = new Vector3(0.0f, player.position.y + cameraHeight, -cameraDistance);
		originalPosition = transform.TransformDirection (originalPosition);

		desiredPosition = new Vector3(0.0f, player.position.y + cameraHeight, -desiredCameraHeight);
		desiredPosition = transform.TransformDirection (desiredPosition);

	}

	void LateUpdate()
	{

		FollowPlayer();
		OrbitPlayer ();
		CheckForCollision ();
		LerpBackToOriginalPosition();

		if(isRotating)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, player.rotation, smoothRotate * Time.deltaTime);
		}
		
		if(transform.rotation == player.rotation)
		{
			isRotating = false;
		}
	}
	
	private void FollowPlayer()
	{
		transform.position = player.position + originalPosition;
		originalPosition = transform.position - player.position;
	}

	private void OrbitPlayer()
	{

		if(Input.GetButton("Camera"))
		{
			transform.RotateAround(player.position, Vector3.up * Input.GetAxis ("Camera"), smoothRotate * Time.deltaTime);
		}
	}

	private void LerpBackToOriginalPosition()
	{
		if(Input.GetButton("Camera Origin"))
		{
			isRotating = true;
		}
	}

	private void CheckForCollision()
	{
		RaycastHit hitInfo;

		if(Physics.Linecast(Camera.main.transform.position, player.position, out hitInfo))
		{
			isRotating = true;
		}

		Debug.DrawLine(Camera.main.transform.position, player.position);

	}
	
}

