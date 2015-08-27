using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour 
{
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	private bool grounded = false;
	private Rigidbody rigidBody;
	private Vector3 velocity;
	
	
	
	void Awake () 
	{
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.freezeRotation = true;
		rigidBody.useGravity = false;
	}
	
	void FixedUpdate () 
	{
		Move ();
	}
	
	void OnCollisionStay () 
	{
		grounded = true;    
	}
	
	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	private void Move()
	{
		if (grounded) 
		{
			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigidBody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
			
			// Jump
			if (canJump && Input.GetButton("Jump")) {
				rigidBody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}
		}
		
		// We apply gravity manually for more tuning control
		rigidBody.AddForce(new Vector3 (0, -gravity * rigidBody.mass, 0));
		
		grounded = false;
		
	}
}



