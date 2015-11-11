using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
    [SerializeField]
    float attackSpeed = 3f;
    [SerializeField]
    int damage = 1;

    Rigidbody rb;
    EnemyStatus status;
    EnemyMovement eMovement;
    Transform player;
    PlayerStatus pStatus;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        status = GetComponent<EnemyStatus>();
        eMovement = GetComponent<EnemyMovement>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }
		
	void OnTriggerEnter (Collider other) { //Add timeToAttack
	    if(other.tag == "Player") { //Check if player is attacking to recieve damage
            Attack(player.position);
        }
	}

    public void Attack(Vector3 target) {
        switch (status.attackType) {
            case EnemyStatus.AttackType.None:
                DamagePlayer(damage);
                break;
            case EnemyStatus.AttackType.Basic:
                AttackBasic();                
                break;
            case EnemyStatus.AttackType.Dive:
                Dive(target);
                break;
        }
    }

    void AttackBasic() {
        //When the attack is done damage the player
        DamagePlayer(damage);
    }
    
    void Dive(Vector3 target) {
        if(eMovement.isAttacking) {
            Vector3 direction = target - transform.position;
            rb.velocity = direction.normalized * attackSpeed;
        }
    }

    void DamagePlayer(int dmg) {
        //Add check if player is dead? Or earlier
        pStatus.LoseHealth(dmg);
    }
}
