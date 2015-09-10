using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public float cameraDistanceY;
	public float cameraDistanceZ;

	private Transform player;
	private float rotateSpeed;
	private float moveSpeed;
	private Vector3 cameraOffset;
	private Quaternion playerLook;
	private Vector3 smoothVelocity;
	private Vector3 targetPosition;


	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Start () 
	{
		cameraDistanceY = 1.40f;
		cameraDistanceZ = 5.0f;
		rotateSpeed = 2.0f;
		moveSpeed = 0.1f;

	}

	void LateUpdate()
	{
		FollowTarget ();
	}

	private void FollowTarget()
	{
		cameraOffset = new Vector3 (0.0f, cameraDistanceY, -cameraDistanceZ);
		targetPosition = player.position + (transform.rotation * cameraOffset);
		transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref smoothVelocity, moveSpeed);

		//playerLook = Quaternion.LookRotation (player.position - transform.position);
		//transform.rotation = Quaternion.Slerp (transform.rotation, playerLook, Time.deltaTime * rotateSpeed);

	}


}
