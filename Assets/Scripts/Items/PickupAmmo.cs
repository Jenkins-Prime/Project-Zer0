using UnityEngine;
using System.Collections;

public class PickupAmmo : MonoBehaviour {
	PlayerInventory inventory;
	[SerializeField]
	public int amount = 1;
	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			if(inventory.GainAmmo(amount) == true) {
				Invoke("ReActivate", 5);
				gameObject.SetActive(false);
			}
		}
	}
	
	void ReActivate() {
		gameObject.SetActive (true);
	}
}
