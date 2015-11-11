using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour {
    public enum EnemyType {
        Ground, Flying, Swimming
    }
    public EnemyType enemyType;

    public enum AttackType {
        None, Basic, Dive
    }
    public AttackType attackType;
    public int health;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
