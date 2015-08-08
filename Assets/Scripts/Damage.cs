using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	[SerializeField] int damage = 1;
	float hitDelay = 2.0f;
	float hitAllowed = 0f;
	PlayerInventory inv;

	// Use this for initialization
	void Start () {
		inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
	}

	void OnCollisionStay(Collision other) {
		if (other.gameObject.tag == "Player") {
			if(Time.fixedTime > hitAllowed) {
				inv.LoseHealth(damage);
				hitAllowed = Time.fixedTime + hitDelay;
			}
		}
	}
}
