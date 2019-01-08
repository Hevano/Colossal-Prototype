using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {
	public float destroyTime;

	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject,destroyTime);
	}
}
