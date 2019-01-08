using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionScript : MonoBehaviour {

	public GameObject controller;
	public GameObject notACamera;
	private GameObject player1;
	private GameObject player2;

	// Use this for initialization
	void Start () 
	{
		player1 = controller.GetComponent<PlayerLogic>().player1;
		player2 = controller.GetComponent<PlayerLogic>().player2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = (player1.transform.position+player2.transform.position)/2;
		notACamera.transform.localPosition = new Vector3(0,15 + (Mathf.Pow(Vector3.Distance(player1.transform.position, player2.transform.position), 2)/ 40),(-25 - Vector3.Distance(player1.transform.position, player2.transform.position)) / 4);
		notACamera.transform.LookAt(gameObject.transform);
	}
}

//-Vector3.Distance(player1.transform.position, player2.transform.position) / 4
/*
notACamera.transform.eulerAngles = new Vector3(90 - (Mathf.Abs(player1.transform.position.z) * 5) - (Mathf.Abs(player2.transform.position.z) * 5), 0, 0);
		if(notACamera.transform.eulerAngles.x < 34)
		{
			notACamera.transform.eulerAngles = new Vector3(34, 0, 0);
		}
*/
//transform.position += new Vector3(0, Mathf.Pow(Vector3.Distance(player1.transform.position, player2.transform.position) / 10, 2) + 10, 0);