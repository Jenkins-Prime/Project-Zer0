using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {
	[SerializeField] int damage = 1;
	float hitDelay = 2.0f;
	float hitAllowed = 0f;
	PlayerHealth playerHealth;

	// Use this for initialization
	void Start () 
	{
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}
	
	void OnCollisionStay(Collision other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			if(Time.fixedTime > hitAllowed) 
			{
				playerHealth.LoseHealth(damage);
				hitAllowed = Time.fixedTime + hitDelay;
			}
		}
	}
}
