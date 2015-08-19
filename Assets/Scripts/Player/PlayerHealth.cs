using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour 
{
	public Image[] hearts;
	
	private int currentHearts;
	private int maxHearts;

	// Use this for initialization
	void Start () 
	{
		currentHearts = maxHearts = 3;
	}

	public bool GainHealth(int amount) 
	{
		currentHearts += amount;
		
		if(currentHearts >= maxHearts)
		{
			currentHearts = maxHearts;
		}
		
		for(int cnt = 0; cnt < currentHearts; cnt++)
		{
			hearts [cnt].enabled = true;
		}
		
		//Do gui stuff, play sound, etc actions
		return true;
	}
	
	public void LoseHealth(int amount) 
	{
		currentHearts-= amount;
		
		if(currentHearts < 0)
		{
			currentHearts = 0;
		}
		int temp = currentHearts + amount;
		for(int cnt = currentHearts; cnt < temp; cnt++)
		{
			hearts [cnt].enabled = false;
		}
	}
	
	public void IncreaseMaxHealth(int amount) 
	{
		maxHearts += amount;
		
		if(maxHearts >= hearts.Length)
		{
			maxHearts = hearts.Length;
		}
		currentHearts = maxHearts;
		for(int cnt = 0; cnt < currentHearts; cnt++)
		{
			hearts [cnt].enabled = true;
			hearts[cnt].transform.parent.gameObject.SetActive(true);
		}
	}
}
