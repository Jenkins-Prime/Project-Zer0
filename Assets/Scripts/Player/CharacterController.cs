using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
	private float turnSmoothing;   
	private float speedDampTime;  
	private float moveSpeed;
	private float jumpVelocity;
	private float slopeLimit;
	private float rotationVelocity;
	private bool isCrouching;
	private bool isGrounded;


	private Animator animator;                   
	private Rigidbody rb;
	private Transform cameraTransform;


	void Awake ()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody> ();
		cameraTransform = Camera.main.transform;
	}

	void Start()
	{
		moveSpeed = 200.0f;
		turnSmoothing = 15.0f;
		speedDampTime = 0.1f;
		slopeLimit = 50.0f;
		jumpVelocity = 250.0f;
		isCrouching = false;
		isGrounded = false;

	}

	void Update()
	{
		Crouch ();
	}

	void FixedUpdate ()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		
		Move(horizontal, vertical);
		Jump ();
	}
	
	void Move(float horizontal, float vertical)
	{
		if(horizontal != 0f || vertical != 0f)
		{
			Rotate(horizontal, vertical);
			CheckMovement(horizontal, vertical);
		}
		else
		{
			animator.SetFloat("speed", 0.0f);
		}
			
	}

	private void Rotate(float horizontal, float vertical)
	{
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		//Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
		Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		rb.MoveRotation(newRotation);
	}

	private void CheckMovement(float horizontal, float vertical)
	{
		if(isGrounded)
		{
			if(vertical < 0)
			{
				rb.velocity = -transform.forward * vertical * moveSpeed * Time.fixedDeltaTime;
				animator.SetFloat("speed", 1.0f);
			}
			
			if(vertical > 0)
			{
				rb.velocity = transform.forward * vertical * moveSpeed * Time.fixedDeltaTime;
				animator.SetFloat("speed", 1.0f);
			}
			
			if(horizontal < 0)
			{
				rb.velocity = transform.forward * -horizontal * moveSpeed * Time.fixedDeltaTime;
				animator.SetFloat("speed", 1.0f);
			}
			
			if(horizontal > 0)
			{
				rb.velocity = transform.forward * horizontal * moveSpeed * Time.fixedDeltaTime;
				animator.SetFloat("speed", 1.0f);
			}
		}
		else
		{
			animator.SetFloat("speed", 0.5f);
		}

	}


	private void Crouch()
	{
		if(Input.GetButton("Crouch") && isGrounded)
		{	
			isCrouching = true;
			animator.SetBool ("Crouching", true);

		}

		if(Input.GetButtonUp("Crouch") && isGrounded)
		{
			isCrouching = false;
			animator.SetBool ("Crouching", false);
		}
	}
	
	private void Jump()
	{
		if(Input.GetButtonDown("Jump") && isGrounded)
		{
			rb.AddForce(0, jumpVelocity, 0);
		}
	}

	private void RotatePlayer()
	{

	}

	void OnCollisionStay(Collision collision)
	{
		foreach(ContactPoint contact in collision.contacts)
		{
			if(Vector3.Angle(contact.normal, Vector3.up) < slopeLimit)
			{
				isGrounded = true;
			}
		}
	}
	
	void OnCollisionExit()
	{
		isGrounded = false;
	}

}