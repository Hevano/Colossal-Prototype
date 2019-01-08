using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSuperScript : MonoBehaviour {

	public GameObject fireEffect;
	private int currentDuration;
	public int maxDurration;
	private bool hitreg;
	public int playerOwner;
	public GameObject controller;
	public float damageRate;
	public float recoil;
	public float stun;
	public GameObject hitEffect;
	
	void OnEnable () 
	{
		currentDuration = 0;
		fireEffect.SetActive(true);
		hitreg = false;
	}

	void FixedUpdate () 
	{
		
		currentDuration += 1;
		if(hitreg)
		{
			if(playerOwner == 0)
			{
				controller.GetComponent<PlayerLogic>().p2.stun = 2;
				controller.GetComponent<PlayerLogic>().p2.health -= damageRate;
				controller.GetComponent<PlayerLogic>().statusBar.HealthChange(1,controller.GetComponent<PlayerLogic>().p2.health);
			}
			else
			{
				controller.GetComponent<PlayerLogic>().p1.stun = 2;
				controller.GetComponent<PlayerLogic>().p1.health -= damageRate;
				controller.GetComponent<PlayerLogic>().statusBar.HealthChange(1,controller.GetComponent<PlayerLogic>().p1.health);
			}
		}
		if(currentDuration > maxDurration)
		{
			if(hitreg)
			{
				if(playerOwner == 0)
				{
					controller.GetComponent<PlayerLogic>().p2.stun = stun;
				}
				else
				{
					controller.GetComponent<PlayerLogic>().p1.stun = stun;
				}
				controller.GetComponent<PlayerLogic>().EndAttack(playerOwner,recoil);
			}
			fireEffect.SetActive(false);
			gameObject.SetActive(false);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			hitreg = true;
			Instantiate(hitEffect, other.transform.position, Quaternion.Euler(new Vector3(270, 0, 0)));
		}
		if(other.tag == "Building")
		{
			other.gameObject.GetComponent<BuildingScript>().Destroy();
		}
	}
}
