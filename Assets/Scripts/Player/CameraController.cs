using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	private Transform player;
	private float smoothMove;
	private float smoothRotate;
	public float cameraHeight;
	public float cameraDistance;
	private Vector3 initialOffset;
	private Vector3 currentOffset;
	private float rayDistance;
	private Vector3 desiredPosition;
	private Vector3 originalPosition;
	private Vector3 collisionOffset;
	private bool isRotating;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start()
	{
		smoothMove = 100.0f;
		smoothRotate = 50.0f;
		cameraHeight = 1.0f;
		rayDistance = 1.0f;
		cameraDistance = 3.0f;
		isRotating = false;
		originalPosition = new Vector3(0.0f, player.position.y + cameraHeight, -cameraDistance);
		initialOffset = transform.position - player.position;
		currentOffset = initialOffset;

	}

	void Update()
	{
		//originplayer.palPosition = new Vector3 (transform.position.x + player.position.x, transform.position.y - player.position.y, transform.position.z + player.position.z);
		//CheckForCollision ();
		//FollowPlayer();
		OrbitPlayer ();
	}

	void LateUpdate()
	{
		//originalPosition = new Vector3 (0, player.position.y + cameraHeight, -Camera.main.transform.position.z);
		//desiredPosition = new Vector3 (0, player.position.y + cameraHeight, -cameraDistance - 10.0f);


		LerpBackToOriginalPosition();

		if(isRotating)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, player.rotation, smoothMove * Time.deltaTime);
		}
		
		if(transform.rotation == player.rotation)
		{
			isRotating = false;
		}
	}
	
	private void FollowPlayer()
	{
		//transform.position = player.position + originalPosition;
	}

	private void OrbitPlayer()
	{
		transform.position = player.position + originalPosition;
		if(Input.GetButton("Camera"))
		{
			transform.RotateAround(player.position, Vector3.up * Input.GetAxis ("Camera"), smoothMove * Time.deltaTime);
			originalPosition = transform.position - player.position;
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
			Debug.Log (hitInfo.collider.name);
			Vector3 hitPoint = new Vector3(0.0f, Mathf.Clamp (hitInfo.transform.position.y, cameraHeight, 3.0f), 0.0f);
			collisionOffset += hitPoint;
			originalPosition += hitPoint;
			transform.position = Vector3.Lerp(transform.position, originalPosition, 0.25f);
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(player.position - transform.position), smoothRotate * Time.deltaTime);
			Debug.Log (hitPoint);
		}
		else
		{
			originalPosition -= collisionOffset;
			originalPosition.y = Mathf.Clamp(originalPosition.y, cameraHeight, 3.0f);
			collisionOffset = Vector3.zero;
			transform.position = Vector3.Lerp(transform.position, originalPosition, 0.25f);
		}

		Debug.DrawLine(Camera.main.transform.position, player.position);
		//Camera.main.transform.position = Vector3.Lerp(transform.position, player.position, 0.25f);
	}
	
}

