using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour {
    [Header("Speed Settings:")]
    [SerializeField]
    float patrolSpeed = 1f;
    [SerializeField]
    float chaseSpeed = 2f;
    [SerializeField]
    float rotationSpeed = 3f;
    [Header("Movement Radius:")]
    [SerializeField]
    float patrolRadius = 5f;
    [SerializeField]
    float chaseRadius = 10f;
    [Header("Behavior Settings:")]
    [SerializeField]
    float sightRange = 5f;
    [SerializeField]
    float attackRange = 2f;

    public bool isAttacking;
    Rigidbody rb;
    Animator anim;
    Transform player;
    EnemyStatus eStatus;
    EnemyAttack eAttack;
    List<Vector3> waypoints = new List<Vector3>();
    int currentPoint;
    float reachDist = 1.0f;    
    bool pauseMotion;
    bool playerSpotted;    
    bool returnBack;
    enum EnemyState {
        Patrol,
        Chase,
        Attack,
        Return
    }
    EnemyState enemyState;

    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        eStatus = GetComponent<EnemyStatus>();
        eAttack = GetComponent<EnemyAttack>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Add waypoints to patrol, Center/N/E/S/W
        waypoints.Add(transform.position); //Return position
        waypoints.Add(transform.position + transform.forward * patrolRadius);
        waypoints.Add(transform.position + transform.right * patrolRadius);
        waypoints.Add(transform.position - transform.forward * patrolRadius);
        waypoints.Add(transform.position - transform.right * patrolRadius);
        currentPoint = 1;
        pauseMotion = true;
        playerSpotted = false;
        isAttacking = false;
        returnBack = false;        
    }

    void FixedUpdate () {        
        if (!pauseMotion) {
            if (!isAttacking) {
                GetEnemyState();
            }

            //move
            switch (enemyState) {
                case EnemyState.Patrol:
                    MoveAtTarget(waypoints[currentPoint], patrolSpeed);
                    break;
                case EnemyState.Chase:                    
                    MoveAtTarget(player.position, chaseSpeed);
                    break;
                case EnemyState.Attack:                    
                    eAttack.Attack(player.position);
                    break;
                case EnemyState.Return:
                    MoveAtTarget(waypoints[0], patrolSpeed);
                    break;
            }

            anim.SetBool("PlayerSpotted", playerSpotted);
        }
    }

    void GetEnemyState() {
        float distFromCenter = Vector3.Distance(transform.position, waypoints[0]);
        float distFromPlayer = Vector3.Distance(transform.position, player.position);

        if (distFromCenter < chaseRadius && !returnBack) {            
            if (distFromPlayer < attackRange && eStatus.attackType != EnemyStatus.AttackType.None) {                
                enemyState = EnemyState.Attack;
                isAttacking = true;
                //pauseMotion = true;
                anim.SetTrigger("Attack");
            }
            else if (distFromPlayer < sightRange) {                
                playerSpotted = true;
                enemyState = EnemyState.Chase;
            }
            else {
                playerSpotted = false;
                Patrol();
            }
        }
        else if(Vector3.Distance(player.position, waypoints[0]) < chaseRadius && distFromPlayer < sightRange) {
            playerSpotted = true;
            enemyState = EnemyState.Chase;
        } else {
            playerSpotted = false;
            returnBack = true;
            enemyState = EnemyState.Return;
            ReturnBack();           
        }
    }

    void Patrol() {
        if (Vector3.Distance(transform.position, waypoints[currentPoint]) > reachDist) {
            enemyState = EnemyState.Patrol;
            anim.SetBool("IsMoving", true);
        }
        else {            
            pauseMotion = true;
            anim.SetBool("IsMoving", false);
            currentPoint++; //Go to next waypoint
            if (currentPoint > waypoints.Count - 1) {
                currentPoint = 1;
            }
        }
    }

    void ReturnBack() {
        if (Vector3.Distance(transform.position, waypoints[0]) < chaseRadius - sightRange) {
            returnBack = false;
        }
        else {            
            MoveAtTarget(waypoints[0], patrolSpeed);
        }
    }

    void MoveAtTarget(Vector3 target, float speed) {
        Vector3 targetDir = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        rb.velocity =  transform.forward * speed;
    }
    
    void PauseMotion(int paused) { //Animation Event function
        if (paused == 0)
            pauseMotion = false;
        else if (paused == 1)
            pauseMotion = true;        
    }

    void TriggerAttack(int attack) { //Animation Event function
        if (attack == 0)
            isAttacking = false;
        else
            isAttacking = true;
    }

    void OnDrawGizmosSelected() {
        if (waypoints.Count > 0) { //To avoid annoying erros
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(waypoints[0], 0.2f);
            Gizmos.DrawWireSphere(transform.position, sightRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(waypoints[1], 0.2f);
            Gizmos.DrawWireSphere(waypoints[2], 0.2f);
            Gizmos.DrawWireSphere(waypoints[3], 0.2f);
            Gizmos.DrawWireSphere(waypoints[4], 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(waypoints[0], chaseRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
