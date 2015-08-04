using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDHeartKey : MonoBehaviour {
	private PlayerInventory inventory;
	private Text hudText;
	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerInventory> ();
		hudText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		hudText.text = "HKey: " + inventory.HeartKeyOwned;
	}
}