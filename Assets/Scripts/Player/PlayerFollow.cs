using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour 
{
	private Transform player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void LateUpdate () 
	{
		
	}
}
