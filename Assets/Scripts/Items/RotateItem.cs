using UnityEngine;
using System.Collections;

public class RotateItem : MonoBehaviour 
{
	public float rotateSpeed;

	void Start()
	{
		rotateSpeed = 100.0f;
	}

	void Update () 
	{
		transform.RotateAround (transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
	}
}
