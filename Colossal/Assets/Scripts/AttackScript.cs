using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {
	public GameObject controller;
	public int dmg;
	public Vector3 aoe;
	public int playerOwner;
	public GameObject ownerObj;
	public int maxDuration;
	public float[] hitFrameDuration;
	public float currentDuration = 0;
	private bool hitreg = false;
	public float knockback;
	public float stun;
	public float recoil;
	public float attackMovement;
	private GameObject[] hitEffects;

	void Start()
	{
		hitEffects = controller.GetComponent<PlayerLogic>().hitEffects;
	}

	// Use this for initialization
	void OnEnable () 
	{
		currentDuration = 0;
		transform.localScale = Vector3.one;
		transform.localScale += aoe;
		hitreg = false;
		if(playerOwner == 0)
		{
			controller.GetComponent<PlayerLogic>().p1_attackMovementFlag = true;
		}
		else
		{
			controller.GetComponent<PlayerLogic>().p2_attackMovementFlag = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		print("something");
		currentDuration += 1;
		if (currentDuration > maxDuration)
		{
			gameObject.SetActive(false);
			print("what happen?");
			controller.gameObject.GetComponent<PlayerLogic>().EndAttack(playerOwner,0);
		}
		if (currentDuration > hitFrameDuration[1])
		{
			if(playerOwner == 0)
			{
				controller.GetComponent<PlayerLogic>().p1_attackMovementFlag = false;
			}
			else
			{
				controller.GetComponent<PlayerLogic>().p2_attackMovementFlag = false;
			}
		}
	}
	void OnTriggerStay(Collider other)
	{
		if(!hitreg && hitFrameDuration[1] > currentDuration && currentDuration > hitFrameDuration[0])
		{
			Instantiate(hitEffects[0],other.gameObject.transform.position,Quaternion.identity);
			if(other.tag == "Player")
			{
				hitreg = true;
				controller.gameObject.GetComponent<PlayerLogic>().Hit(playerOwner, dmg, stun);
				other.GetComponent<Rigidbody>().AddExplosionForce(knockback, ownerObj.transform.position, 0, 0, ForceMode.Impulse);
				controller.gameObject.GetComponent<PlayerLogic>().EndAttack(playerOwner, recoil);
				gameObject.SetActive(false);
			}
			if(other.tag == "Building")
			{
				other.gameObject.GetComponent<BuildingScript>().Destroy();
				controller.gameObject.GetComponent<PlayerLogic>().EndAttack(playerOwner, recoil);
				gameObject.SetActive(false);

			}
		}
	}
}
