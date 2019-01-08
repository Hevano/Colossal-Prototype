using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour {
	public bool[] p1_direction;
	public bool[] p2_direction;
	/*
	0 - left
	1 - right
	2 - forward
	3 - back
	*/
	public GameObject player1;
	public PlayerData p1;
	public Transform p1_meshTransform;
	public Animator p1_anim;
	public bool p1_moving = false;
	private float p1_attackMovement;
	public bool p1_attackMovementFlag;
	public bool p1_chargeFlag = false;
	public GameObject p1_chargeEffect;
	public bool p1_attacking = false;
	private AudioSource p1_AudioSource;

	public GameObject player2;
	public PlayerData p2;
	public Transform p2_meshTransform;
	public Animator p2_anim;
	public bool p2_moving = false;
	private float p2_attackMovement;
	public bool p2_attackMovementFlag;
	public bool p2_chargeFlag = false;
	public GameObject p2_chargeEffect;
	public bool p2_attacking = false;
	private AudioSource p2_AudioSource;

	public UIBarScript statusBar;

	private Vector3 direction;

	public GameObject[] hitEffects;

	public Text victoryMsg;
	public Text startMsg;

	private Vector3 p1_loc;
	private Vector3 p2_loc;

	void Start () 
	{
		p1 = player1.GetComponent<PlayerData>();
		p2 = player2.GetComponent<PlayerData>();

		p1_anim = player1.GetComponentInChildren<Animator>();
		p2_anim = player2.GetComponentInChildren<Animator>();

		p1_AudioSource = player1.GetComponentInChildren<AudioSource>();
		p2_AudioSource = player1.GetComponentInChildren<AudioSource>();

		statusBar = gameObject.GetComponent<UIBarScript>();
		statusBar.HealthChange(0,p1.health);
		statusBar.HealthChange(1,p2.health);
	}

	void FixedUpdate()
	{
		//Player Controls
		//Player 1 Movement
		if (!p1_attacking && !p1.superAttack.activeSelf && p1.stun < 1) {
			p1_anim.SetInteger("AnimStatus", 0);
			if (Input.GetKey("left")) {
				p1_anim.SetInteger("AnimStatus", 1);
				player1.transform.position += new Vector3(-p1.speed, 0, 0);
				player1.transform.eulerAngles = new Vector3(0, 270, 0);
			} else if (Input.GetKey("right")) {
				p1_anim.SetInteger("AnimStatus", 1);
				player1.transform.position += new Vector3(p1.speed, 0, 0);
				player1.transform.eulerAngles = new Vector3(0, 90, 0);
			}

			if (Input.GetKey("down")) {
				p1_anim.SetInteger("AnimStatus", 1);
				player1.transform.position += new Vector3(0,0, -p1.speed);
				player1.transform.eulerAngles = new Vector3(0, 180, 0);
			} else if (Input.GetKey("up")) {
				p1_anim.SetInteger("AnimStatus", 1);
				player1.transform.position += new Vector3(0, 0, p1.speed);
				player1.transform.eulerAngles = new Vector3(0, 0, 0);
			}
		}

		//Attack
		if(Input.GetKey(".") && p1.stun == 0 && !p1_attacking && !p1_chargeFlag) {
			p1_attacking = true;
			p1_anim.SetInteger("AnimStatus", 2);
			p1.lightAttack.SetActive(true);
			p1_AudioSource.clip = p1.audioList[1];
			p1_AudioSource.Play();
			p1_attackMovement = p1.lightAttack.GetComponent<AttackScript>().attackMovement;
		}

		if (Input.GetKeyDown("/") && !p1_attacking && p1.stun == 0){
			p1_chargeFlag = true;
		}

		if (Input.GetKeyUp("/") && !p1_attacking && p1.stun == 0) {
			p1_chargeEffect.SetActive(false);
			if(p1.charge < 90)
			{
				p1_chargeFlag = false;
				p1_attacking = true;
				p1_anim.SetInteger("AnimStatus", 3);
				p1.heavyAttack.SetActive(true);
				p1_AudioSource.clip = p1.audioList[3];
				p1_AudioSource.Play();
				p1_attackMovement = p1.heavyAttack.GetComponent<AttackScript>().attackMovement;
			}
			else
			{
				p1_anim.SetInteger("AnimStatus", 6);
				if(p1.superAttackType == 1)
				{
					p1_anim.SetInteger("AnimStatus", 0);
				}
				p1_chargeFlag = false;
				p1.superAttack.SetActive(true);
			}
		}

		//Player 2 Movement
		if (!p2_attacking && !p2.superAttack.activeSelf && p2.stun < 1) {
			print("yieks");
			
		}

		p2_anim.SetInteger("AnimStatus", 0);
		if (Input.GetKey("a")) {

			p2_anim.SetInteger("AnimStatus", 1);
			player2.transform.position += new Vector3(-p2.speed, 0, 0);
			player2.transform.eulerAngles = new Vector3(0, 270, 0);
		} else if (Input.GetKey("d")) {
			p2_anim.SetInteger("AnimStatus", 1);
			player2.transform.position += new Vector3(p2.speed, 0, 0);
			player2.transform.eulerAngles = new Vector3(0, 90, 0);
		}

		if (Input.GetKey("s")) {
			p2_anim.SetInteger("AnimStatus", 1);
			player2.transform.position += new Vector3(0,0, -p2.speed);
			player2.transform.eulerAngles = new Vector3(0, 180, 0);
		} else if (Input.GetKey("w")) {
			p2_anim.SetInteger("AnimStatus", 2);
			player2.transform.position += new Vector3(0, 0, p2.speed);
			player2.transform.eulerAngles = new Vector3(0, 0, 0);
		}

		//Player 2 Attack
		if(Input.GetKey("c") && p2.stun == 0 && !p2_attacking && !p2_chargeFlag) {
			p2_attacking = true;
			p2_anim.SetInteger("AnimStatus", 2);
			p2.lightAttack.SetActive(true);
			p2_AudioSource.clip = p2.audioList[1];
			p2_AudioSource.Play();
			p2_attackMovement = p2.lightAttack.GetComponent<AttackScript>().attackMovement;
		}

		if (Input.GetKeyDown("v") && !p2_attacking && p2.stun == 0){
			p2_chargeFlag = true;
		}

		if (Input.GetKeyUp("v") && !p2_attacking && p2.stun == 0) {
			p2_chargeEffect.SetActive(false);
			if(p2.charge < 90)
			{
				p2_chargeFlag = false;
				p2_attacking = true;
				p2_anim.SetInteger("AnimStatus", 3);
				p2.heavyAttack.SetActive(true);
				p2_AudioSource.clip = p2.audioList[3];
				p2_AudioSource.Play();
				p2_attackMovement = p2.heavyAttack.GetComponent<AttackScript>().attackMovement;
			}
			else
			{
				p2_anim.SetInteger("AnimStatus", 6);
				if(p2.superAttackType == 1)
				{
					p2_anim.SetInteger("AnimStatus", 0);
				}
				p2_chargeFlag = false;
				p2.superAttack.SetActive(true);
			}
		}

		//UI Message Management
		if(p1.health <= 0)
		{
			p1_anim.SetInteger("AnimStatus", 5);
			victoryMsg.text = "Player 2 Wins!";
		}
		if(p2.health <= 0)
		{
			p2_anim.SetInteger("AnimStatus", 5);
			victoryMsg.text = "Player 1 Wins!";
		}

		if(p1.health == 100 && p2.health == 100)
		{
			startMsg.text = "FIGHT!";
		}
		else if(p1.health + p2.health != 200)
		{
			startMsg.text = "";
		}

		if(p1_anim.GetInteger("AnimStatus") == 4)
		{
			p1_anim.SetInteger("AnimStatus", 0);
		}

		if(p2_anim.GetInteger("AnimStatus") == 4)
		{
			p2_anim.SetInteger("AnimStatus", 0);
		}

		//Reduces Stun Every Call of Fixed Update
		AdjustStun(p1,1);
		AdjustStun(p2,1);

		// Manages Charge, and the Effect of High Charge
		if(p1_chargeFlag)
		{
			p1.charge += 2;
			statusBar.ChargeChange(0,p1.charge);
		}
		else
		{
			p1.charge = 0;
			statusBar.ChargeChange(0,p1.charge);
		}
		if(p1.charge > 20)
		{
			p1_chargeEffect.SetActive(true);
			SuperAttackSpecialEffect(p1,player1,player2);
		}

		if(p2_chargeFlag)
		{
			p2.charge += 2;
			statusBar.ChargeChange(1,p2.charge);
		}
		else
		{
			p2.charge = 0;
			statusBar.ChargeChange(1,p2.charge);
		}
		if(p2.charge > 20)
		{
			p2_chargeEffect.SetActive(true);
			SuperAttackSpecialEffect(p2,player2,player1);
		}
		
		

		//Moves players that are attacking
		if(p1_attacking && p1_attackMovementFlag)
		{
			if(Mathf.Round(player1.transform.eulerAngles.y) == 270)
			{
				player1.transform.position += new Vector3(-p1_attackMovement, 0, 0);
				print("1");
			}
			if(Mathf.Round(player1.transform.eulerAngles.y) == 90)
			{
				player1.transform.position += new Vector3(p1_attackMovement, 0, 0);
				print("2");
			}
			if(Mathf.Round(player1.transform.eulerAngles.y) == 180)
			{
				player1.transform.position += new Vector3(0,0,-p1_attackMovement);
				print("3");
			}
			if(Mathf.Round(player1.transform.eulerAngles.y) == 0)
			{
				player1.transform.position += new Vector3(0,0,p1_attackMovement);
				print("4");
			}
		}
		if(!p2_attacking && !p2.superAttack.activeSelf)
		{
			if(p2_moving && p2.stun < 1)
			{
				p2_anim.SetInteger("AnimStatus", 1);
				if(p2_direction[0] == true)
				{
					player2.transform.position += new Vector3(-p1.speed, 0, 0);
					player2.transform.eulerAngles = new Vector3(0, 270, 0);
				}
				if(p2_direction[1] == true)
				{
					player2.transform.position += new Vector3(p1.speed, 0, 0);
					player2.transform.eulerAngles = new Vector3(0, 90, 0);
				}
				if(p2_direction[2] == true)
				{
					player2.transform.position += new Vector3(0,0,-p1.speed);
					player2.transform.eulerAngles = new Vector3(0, 180, 0);
				}
				if(p2_direction[3] == true)
				{
					player2.transform.position += new Vector3(0,0,p1.speed);
					player2.transform.eulerAngles = new Vector3(0, 0, 0);
				}
			}
		}
		if(p2_attacking && p2_attackMovementFlag)
		{
			if(Mathf.Round(player2.transform.eulerAngles.y) == 270)
			{
				player2.transform.position += new Vector3(-p2_attackMovement, 0, 0);
			}
			if(Mathf.Round(player2.transform.eulerAngles.y) == 90)
			{
				player2.transform.position += new Vector3(p2_attackMovement, 0, 0);
			}
			if(Mathf.Round(player2.transform.eulerAngles.y) == 180)
			{
				player2.transform.position += new Vector3(0,0,-p2_attackMovement);
			}
			if(Mathf.Round(player2.transform.eulerAngles.y) == 0)
			{
				player2.transform.position += new Vector3(0,0,p2_attackMovement);
			}
		}
	}

	public void Hit(int owner_int, int damage_int, float stun)
	{
		if(owner_int == 1)
		{
			if(damage_int == p2.lightAttack.GetComponent<AttackScript>().dmg)
			{
				p2_AudioSource.clip = p2.audioList[0];
				p2_AudioSource.Play();
			}
			else
			{
				p2_AudioSource.clip = p2.audioList[2];
				p2_AudioSource.Play();
			}

			p1.stun = stun;
			p1.health -= damage_int;
			statusBar.HealthChange(0,p1.health);
			p1_anim.SetInteger("AnimStatus", 4);
		}
		else
		{
			if(damage_int == p2.lightAttack.GetComponent<AttackScript>().dmg)
			{
				p1_AudioSource.clip = p1.audioList[0];
				p1_AudioSource.Play();
			}
			else
			{
				p1_AudioSource.clip = p1.audioList[2];
				p1_AudioSource.Play();
			}

			p2.stun = stun;
			p2.health -= damage_int;
			statusBar.HealthChange(1,p2.health);
			p2_anim.SetInteger("AnimStatus", 4);
		}
	}

	public void BuildingHit(int target, int damage)
	{
		if(target == 0 && p1.stun > 0)
		{
			p1.health -= damage;
			statusBar.HealthChange(0,p1.health);
		}
		else if(target == 1 && p2.stun > 0)
		{
			p2.health -= damage;
			statusBar.HealthChange(1,p2.health);
		}

	}

	public void EndAttack(int owner_int, float recoil)
	{
		if(owner_int == 1)
		{
			p2_attacking = false;
			p2_attackMovement = 0f;
			p2_anim.SetInteger("AnimStatus",0);
			p2.stun = recoil;
		}
		else
		{
			print("attack is ended!");
			p1_attacking = false;
			p1_attackMovement = 0f;
			p1_anim.SetInteger("AnimStatus",0);
			p1.stun = recoil;
		}
	}

	void AdjustStun (PlayerData player, int rate)
	{
		player.stun -= rate;
		if(player.stun < 0)
		{
			player.stun = 0;
		}
	}

	public void SuperAttackSpecialEffect(PlayerData player, GameObject playerObj, GameObject target)
	{
		if(player.superAttackType == 0)
		{
			playerObj.transform.rotation = Quaternion.Lerp(playerObj.transform.rotation, Quaternion.LookRotation(target.transform.position - playerObj.transform.position), 0.02f);
		}
		if(player.superAttackType == 1)
		{
			if(player == p1)
			{
				p1_anim.SetInteger("AnimStatus",6);
			}
			else
			{
				p2_anim.SetInteger("AnimStatus", 6);
			}
		}
	}
}
