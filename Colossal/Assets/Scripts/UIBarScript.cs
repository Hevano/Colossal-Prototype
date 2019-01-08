using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarScript : MonoBehaviour {

	public Slider p1_healthbar;
	public Slider p2_healthbar;
	public Slider p1_healthbarUnderlay;
	public Slider p2_healthbarUnderlay;
	public Slider p1_Chargebar;
	public Slider p2_Chargebar;


	public void HealthChange(int player,float value) 
	{
		if(player == 0)
		{
			p1_healthbar.value = value / 100f;
		}
		else
		{
			p2_healthbar.value = value / 100f;
		}
		
	}
	public void ChargeChange(int player, float value)
	{
		if(player == 0)
		{
			p1_Chargebar.value = value / 100f;
		}
		else
		{
			p2_Chargebar.value = value / 100f;
		}
	}

	void FixedUpdate()
	{
		if(p1_healthbar.value != p1_healthbarUnderlay.value)
		{
			p1_healthbarUnderlay.value -= 0.005f;
			if(p1_healthbarUnderlay.value < p1_healthbar.value)
			{
				p1_healthbarUnderlay.value = p1_healthbar.value;
			}
		}
		if(p2_healthbar.value != p2_healthbarUnderlay.value)
		{
			p2_healthbarUnderlay.value -= 0.005f;
			if(p2_healthbarUnderlay.value < p2_healthbar.value)
			{
				p2_healthbarUnderlay.value = p2_healthbar.value;
			}
		}
	}
}
