using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour {

	public int playerDamage;
	public AudioSource player;
	public AudioClip explodeSound;
	private Vector3 explosionLocation;
	private GameObject explosion;
	private bool destructionFlag;

	void Start()
	{
		player = GetComponent<AudioSource>();
		player.clip = explodeSound;
		explosionLocation = transform.position;
		explosion = Resources.Load("Particles/Explosion1", typeof(GameObject)) as GameObject;
		
	}

	void FixedUpdate()
	{
		if(destructionFlag)
		{
			transform.position += new Vector3(0f,-0.2f,0f);
			if(transform.position.y < -30)
			{
				Destroy(gameObject);
			}
		}
	}
	public void Destroy()
	{
		player.Play();
		Instantiate(explosion,explosionLocation,Quaternion.identity);
		destructionFlag = true;
	}
}
