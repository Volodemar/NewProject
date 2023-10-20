using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
	private float Coins					= 0f;
	private float Money					= 0f;
	private float Crystal				= 0f;

	public string PriorityNextScene		= "";
	
	/// <summary>
	/// Инициализация новой игры
	/// </summary>
    public void NewGameLevelData()
	{
		Coins				= 0f;
		Money				= 0f;
		Crystal				= 0f;
		PriorityNextScene	= "";
	}

	public float GetCoins()
	{
		return Coins;
	}

	public float GetMoney()
	{
		return Money;
	}

	public float GetCrystal()
	{
		return Crystal;
	}

	public void ModifyCoins(float value)
	{
		float newValue = Coins + value;

		if(newValue < 0)
			newValue = 0;

		Coins = newValue;
	}

	public void ModifyMoney(float value)
	{
		float newValue = Money + value;

		if(newValue < 0)
			newValue = 0;

		Money = newValue;
	}

	public void ModifyCrystal(float value)
	{
		float newValue = Crystal + value;

		if(newValue < 0)
			newValue = 0;

		Crystal = newValue;
	}

	public void SetCoins(float value)
	{
		Coins = value;
	}

	public void SetMoney(float value)
	{
		Money = value;
	}

	public void SetCrystal(float value)
	{
		Crystal = value;
	}
}
