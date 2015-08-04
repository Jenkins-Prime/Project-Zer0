using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour {

	private int health = 3;
	private int maxHealth = 3; //Import from PlayerStats script
	private int lives = 2; //Import from PlayerStats script?
	private int maxLives = 10; //Import from PlayerStats script
	private int ammo = 0; //Import from PlayerStats script
	private int maxAmmo = 20; //Import from PlayerStats script

	//Sparks
	private int sparkCount = 0;  //Import from PlayerStats script for this level
	private int totalSparks = 10; //Import from PlayerStats script for this level
	//Scrap
	private int scrapCount = 0;  //Import from PlayerStats script for this level
	private int totalScrap = 100; //Import from PlayerStats script for this level
	//Gold Coins
	private int goldCoinCount = 0;  //Import from PlayerStats script for this level
	private int totalGoldCoins = 5; //Import from PlayerStats script for this level
	//Heart Key - Heart Cage
	private bool heartKeyOwned = false;  //Import from PlayerStats script for this level
	private bool heartCageUnlocked = false;  //Import from PlayerStats script for this level


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool GainHealth(int amount) {
		if (health == maxHealth) {
			return false;
		} else {
			health += amount;
			if (health >= maxHealth) {
				health = maxHealth;
			}
			//Do gui stuff, play sound, etc actions
			return true;
		}
	}

	public void LoseHealth(int amount) {
		health -= amount;
		if (health <= 0) {
			LoseLife();
		}
	}

	public void IncreaseMaxHealth() {
		maxHealth++;
		health = maxHealth;
		//Do GUI stuff, animation
	}

	public bool GainLife(int amount) {
		if (lives == maxLives) {
			return false;
		} else {
			lives += amount;
			if (lives >= maxLives) {
				lives = maxLives;
			}
			//Do GUI stuff, play sound, etc actions
			return true;
		}
	}

	public void LoseLife() {
		lives--;
		//Die
		if (lives <= 0) {
			//Do Gameover actions
			lives = 2;
		}
		//Reset Stuff
	}

	public bool GainAmmo(int amount) {
		if (ammo == maxAmmo) {
			return false;
		} else {
			ammo += amount;
			if (ammo >= maxAmmo) {
				ammo = maxAmmo;
			}
			//Do GUI stuff, play sound, etc actions
			return true;
		}
	}

	public void LoseAmmo(int amount) {
		ammo -= amount;
		if (ammo < 0) {
			ammo = 0;
		}
	}

	public void CollectSpark() {
		sparkCount++;
		//Do GUI stuff, animation
		if (sparkCount == totalSparks) {
			//Do GUI stuff, animation for completed Sparks
		}
	}

	public void CollectScrap() {
		scrapCount++;
		//Do GUI stuff, animation
		if (scrapCount == totalScrap) {
			//Do GUI stuff, animation for completed Scrap
		}
	}

	public void CollectGoldCoin() {
		goldCoinCount++;
		//Do GUI stuff, animation
		if (goldCoinCount == totalGoldCoins) { //IF NECESSARY
			//Do GUI stuff, animation for completed GoldCoins
		}
	}

	public void CollectHeartKey() {
		heartKeyOwned = true;
		//Do GUI stuff, animation
	}

	public bool CollectHeartCage() {
		if (heartKeyOwned == true) {
			heartKeyOwned = false;
			heartCageUnlocked = true;

			IncreaseMaxHealth ();
			return true;
		}
		return false;
	}

	//==========Getter Functions===========
	public int Health {
		get {
			return health;
		}
	}

	public int MaxHealth {
		get {
			return maxHealth;
		}
	}

	public int Lives {
		get {
			return lives;
		}
	}

	public int Ammo {
		get {
			return ammo;
		}
	}

	public int MaxAmmo {
		get {
			return maxAmmo;
		}
	}

	public int SparkCount {
		get {
			return sparkCount;
		}
	}

	public int ScrapCount {
		get {
			return scrapCount;
		}
	}

	public int GoldCoinCount {
		get {
			return goldCoinCount;
		}
	}

	public bool HeartKeyOwned {
		get {
			return heartKeyOwned;
		}
	}

	public bool HeartCageUnlocked {
		get {
			return heartCageUnlocked;
		}
	}
}
