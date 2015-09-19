using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	private bool grounded = false;
	private Vector3 groundVelocity;
	private CapsuleCollider capsule;
	private Rigidbody rBody;

	private bool jumpFlag = false;

	public float walkSpeed = 8.0f;
	public float walkBackwardSpeed = 4.0f;
	public float runSpeed = 14.0f;
	public float runBackwardSpeed = 6.0f;
	public float sidestepSpeed = 8.0f;
	public float runSidestepSpeed = 12.0f;
	public float maxVelocityChange = 10.0f;

	public float inAirControl = 0.1f;
	public float jumpHeight = 2.0f;

	public bool canRunSidestep = true;
	public bool canJump = true;
	public bool canRun = true;

	private Vector3 inputVector;
	private float turnSpeed;

	void Awake()
	{
		capsule = GetComponent<CapsuleCollider>();
		rBody = GetComponent<Rigidbody> ();
		rBody.freezeRotation = true;
		rBody.useGravity = true;
	}

	void Start()
	{
		turnSpeed = 100.0f;
	}

	void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			jumpFlag = true;
		}
	}
	

	void FixedUpdate()
	{
		inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		

		if (grounded)
		{
			var velocityChange = CalculateVelocityChange(inputVector);
			rBody.AddForce(velocityChange, ForceMode.VelocityChange);
			Rotate(inputVector.x, inputVector.z);

			if (canJump && jumpFlag)
			{
				jumpFlag = false;
				rBody.velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y + CalculateJumpVerticalSpeed(), GetComponent<Rigidbody>().velocity.z);
			}

			grounded = false;
		}
		else
		{
			var velocityChange = transform.TransformDirection(inputVector) * inAirControl;
			rBody.AddForce(velocityChange, ForceMode.VelocityChange);
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.transform == transform.parent)
		{
			transform.parent = null;
		}
	}

	void OnCollisionStay(Collision col)
	{
		TrackGrounded(col);
	}
	
	void OnCollisionEnter(Collision col)
	{
		TrackGrounded(col);
	}


	private Vector3 CalculateVelocityChange(Vector3 inputVector)
	{
		var relativeVelocity = Camera.main.transform.TransformDirection(inputVector);

		if (inputVector.z > 0)
		{

			relativeVelocity.z *= (canRun && Input.GetButton("Sprint")) ? runSpeed : walkSpeed;
		}
		else
		{
			relativeVelocity.z *= (canRun && Input.GetButton("Sprint")) ? runBackwardSpeed : walkBackwardSpeed;
		}
		relativeVelocity.x *= (canRunSidestep && Input.GetButton("Sprint")) ? runSidestepSpeed : sidestepSpeed;

		var currRelativeVelocity = GetComponent<Rigidbody>().velocity - groundVelocity;
		var velocityChange = relativeVelocity - currRelativeVelocity;
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = 0;
		
		return velocityChange;
	}

	private float CalculateJumpVerticalSpeed()
	{
		return Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(Physics.gravity.y));
	}

	private void TrackGrounded(Collision collision)
	{
		var maxHeight = capsule.bounds.min.y + capsule.radius * .9f;
		foreach (var contact in collision.contacts)
		{
			if (contact.point.y < maxHeight)
			{
				if (isKinematic(collision))
				{
					groundVelocity = collision.rigidbody.velocity;
					transform.parent = collision.transform;
				}
				else if (isStatic(collision))
				{
					transform.parent = collision.transform;
				}
				else
				{
					groundVelocity = Vector3.zero;
				}

				grounded = true;
			}
			
			break;
		}
	}
	
	private bool isKinematic(Collision collision)
	{
		return isKinematic(GetComponent<Collider>().transform);
	}
	
	private bool isKinematic(Transform transform)
	{
		return transform.GetComponent<Rigidbody>() && transform.GetComponent<Rigidbody>().isKinematic;
	}
	
	private bool isStatic(Collision collision)
	{
		return isStatic(collision.transform);
	}
	
	private bool isStatic(Transform transform)
	{
		return transform.gameObject.isStatic;
	}

	private void Rotate(float horizontal, float vertical)
	{
		if(inputVector != Vector3.zero)
		{
			inputVector = new Vector3(horizontal, 0.0f, vertical);
			inputVector = Camera.main.transform.TransformDirection (inputVector);
			Quaternion targetRotation = Quaternion.LookRotation(inputVector);
			Quaternion newRotation = Quaternion.Lerp(rBody.rotation, targetRotation, turnSpeed * Time.deltaTime);
			rBody.MoveRotation(newRotation);
		}

	}
}

