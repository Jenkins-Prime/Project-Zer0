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
	public float cameraXAxis;


	private float rayDistance;
	private Vector3 desiredPosition;
	private Vector3 originalPosition;
	private bool isRotating;
	public bool isColliding;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start()
	{
		smoothMove = 0.1f;
		smoothRotate = 100.0f;
		cameraHeight = 1.0f;
		desiredCameraHeight = 0.58f;
		cameraDistance = 3.0f;
		desiredCameraDistance = 1.03f;
		rayDistance = 1.0f;
		cameraXAxis = 3.13f;
		isRotating = false;
		isColliding = false;


	}

	void Update()
	{
		originalPosition = new Vector3(0.0f, player.position.y + cameraHeight, -cameraDistance);
		originalPosition = transform.TransformDirection (originalPosition);

		desiredPosition = new Vector3(0.0f, player.position.y + desiredCameraHeight, -desiredCameraDistance);
		desiredPosition = transform.TransformDirection (desiredPosition);

		if(Input.GetKeyDown (KeyCode.Y))
		{
			isColliding = false;
		}

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
		if(!isColliding)
		{
			transform.position = Vector3.Lerp (transform.position, player.position + originalPosition, smoothMove);
			originalPosition = transform.position - player.position;
		}
		else
		{
			transform.position = Vector3.Lerp (transform.position, player.position + desiredPosition, smoothMove);
			desiredPosition = transform.position - player.position;
		}

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
			isColliding = true;
		}
	


		Debug.DrawLine(Camera.main.transform.position, player.position);

	}
	
}

