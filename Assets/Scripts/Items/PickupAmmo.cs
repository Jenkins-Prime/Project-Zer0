using UnityEngine;
using System.Collections;

public class PickupAmmo : MonoBehaviour {
	PlayerStatus status;
	[SerializeField] int amount = 1;

	// Use this for initialization
	void Start () {
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			if(status.GainAmmo(amount) == true) {
				Invoke("ReActivate", 5);
				gameObject.SetActive(false);
			}
		}
	}
	
	void ReActivate() {
		gameObject.SetActive (true);
	}
}
