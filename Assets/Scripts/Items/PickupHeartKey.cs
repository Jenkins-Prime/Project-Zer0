using UnityEngine;
using System.Collections;

public class PickupHeartKey : MonoBehaviour {
	PlayerStatus status;
	PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			status.CollectHeartKey();
			playerHealth.IncreaseMaxHealth(1);
			gameObject.SetActive(false);
		}
	}
}
