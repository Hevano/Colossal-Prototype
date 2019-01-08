using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	public GameObject controller;
	public int playerNumber;
	public AudioClip[] audioList;
	public float speed;
	public int charge;
	public float stun;
	public GameObject lightAttack;
	public GameObject heavyAttack;
	public GameObject superAttack;
	public float health;
	public int superAttackType;
	

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Building")
		{
			controller.GetComponent<PlayerLogic>().BuildingHit(playerNumber,other.GetComponent<BuildingScript>().playerDamage);
			other.GetComponent<BuildingScript>().Destroy();
		}
	}
}
