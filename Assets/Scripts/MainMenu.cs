using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	private UIManager manager;
	private bool isPaused;
	
	void Awake () 
	{
		manager = GameObject.FindGameObjectWithTag ("UIManager").GetComponent<UIManager> ();
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			isPaused = !isPaused;

			if(isPaused)
			{
				manager.ShowMenu();
			}
			else
			{
				manager.HideMenu();
			}


		}
	}
}
