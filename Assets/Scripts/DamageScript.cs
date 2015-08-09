using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {
	[SerializeField] int damage = 1;
	float hitDelay = 2.0f;
	float hitAllowed = 0f;
	PlayerStatus status;

	// Use this for initialization
	void Start () {
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
	}
	
	void OnCollisionStay(Collision other) {
		if (other.gameObject.tag == "Player") {
			if(Time.fixedTime > hitAllowed) {
				status.LoseHealth(damage);
				hitAllowed = Time.fixedTime + hitDelay;
			}
		}
	}
}
