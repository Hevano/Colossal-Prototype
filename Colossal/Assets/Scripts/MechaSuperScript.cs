using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaSuperScript : MonoBehaviour {

	public Transform home;
	public float[] speed;
	public int playerOwner;
	private Collider ownerCollider;
	public GameObject controller;
	public GameObject hitEffect;
	private int currentDuration = 0;
	public int maxDuration;
	public float knockback;
	public int dmg;
	public int stun;
	public GameObject lineObject;
	private LineRenderer line;
	public int delay;
	public GameObject impactEffect;
	private bool hitreg;
	private Transform playerTarget;

	private Transform hitTarget;

	void OnDisable()
	{
		if(playerOwner == 0)
		{		
			controller.GetComponent<PlayerLogic>().p1_anim.SetInteger("AnimStatus", 0);
		}
		else
		{
			controller.GetComponent<PlayerLogic>().p2_anim.SetInteger("AnimStatus", 0);
		}
	}

	void OnEnable () 
	{
		currentDuration = 0;
		transform.position = home.position;
		hitreg= false;
		line = lineObject.GetComponent<LineRenderer>();
		line.SetPosition(0,transform.position);
		line.SetPosition(1,transform.position);
		impactEffect.SetActive(false);
		if(playerOwner == 0)
		{
			ownerCollider = controller.GetComponent<PlayerLogic>().player1.GetComponent<BoxCollider>();
			playerTarget = controller.GetComponent<PlayerLogic>().player2.transform;
		}
		else
		{
			ownerCollider = controller.GetComponent<PlayerLogic>().player2.GetComponent<BoxCollider>();
			playerTarget = controller.GetComponent<PlayerLogic>().player1.transform;
		}
		 Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), ownerCollider,true);
	}
	
	void FixedUpdate () 
	{
		currentDuration++;
		if(currentDuration > delay)
		{
			line.SetPosition(0,transform.position);
			if(!hitreg)
			{
				transform.position += transform.forward * speed[0];
				if(transform.position.y > 3)
				{
					transform.position += Vector3.down;
				}
				else
				{
					impactEffect.SetActive(true);
				}
			}
			else
			{
				transform.position += transform.up * speed[1];
				transform.position += transform.forward * speed[1];
				impactEffect.transform.eulerAngles = new Vector3(0,270,0);
				if(playerOwner == 0)
				{
					controller.GetComponent<PlayerLogic>().p2.stun = 2;
				}
				else
				{
					controller.GetComponent<PlayerLogic>().p1.stun = 2;
				}

				if(transform.position.y > 5)
				{
					PlayerHit();
				}
			}
		}
		if(currentDuration > maxDuration)
		{
			gameObject.SetActive(false);
			controller.GetComponent<PlayerLogic>().p1_anim.SetInteger("AnimStatus", 0);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			hitreg = true;
		}
		if(other.tag == "Building")
		{
			other.gameObject.GetComponent<BuildingScript>().Destroy();
		}
	}
	void PlayerHit()
	{
		controller.gameObject.GetComponent<PlayerLogic>().Hit(playerOwner, dmg, stun);
		playerTarget.GetComponent<Rigidbody>().AddExplosionForce(knockback, home.position, 0);
		Instantiate(hitEffect, transform.position, Quaternion.identity);
		gameObject.SetActive(false);
	}
}
