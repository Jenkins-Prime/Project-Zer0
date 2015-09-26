using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {
    [Header("Speed Settings:")]
	public float patrolSpeed = 1f;
	public float chaseSpeed = 2f;
	public float rotationSpeed = 3f;
    [Header("Movement Radius:")]
    public float patrolRadius = 5f;
    public float chaseRadius = 10f;
    [Header("Behavior Settings:")]
    public float sightRange = 5f;		
    public float attackRange = 2f;
	
	Rigidbody rb;
	Animator anim;
	Transform player;
	List<Vector3> waypoints = new List<Vector3>();
	int currentPoint;
	float reachDist = 1.0f;
	bool pauseMotion;
	bool playerSpotted;
	bool returnBack;

	void Awake() {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
	}

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//Add waypoints to patrol, Center/N/E/S/W
		waypoints.Add(transform.position); //Return position
		waypoints.Add(transform.position + transform.forward * patrolRadius);
		waypoints.Add(transform.position + transform.right * patrolRadius);
		waypoints.Add(transform.position - transform.forward * patrolRadius);
		waypoints.Add(transform.position - transform.right * patrolRadius);
		currentPoint = 1;
		pauseMotion = true;
		playerSpotted = false;
		returnBack = false;
	}

	void FixedUpdate() {
		if (!pauseMotion) {
            float distFromCenter = Vector3.Distance(transform.position, waypoints[0]);
            float distFromPlayer = Vector3.Distance(transform.position, player.position);

            if (returnBack) {
                ReturnBack();
            } else {
				if (distFromCenter < chaseRadius) {
                    if (distFromPlayer < attackRange) {
                        Attack();
                    } else if (distFromPlayer < sightRange) {
                        playerSpotted = true;
                        MoveAtTarget(player.position, chaseSpeed);						
					} else {
                        playerSpotted = false;
                        Patrol();						
					}
				} else {					
					playerSpotted = false;
					returnBack = true;
                    ReturnBack();
				}
			}
			anim.SetBool ("PlayerSpotted", playerSpotted);
		}
	}
	
	void Patrol() {
		if (Vector3.Distance (transform.position, waypoints [currentPoint]) > reachDist) {
			MoveAtTarget (waypoints [currentPoint], patrolSpeed);
			anim.SetBool ("IsMoving", true);
		} else {
			pauseMotion = true;
			anim.SetBool ("IsMoving", false);
			currentPoint ++; //Go to next waypoint
			if (currentPoint > waypoints.Count - 1) {
				currentPoint = 1;
			}
		}
	}

    void ReturnBack() {
        if (Vector3.Distance(transform.position, waypoints[0]) < chaseRadius - sightRange) {
            returnBack = false;
        } else {
            MoveAtTarget(waypoints[0], patrolSpeed);
        }
    }

    void Attack() {
       // pauseMotion = true;
        anim.SetTrigger("Attack");
        //Dive
        rb.velocity = (transform.forward * 2 * chaseSpeed) + (transform.up * 3); //Change this
    }

	void MoveAtTarget(Vector3 target, float speed) {
		Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
		rb.velocity = transform.forward * speed;
	}

	void PauseMotion(int paused) {
		if (paused == 0)
			pauseMotion = false;
		else
			pauseMotion = true;
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
			Gizmos.DrawWireSphere (waypoints[0], chaseRadius);
		}
	}
}
