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
	
	
	[HideInInspector]
	public bool grounded = false;
	
	
	private float walkFrictionVelocityX;
	private float walkFrictionVelocityZ;
	private Vector2 horizontalMovement;
	private Rigidbody rigidbody;
	private Animator animator;
	private float movementHorizontal;
	private float movementVertical;


	void Awake()
	{
		rigidbody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		walkAcceleration = 5000.0f;
		walkFriction = 7.0f;
		walkAccelerationAirRatio = 0.1f;
		maxWalkSpeed = 3.0f;
		jumpVelocity = 250.0f;
		slopeLimit = 50.0f;
		runtime = 5.0f;


	}

	void Start()
	{
		if(animator == null)
		{
			Debug.LogError("You are missing an animator component");
			return;
		}
	}

	void FixedUpdate()
	{
		Move ();
		if(Input.GetButtonDown("Jump") && grounded && movementHorizontal == 0)
		{
			Jump();
		}

		movementHorizontal = Input.GetAxis("Horizontal");
		movementVertical = Input.GetAxis("Vertical");
	}

	private void Move()
	{
		horizontalMovement = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);

		
		if(horizontalMovement.magnitude > maxWalkSpeed)
		{
			horizontalMovement = horizontalMovement.normalized;
			horizontalMovement *= maxWalkSpeed;
		}
		
		rigidbody.velocity = new Vector3(horizontalMovement.x * walkFriction, rigidbody.velocity.y, horizontalMovement.y);
		
		if(grounded)
		{
	
			rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * walkFriction * Time.fixedDeltaTime, rigidbody.velocity.y, Input.GetAxis("Vertical") * walkFriction * Time.fixedDeltaTime);

			animator.SetFloat("speed", movementHorizontal);
			animator.SetFloat("speed", movementVertical);
		}
		else
		{
			rigidbody.AddForce(horizontalMovement.x * Time.fixedDeltaTime * walkAcceleration * walkAccelerationAirRatio, 0,
			                   horizontalMovement.y * Time.fixedDeltaTime * walkAcceleration * walkAccelerationAirRatio);
			animator.SetFloat("speed", 0.0f);
			animator.SetFloat("speed", 0.0f);

		}

	}

	private void Jump()
	{
		rigidbody.AddForce(0, jumpVelocity, 0);
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
