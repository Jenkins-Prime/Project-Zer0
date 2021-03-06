using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour 
{
	public float speed;
	public float gravity;
	public float maxVelocityChange;
	public float jumpHeight;
	public float airControl;

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
	private Vector3 velocity;
	private Vector3 velocityChange;

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
		speed = 5.0f;
		gravity = 10.0f;
		maxVelocityChange = 10.0f;
		jumpHeight = 0.5f;
		airControl = 3.0f;
		turnSpeed = 50.0f;
		slopeLimit = 50.0f;
		timeOnGround = 1;
		grounded = false;
		isCrouching = false;
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
			isJumping = true;
			animator.SetBool("onGround", true);
			targetVelocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical")).normalized;
			targetVelocity *= speed;
			targetVelocity = Camera.main.transform.TransformDirection(targetVelocity);


			velocity = rBody.velocity;
			velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp (velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp (velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			Rotate(targetVelocity.x, targetVelocity.z);
			rBody.AddForce(velocityChange, ForceMode.VelocityChange);

			if(targetVelocity != Vector3.zero)
			{
				animator.SetFloat("speed", 5.0f);
			}
			else
			{
				animator.SetFloat("speed", 0.0f);
			}
		}
		else
		{
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");

			if(h != 0 || v != 0)
			{
				targetVelocity = new Vector3(h, 0.0f, v);
				targetVelocity = Camera.main.transform.TransformDirection (targetVelocity);
				Quaternion targetRotation = Quaternion.LookRotation(targetVelocity);
				Quaternion newRotation = Quaternion.Lerp(rBody.rotation, targetRotation, turnSpeed * Time.deltaTime);
				rBody.MoveRotation(newRotation);
				rBody.AddForce(new Vector3 (h, 0.0f, v));
			}

			animator.SetFloat("speed", 0.0f);

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
			Quaternion targetRotation = Quaternion.LookRotation(targetVelocity);
			Quaternion newRotation = Quaternion.Lerp(rBody.rotation, targetRotation, turnSpeed * Time.deltaTime);
			rBody.MoveRotation(newRotation);
		}
	}



	private void Jump()
	{
		if (Input.GetButtonDown("Jump") && isJumping) 
		{
			animator.SetBool("onGround", false);
			rBody.velocity = new Vector3 (0, CalculateJumpVerticalSpeed (), 0);
			isJumping = false;
		}
		else
		{
			rBody.AddForce(new Vector3 (0, -gravity * rBody.mass, 0));
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

	
	private float CalculateJumpVerticalSpeed () 
	{
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}

