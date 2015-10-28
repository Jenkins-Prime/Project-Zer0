using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	private Transform player;
	private UIManager manager;
	private float smoothMove;
	private float smoothRotate;

	public float cameraHeight;
	public float cameraDistance;
	public float desiredCameraHeight;
	public float desiredCameraDistance;
	public float cameraZoomHeight;
	public float cameraZoomDistance;
	public float cameraZoomXDistance;

	private float mouseX;
	private float mouseY;
	private float mouseSensitivityX;
	private float mouseSensitivityY;

	private float rayDistance;
	private Vector3 desiredPosition;
	private Vector3 originalPosition;
	private Vector3 firePosition;
	private bool isRotating;
	[SerializeField]
	private bool isColliding;
	[SerializeField]
	private bool isZoomed;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		manager = GameObject.FindGameObjectWithTag ("UIManager").GetComponent<UIManager> ();
	}

	void Start()
	{
		smoothMove = 0.1f;
		smoothRotate = 100.0f;
		cameraHeight = -10.81f;
		desiredCameraHeight = 0.58f;
		cameraDistance = 15.41f;
		desiredCameraDistance = 1.03f;
		rayDistance = 1.0f;
		cameraZoomHeight = 1.0f;
		cameraZoomDistance = 0.1f;
		cameraZoomXDistance = 2.042f;

		mouseX = 0.0f;
		mouseY = 0.0f;
		mouseSensitivityX = 100.0f;
		mouseSensitivityY = 100.0f;



		isRotating = false;
		isColliding = false;
		isZoomed = true;


	}

	void Update()
	{
		originalPosition = new Vector3(0.0f, player.position.y + cameraHeight, -cameraDistance);
		originalPosition = transform.TransformDirection (originalPosition);

		desiredPosition = new Vector3(0.0f, player.position.y + desiredCameraHeight, -desiredCameraDistance);
		desiredPosition = transform.TransformDirection (desiredPosition);

		firePosition = new Vector3(cameraZoomXDistance, player.position.y + cameraZoomHeight, -cameraZoomDistance);
		firePosition = transform.TransformDirection (firePosition);

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

		if(Input.GetButton("Zoom"))
		{
			isZoomed = true;
			manager.ShowTarget();
			FireMode();
			Cursor.visible = false;


		}
		else
		{
			isZoomed = false;
			manager.HideTarget();
			Cursor.visible = true;
		}

		if(isZoomed)
		{
			transform.position = Vector3.Lerp (transform.position, player.position + firePosition, smoothMove);
			firePosition = transform.position - player.position;


		}
		else
		{
			transform.position = Vector3.Lerp (transform.position, player.position + originalPosition, smoothMove);
			originalPosition = transform.position - player.position;

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

	private void FireMode()
	{
		isRotating = true;
		Vector3 mousePos;
		mouseX += Input.GetAxis ("Mouse X") * mouseSensitivityX * Time.deltaTime;
		mouseY += Input.GetAxis ("Mouse Y") * mouseSensitivityY * Time.deltaTime;
		mouseY = Mathf.Clamp (mouseY, -25.0f, 25.0f);
		mousePos = new Vector3(-mouseY, mouseX, 0.0f);
		player.eulerAngles = mousePos;
		mousePos = player.position;
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

