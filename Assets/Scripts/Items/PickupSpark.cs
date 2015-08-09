using UnityEngine;
using System.Collections;

public class PickupSpark : MonoBehaviour {
	PlayerStatus status;

	// Use this for initialization
	void Start () {
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			status.CollectSpark();
			gameObject.SetActive(false);
		}
	}
}
