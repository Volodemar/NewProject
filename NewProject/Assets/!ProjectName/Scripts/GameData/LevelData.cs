using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
	private float _score				= 0f;
	private float _coins				= 0f;
	private float _money				= 0f;
	private float _crystal				= 0f;

	public string PriorityNextScene		= "";
	
	/// <summary>
	/// Инициализация новой игры
	/// </summary>
    public void Init()
	{
		_score				= 0f;
		_coins				= 0f;
		_money				= 0f;
		_crystal			= 0f;
		PriorityNextScene	= "";
	}

	public float Score(float value = 0, bool isSet = false)
	{
		if(isSet)
			return _score = value;
		else if(value == 0)
			return _score;
		else
			return _score = _score + value > 0 ? _score + value : 0; 
	}

	public float Coins(float value = 0, bool isSet = false)
	{
		if(isSet)
			return _coins = value;
		else if(value == 0)
			return _coins;
		else
			return _coins = _coins + value > 0 ? _coins + value : 0; 
	}

	public float Money(float value = 0, bool isSet = false)
	{
		if(isSet)
			return _money = value;
		else if(value == 0)
			return _money;
		else
			return _money = _money + value > 0 ? _money + value : 0; 
	}

	public float Crystal(float value = 0, bool isSet = false)
	{
		if(isSet)
			return _crystal = value;
		else if(value == 0)
			return _crystal;
		else
			return _crystal = _crystal + value > 0 ? _crystal + value : 0; 
	}
}
