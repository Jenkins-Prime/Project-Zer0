using UnityEngine;
using System.Collections;

public class PickupHeartCage : MonoBehaviour {
	PlayerInventory inventory;
	
	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			if(inventory.CollectHeartCage() == true)
				gameObject.SetActive(false);
		}
	}
}
