using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	[SerializeField] int health = 3;
	int maxHealth = 3;
	[SerializeField] int lives = 2;
	int maxLives = 10;
	[SerializeField] int ammo = 0;
	int maxAmmo = 20;
	
	[SerializeField] int sparkCount = 0;
	int totalSparks = 10;
	[SerializeField] bool[] sparkiesCollected = {false, false, false, false, false};
	[SerializeField] int scrapCount = 0;
	int totalScrap = 100;
	[SerializeField] int goldCoinCount = 0;
	int totalGoldCoins = 5;
	[SerializeField] bool heartKeyOwned = false;
	[SerializeField] bool heartCageUnlocked = false;

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
		if (health < 0) {
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
		/*
		if (lives <= 0) {
			//Do Gameover actions
			lives = 2;
		}*/
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

	public void CollectSparkie(int index) {
		sparkiesCollected[index] = true;
		bool sparkiesComplete = true;
		for(int i=0; i< 5; i++) {
			if(sparkiesCollected[i] == false) {
				sparkiesComplete = false;
				break;
			}
		}
		if (sparkiesComplete) {
			CollectSpark();
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
