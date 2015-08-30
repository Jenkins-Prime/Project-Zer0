using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour 
{
	// Public Variables
	
	public float walkAcceleration = 1000.0f;
	public float walkFriction = 0.2f;
	public float walkAccelerationAirRatio = 0.1f;
	public float maxWalkSpeed = 3.0f;
	public float jumpVelocity = 250.0f;
	public float slopeLimit = 50.0f;
	public float runtime = 5.0f;
	
	// Public Hidden Variables

	public bool grounded = false;

	private float walkFrictionVelocityX;
	private float walkFrictionVelocityZ;
	private Vector2 horizontalMovement;
	private float horizontal;
	private float vertical;
	new private Rigidbody rigidbody;
	private Animator animator;

	void Awake()
	{
		rigidbody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
	}

	void Start()
	{
		rigidbody.freezeRotation = true;
	}

	void Update()
	{

	}

	void FixedUpdate()
	{
		Move ();
		Crouch ();
		Jump ();
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");
	}

	private void Move()
	{
		horizontalMovement = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);
		
		if(horizontalMovement.magnitude > maxWalkSpeed)
		{
			horizontalMovement = horizontalMovement.normalized;
			horizontalMovement *= maxWalkSpeed;
		}
		
		rigidbody.velocity = new Vector3(horizontalMovement.x, rigidbody.velocity.y, horizontalMovement.y);
		
		if(grounded)
		{
			rigidbody.velocity = new Vector3(Mathf.SmoothDamp(rigidbody.velocity.x, 0, ref walkFrictionVelocityX, walkFriction),
			                                 rigidbody.velocity.y,
			                                 Mathf.SmoothDamp(rigidbody.velocity.z, 0, ref walkFrictionVelocityZ, walkFriction));
			rigidbody.AddRelativeForce(horizontal * Time.fixedDeltaTime * walkAcceleration, 0,
			                           vertical * Time.fixedDeltaTime * walkAcceleration);

			animator.SetFloat("speed", vertical);

		}
		
		else
		{
			rigidbody.AddRelativeForce(horizontal * Time.fixedDeltaTime * walkAcceleration * walkAccelerationAirRatio, 0,
			                           vertical * Time.fixedDeltaTime * walkAcceleration * walkAccelerationAirRatio);
			animator.SetFloat("speed", 0.0f);
		}
	}

	private void Crouch()
	{
		if(Input.GetButton("Crouch") && grounded)
		{
			animator.SetBool("Crouching", true);
		}

		if(Input.GetButtonUp("Crouch") && grounded)
		{
			animator.SetBool("Crouching", false);
		}

	}

	private void Jump()
	{
		if(Input.GetButtonDown("Jump") && grounded)
		{
			rigidbody.AddForce(0, jumpVelocity, 0);
		}
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

