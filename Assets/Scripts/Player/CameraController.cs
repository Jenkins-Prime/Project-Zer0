using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{

	private Transform target;
	private float distanceUp;
	private float distanceAway;
	private float rotateSpeed;
	private Vector3 targetPosition;


	
	void Awake()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		distanceUp = 2.0f;
		distanceAway = 5.0f;
		rotateSpeed = 1.0f;
	}

	void Start () 
	{
	}

	void LateUpdate()
	{
		FollowTarget ();
	}

	private void FollowTarget()
	{
		targetPosition = (target.position + target.up * distanceUp) - (target.forward * distanceAway);
		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * rotateSpeed);
		transform.LookAt (target);
	}


}
