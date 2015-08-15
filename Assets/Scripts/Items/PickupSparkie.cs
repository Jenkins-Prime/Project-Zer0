using UnityEngine;
using System.Collections;

public class PickupSparkie : MonoBehaviour {
	PlayerStatus status;
	[SerializeField] int sparkieIndex;

	// Use this for initialization
	void Start () {
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			status.CollectSparkie(sparkieIndex);
			gameObject.SetActive(false);
		}
	}
}
