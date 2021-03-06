﻿using UnityEngine;
using System.Collections;

public class PickupHeart : MonoBehaviour {
	PlayerStatus status;
	[SerializeField] int amount = 1;

	// Use this for initialization
	void Start () {
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
	}
	
	void OnTriggerEnter (Collider other) 
	{
		if (other.tag == "Player") 
		{
			if(status.GainHealth(amount)) 
			{
				gameObject.SetActive(false);
				Invoke("ReActivate", 5);
			}
		}
	}

	void ReActivate() {
		gameObject.SetActive (true);
	}
}
