using UnityEngine;
using System.Collections;

public class PickupHeartKey : MonoBehaviour {
	PlayerStatus status;

	// Use this for initialization
	void Start () {
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			status.CollectHeartKey();
			gameObject.SetActive(false);
		}
	}
}
