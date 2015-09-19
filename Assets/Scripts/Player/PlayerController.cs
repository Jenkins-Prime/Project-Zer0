using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float gravity;
	public float maxVelocityChange;
	public float jumpHeight;

	private bool grounded;
	private bool isCrouching;
	private Vector3 targetVelocity;
	private float turnSpeed;
	private float slopeLimit;

	private Rigidbody rBody;
	private Animator animator;
	
	
	
	void Awake () 
	{
		rBody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		rBody.freezeRotation = true;
		rBody.useGravity = false;
	}

	void Start()
	{
		speed = 4.0f;
		gravity = 10.0f;
		maxVelocityChange = 10.0f;
		jumpHeight = 2.0f;
		turnSpeed = 50.0f;
		slopeLimit = 50.0f;
		grounded = false;
		isCrouching = false;
	}

	void Update()
	{
		Crouch();
	}
	
	void FixedUpdate () 
	{
		MovePlayer ();
		Jump ();

		grounded = false;
	}

	private void MovePlayer()
	{
		if (grounded) 
		{
			targetVelocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			//targetVelocity = transform.TransformDirection (targetVelocity).normalized;
			targetVelocity *= speed;
			Rotate (targetVelocity.x, targetVelocity.z);

			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rBody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp (velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp (velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rBody.AddForce (velocityChange, ForceMode.VelocityChange);
		}
		else
		{
			rBody.AddForce(new Vector3 (0, -gravity * rBody.mass, 0));
		}
	}

	private void Crouch()
	{
		if(Input.GetButton("Crouch") && grounded)
		{	
			isCrouching = true;
			animator.SetBool ("Crouching", true);
		}
		
		if(Input.GetButtonUp("Crouch") && grounded)
		{
			isCrouching = false;
			animator.SetBool ("Crouching", false);
		}
	}



	private void Rotate(float horizontal, float vertical)
	{
		if(targetVelocity != Vector3.zero)
		{
			targetVelocity = new Vector3(horizontal, 0.0f, vertical);
			targetVelocity = Camera.main.transform.TransformDirection (targetVelocity);
			Quaternion targetRotation = Quaternion.LookRotation(targetVelocity);
			Quaternion newRotation = Quaternion.Lerp(rBody.rotation, targetRotation, turnSpeed * Time.deltaTime);
			rBody.MoveRotation(newRotation);
		}

	}


	private void Jump()
	{
		if (Input.GetButton ("Jump") && grounded) 
		{
			rBody.velocity = new Vector3 (rBody.velocity.x, CalculateJumpVerticalSpeed (), rBody.velocity.z);
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

	
	float CalculateJumpVerticalSpeed () 
	{

		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}

