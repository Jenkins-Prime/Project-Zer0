using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour 
{
	public GameObject coinUI;
	public GameObject sparkUI;
	public GameObject scrapUI;
	public GameObject ammoUI;
	public GameObject menu;

	void Start()
	{
		coinUI.SetActive(false);
		sparkUI.SetActive(false);
		scrapUI.SetActive (false);
		ammoUI.SetActive (false);
		menu.SetActive(false);

	}

	public void ShowCoin()
	{
		coinUI.SetActive (true);
	}

	public void HideCoin()
	{
		coinUI.SetActive (false);
	}

	public void ShowSpark()
	{
		sparkUI.SetActive (true);
	}
	
	public void HideSpark()
	{
		sparkUI.SetActive (false);
	}

	public void ShowScrap()
	{
		scrapUI.SetActive (true);
	}
	
	public void HideScrap()
	{
		scrapUI.SetActive (false);
	}

	public void ShowAmmo()
	{
		ammoUI.SetActive (true);
	}
	
	public void HideAmmo()
	{
		ammoUI.SetActive (false);
	}

	public void ShowMenu()
	{
		menu.SetActive (true);
		coinUI.SetActive(true);
		sparkUI.SetActive(true);
		scrapUI.SetActive (true);
		ammoUI.SetActive (true);
	}
	
	public void HideMenu()
	{
		menu.SetActive (false);
		coinUI.SetActive(false);
		sparkUI.SetActive(false);
		scrapUI.SetActive (false);
		ammoUI.SetActive (false);
	}
}
