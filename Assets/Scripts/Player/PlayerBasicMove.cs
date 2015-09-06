using UnityEngine;
using System.Collections;

public class PlayerBasicMove : MonoBehaviour {
	public float moveSpeed = 12f;
	public float turnSpeed = 100f;

	Rigidbody rb;
	Animator anim;
	Quaternion targetRotation;
	float forwardInput;
	float turnInput;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		targetRotation = transform.rotation;
		forwardInput = 0;
		turnInput = 0;
	}

	void Update() {
		forwardInput = Input.GetAxis("Vertical");
		if (forwardInput > 0.2f) {
			anim.SetFloat ("speed", 1f);
		} else {
			anim.SetFloat ("speed", 0f);
		}
		turnInput = Input.GetAxis("Horizontal");
		Turn ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move ();
	}

	void Move() {
		rb.velocity = transform.forward * forwardInput * moveSpeed;
	}

	void Turn() {
		targetRotation *= Quaternion.AngleAxis (turnSpeed * turnInput * Time.deltaTime, Vector3.up);
		transform.rotation = targetRotation;
	}
}
