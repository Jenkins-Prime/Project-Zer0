using UnityEngine;
using System.Collections;

public class PickupHeartCage : MonoBehaviour {
	PlayerStatus status;

	// Use this for initialization
	void Start () {
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			if(status.CollectHeartCage() == true)
				gameObject.SetActive(false);
		}
	}
}
