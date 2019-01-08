using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRandom : MonoBehaviour {
	public float maxDegree;
	public float minDegree;

	void Start () 
	{
		transform.localRotation = Quaternion.Euler(Random.Range(minDegree,maxDegree),Random.Range(0f,360f),Random.Range(minDegree,maxDegree));
	}
	
}
