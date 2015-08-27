using UnityEngine;
using System.Collections;

public class LocomotionController : MonoBehaviour 
{
		
	public float walkAcceleration;
	public float walkFriction;
	public float walkAccelerationAirRatio;
	public float maxWalkSpeed;
	public float jumpVelocity;
	public float slopeLimit;
	public float runtime;
	
	
	//[HideInInspector]
	public bool grounded = false;
	
	
	private float walkFrictionVelocityX;
	private float walkFrictionVelocityZ;
	private Vector2 horizontalMovement;
	private Rigidbody rigidBody;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody> ();
		walkAcceleration = 5000.0f;
		walkFriction = 0.2f;
		walkAccelerationAirRatio = 0.1f;
		maxWalkSpeed = 3.0f;
		jumpVelocity = 250.0f;
		slopeLimit = 50.0f;
		runtime = 5.0f;
	}

	void FixedUpdate()
	{
		Move ();

		if(Input.GetButtonDown("Jump") && grounded)
		{
			Jump();
		}
	}

	private void Move()
	{
		horizontalMovement = new Vector2(rigidBody.velocity.x, rigidBody.velocity.z);
		
		if(horizontalMovement.magnitude > maxWalkSpeed)
		{
			horizontalMovement = horizontalMovement.normalized;
			horizontalMovement *= maxWalkSpeed;
		}
		
		rigidBody.velocity = new Vector3(horizontalMovement.x, rigidBody.velocity.y, horizontalMovement.y);
		
		if(grounded)
		{
			rigidBody.AddForce(Input.GetAxis("Horizontal") * Time.fixedDeltaTime * walkAcceleration, 0,
			                           Input.GetAxis("Vertical") * Time.deltaTime * walkAcceleration);
		}
		else
		{
			rigidBody.AddForce(Input.GetAxis("Horizontal") * Time.fixedDeltaTime * walkAcceleration * walkAccelerationAirRatio, 0,
			                           Input.GetAxis("Vertical") * Time.fixedDeltaTime * walkAcceleration * walkAccelerationAirRatio);
		}

	}

	private void Jump()
	{
		rigidBody.AddForce(0, jumpVelocity, 0);
	}

	void OnCollisionStay(Collision collision)
	{
		foreach(ContactPoint contact in collision.contacts)
		{
			if(Vector3.Angle(contact.normal, Vector3.up) < slopeLimit)
			{
				grounded = true;
			}
		}
	}
	
	void OnCollisionExit()
	{
		grounded = false;
	}
	
}
