using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListScript : MonoBehaviour {

	public Text nameText;
	public RawImage[] imageStates;
	public RawImage confirmImg;

	void Awake()
	{
		nameText.gameObject.SetActive(false);
		imageStates[1].gameObject.SetActive(false);
		print("why");
	}

	public void FillSlot(string playerName)
	{

		print("boink");
		nameText.text = playerName;
		nameText.gameObject.SetActive(true);
		imageStates[1].gameObject.SetActive(true);
		print(imageStates[1].gameObject.activeSelf);
	}

}
