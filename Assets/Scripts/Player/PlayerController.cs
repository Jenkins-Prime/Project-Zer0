using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float gravity;
	public float maxVelocityChange;
	public float jumpHeight;

	[SerializeField]
	private bool grounded;
	private bool isCrouching;
	[SerializeField]
	private bool isJumping;
	private bool isInAir;
	private Vector3 targetVelocity;
	private float turnSpeed;
	private float slopeLimit;
	private int jumpTime;
	private int timeOnGround;

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
		jumpHeight = 0.5f;
		turnSpeed = 50.0f;
		slopeLimit = 50.0f;
		timeOnGround = 1;
		grounded = false;
		isCrouching = false;
		isInAir = false;
		isJumping = false;
	}

	void Update()
	{
		Crouch();
	}
	
	void FixedUpdate () 
	{
		MovePlayer ();
		Jump ();
	}

	private void MovePlayer()
	{
		if (grounded) 
		{
			animator.SetBool("onGround", true);
			targetVelocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			//targetVelocity = transform.TransformDirection (targetVelocity).normalized;
			targetVelocity *= speed;


			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rBody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp (velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp (velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rBody.AddForce (velocityChange, ForceMode.VelocityChange);
			isJumping = true;
			if(targetVelocity != Vector3.zero)
			{
				animator.SetFloat("speed", 1.0f);
			}
			else
			{
				animator.SetFloat("speed", 0.0f);
			}
		}
		else
		{
			rBody.AddForce(new Vector3 (0, -gravity * rBody.mass, 0));
		}

		Rotate (targetVelocity.x, targetVelocity.z);
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
		if (!Input.GetButton("Jump") && isJumping) 
		{
			jumpTime++;
		}
		else if(jumpTime >= timeOnGround)
		{
			isJumping = false;
			animator.SetBool("onGround", false);
			rBody.velocity = new Vector3 (rBody.velocity.x, CalculateJumpVerticalSpeed (), rBody.velocity.z);
			jumpTime = 0;
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
		isInAir = true;
	}

	
	float CalculateJumpVerticalSpeed () 
	{

		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}

