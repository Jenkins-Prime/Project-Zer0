using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {
	[SerializeField] int damage = 1;
	float hitDelay = 2.0f;
	float hitAllowed = 0f;
	PlayerStatus status;
	private Animator animator;

	// Use this for initialization
	void Awake () 
	{
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
		animator = GetComponent<Animator> ();
	}
	
	void OnCollisionStay(Collision other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			if(Time.fixedTime > hitAllowed) 
			{
				animator.SetBool("isHurt", true);
				status.IncreaseMaxHealth(damage);
				hitAllowed = Time.fixedTime + hitDelay;
			}
		}
	}
	
}
