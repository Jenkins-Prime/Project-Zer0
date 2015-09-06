using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {
	public float moveSpeed = 2f;
	public float rotationSpeed = 3f;
	public float sightRange = 5f;
	public float patrolRadius = 5f;
	public float chaseRange = 10f;
	
	Rigidbody rb;
	Animator anim;
	Transform player;
	List<Vector3> waypoints = new List<Vector3>();
	int currentPoint;
	float reachDist = 1.0f;
	bool enemySpotted;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//Add waypoints to patrol, Center/N/E/S/W
		waypoints.Add(transform.position); //Return position
		waypoints.Add(transform.position + transform.forward * patrolRadius);
		waypoints.Add(transform.position + transform.right * patrolRadius);
		waypoints.Add(transform.position - transform.forward * patrolRadius);
		waypoints.Add(transform.position - transform.right * patrolRadius);
		currentPoint = 1;
	}

	void Update() {
		if (Vector3.Distance (transform.position, player.position) < sightRange) {
			enemySpotted = true;
		} else {
			enemySpotted = false;
		}
		anim.SetBool ("enemySpotted", enemySpotted);

	}
	
	void FixedUpdate() {
		if (enemySpotted) {
			if(Vector3.Distance(transform.position, waypoints[0]) < chaseRange) {
				MoveAtTarget(player.position);
			} else if(Vector3.Distance(transform.position, waypoints[0]) > chaseRange / 2f) { //Return back to center
				MoveAtTarget(waypoints[0]);
			} else {
				enemySpotted = false;
			}
		} else { //Patrol
			if(Vector3.Distance(transform.position, waypoints[currentPoint]) > reachDist) {
				MoveAtTarget(waypoints[currentPoint]);
				anim.SetBool("walk", true);
			} else if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f) {
				currentPoint ++; //Go to next waypoint
				if(currentPoint > waypoints.Count - 1)
					currentPoint = 1;
			} 
			else {
				anim.SetBool("walk", false); //Keep idle-ing	
			}
		}
	}

	void MoveAtTarget(Vector3 target) {
		Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
		rb.velocity = transform.forward * moveSpeed;
	}

	void OnDrawGizmosSelected() {
		if (waypoints.Count > 0) { //To avoid annoying erros
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere (waypoints[0], 0.2f);
			Gizmos.DrawWireSphere (transform.position, sightRange);
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere (waypoints[1], 0.2f);
			Gizmos.DrawWireSphere (waypoints[2], 0.2f);
			Gizmos.DrawWireSphere (waypoints[3], 0.2f);
			Gizmos.DrawWireSphere (waypoints[4], 0.2f);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (waypoints[0], chaseRange);
		}
	}
}
