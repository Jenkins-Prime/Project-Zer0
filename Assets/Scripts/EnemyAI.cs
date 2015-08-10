using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	[SerializeField] Vector3 returnPosition;
	[SerializeField] Vector3 target;
	[SerializeField] float moveSpeed = 0.7f;
	//[SerializeField] float rotationSpeed = 3.0f;
	//[SerializeField] float maxChaseDistance = 7.0f;
	
	SphereCollider col;
	bool isChasing;

	void Start () {
		isChasing = false;
		returnPosition = transform.position;
		target = returnPosition;
		col = GetComponent<SphereCollider> ();

	}

	void FixedUpdate() {
		if (isChasing) {
			//Go to player
			transform.LookAt(target);
			transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
		} else {
			//Go back
			transform.LookAt(returnPosition);
			transform.position = Vector3.Lerp(transform.position, returnPosition, moveSpeed * Time.deltaTime);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			isChasing = true;
			Vector3 direction = other.transform.position - transform.position;
			RaycastHit hit;
			if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius)) {
				if(hit.collider.tag == "Player") {
					target = hit.transform.position + transform.up;
				}
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			isChasing = false;
		}
	}

/* === OLD CODE (COULD BE USEFUL LATER?) ===]
	Transform target //target is the Player

	void FixedUpdate () {
		if (chasing) {
			if(Vector3.Distance(transform.position,returnPosition) > maxChaseDistance) {
				chasing = false;
			} else {
			Debug.DrawLine (target.position, transform.position, Color.yellow); //sada

			Rotate (target.position);
			Move (target.position);
			}
		} else {
			Debug.DrawLine (returnPosition, transform.position, Color.yellow);
			
			Rotate (returnPosition);
			Move (returnPosition);
		}
	}

	void Rotate(Vector3 targetPos) {
		targetPos = new Vector3 (targetPos.x, transform.position.y, targetPos.z);
		targetRotation = Quaternion.LookRotation(targetPos - transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	void Move(Vector3 targetPos) {
		if (Quaternion.Angle (targetRotation, transform.rotation) < 10) {
			transform.position = Vector3.Lerp (transform.position, targetPos, moveSpeed * Time.deltaTime);
		}
	} === END OF OLD CODE ===*/

}
