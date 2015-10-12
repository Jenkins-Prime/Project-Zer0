using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
    [SerializeField]
    float attackSpeed = 3f;

    Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Attack(Vector3 target) {
        Vector3 direction = Vector3.zero;

        direction = target - transform.position;
        rb.velocity = direction.normalized * attackSpeed * 2;
        Debug.Log(rb.velocity);
    }
}
