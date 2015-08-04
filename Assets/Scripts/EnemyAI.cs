using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public Vector3 returnPosition;
	public float moveSpeed = 0.7f;
	public float rotationSpeed = 3.0f;
	public float maxChaseDistance = 30.0f;

	Transform target;
	Quaternion targetRotation;
	bool chasing;
	
	void Awake () {
		returnPosition = transform.position;
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void FixedUpdate () {
		if (chasing) {
			if(Vector3.Distance(transform.position,returnPosition) > maxChaseDistance) {
				chasing = false;
			} else {
			Debug.DrawLine (target.position, transform.position, Color.yellow);

			Rotate (target.position);
			Move (target.position);
			}
		} else {
			Debug.DrawLine (returnPosition, transform.position, Color.yellow);
			
			Rotate (returnPosition);
			Move (returnPosition);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			chasing = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			chasing = false;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (returnPosition, maxChaseDistance);
	}

	void Rotate(Vector3 targetPos) {
		targetRotation = Quaternion.LookRotation(targetPos - transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	void Move(Vector3 targetPos) {
		if (Quaternion.Angle (targetRotation, transform.rotation) < 10) {
			transform.position = Vector3.Lerp (transform.position, targetPos, moveSpeed * Time.deltaTime);
		}
	}

}
