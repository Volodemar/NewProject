using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavePlayerData 
{
	[SerializeField] public float	Score				= 0;
    [SerializeField] public float	Coins				= 0;
    [SerializeField] public float	Money				= 0;
    [SerializeField] public float	Crystal				= 0;
	[SerializeField] public int		CurrentLevel		= 5;
}

public class PlayerData
{
	private int   _currentLevel				= 0;
	private float _score					= 0f;
	private float _coins					= 0f;
	private float _money					= 0f;
	private float _crystal					= 0f;

	/// <summary>
	/// Инициализация новой игры
	/// </summary>
    public void Init()
	{
		_currentLevel				= 0;
		_score						= 0;
		_coins						= 0f;
		_money						= 0f;
		_crystal					= 0f;
	}

	public int CurrentLevel { get; set; }
	public int LastLevel	{ get => _currentLevel-1<0?0:_currentLevel-1; }

	public void Save()
	{
		SavePlayerData save = new SavePlayerData();
		save.CurrentLevel				= _currentLevel;
		save.Score						= _score;
		save.Coins						= _coins;
		save.Money						= _money;
		save.Crystal					= _crystal;
		

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath+"/PlayerData.json", json);
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath+"/PlayerData.json"))
		{ 
			string json = File.ReadAllText(Application.persistentDataPath+"/PlayerData.json");
			SavePlayerData load = JsonUtility.FromJson<SavePlayerData>(json);
			_currentLevel				= load.CurrentLevel;
			_score						= load.Score;
			_coins						= load.Coins;
			_money						= load.Money;
			_crystal					= load.Crystal;
		}
		else
		{
			Init();
		}
	}

	public float Score(float value = 0, bool isSet = false)
	{
		if(isSet)
			return _score = value;
		else if(value == 0)
			return _score;
		else
		{ 
			float newValue = _score = _score + value > 0 ? _score + value : 0;
			EventManager.OnActionSend(EventManager.ChangeScore, _score, value);
			return newValue; 
		}
	}

	public float Coins(float value = 0, bool isSet = false)
	{
		if(isSet)
			return _coins = value;
		else if(value == 0)
			return _coins;
		else
		{ 
			float newValue = _coins = _coins + value > 0 ? _coins + value : 0;
			EventManager.OnActionSend(EventManager.ChangedCoins, _coins, value);
			return newValue; 
		}
	}

	public float Money(float value = 0, bool isSet = false)
	{
		if(isSet)
			return _money = value;
		else if(value == 0)
			return _money;
		else
		{ 
			float newValue = _money = _money + value > 0 ? _money + value : 0;
			EventManager.OnActionSend(EventManager.ChangedMoney, _money, value);
			return newValue; 
		}
	}

	public float Crystal(float value = 0, bool isSet = false)
	{
		if(isSet)
			return _crystal = value;
		else if(value == 0)
			return _crystal;
		else
		{ 
			float newValue = _crystal = _crystal + value > 0 ? _crystal + value : 0;
			EventManager.OnActionSend(EventManager.ChangedCrystal, _crystal, value);
			return newValue; 
		}
	}
}
