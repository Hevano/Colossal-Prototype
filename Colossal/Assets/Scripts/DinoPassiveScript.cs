using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoPassiveScript : MonoBehaviour {

	public GameObject heavyAttack;

	void Update () 
	{
		if(heavyAttack.activeSelf)
		{
			gameObject.layer = 11;
		}
		else
		{
			gameObject.layer = 9;

		}	
	}
}
