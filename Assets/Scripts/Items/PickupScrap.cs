using UnityEngine;
using System.Collections;

public class PickupScrap : MonoBehaviour {
	PlayerInventory inventory;

	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			inventory.CollectScrap();
			gameObject.SetActive(false);
		}
	}
}
