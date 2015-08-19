using UnityEngine;
using System.Collections;

public class PickupHeart : MonoBehaviour {
	PlayerHealth playerHealth;
	[SerializeField] int amount = 1;

	// Use this for initialization
	void Start () {
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			if(playerHealth.GainHealth(amount)) {
				Invoke("ReActivate", 5);
				gameObject.SetActive(false);
			}
		}
	}

	void ReActivate() {
		gameObject.SetActive (true);
	}
}
