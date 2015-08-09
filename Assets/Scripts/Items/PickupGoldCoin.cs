using UnityEngine;
using System.Collections;

public class PickupGoldCoin : MonoBehaviour {
	PlayerStatus status;

	// Use this for initialization
	void Start () {
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			status.CollectGoldCoin();
			gameObject.SetActive(false);
		}
	}
}
