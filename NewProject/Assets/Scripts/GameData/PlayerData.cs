using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavePlayerData 
{
    [SerializeField] public float Coins				= 0;
    [SerializeField] public float Money				= 0;
    [SerializeField] public float Crystal			= 0;
	[SerializeField] public float CurrentLevel		= 5;

	//[SerializeField] public int PurchaseUpgrade1			= 0;
	//[SerializeField] public int PurchaseUpgrade2			= 0;
	//[SerializeField] public int PurchaseUpgrade3			= 0;
}

public class PlayerData
{
	private float CurrentLevel				= 0;
	private float Coins						= 0f;
	private float Money						= 0f;
	private float Crystal					= 0f;

	//private int PurchaseUpgrade1		= 0;
	//private int PurchaseUpgrade2		= 0;
	//private int PurchaseUpgrade3		= 0;

	/// <summary>
	/// Инициализация новой игры
	/// </summary>
    public void NewGamePlayerData()
	{
		//PurchaseUpgrade1			= 0;
		//PurchaseUpgrade2			= 0;
		//PurchaseUpgrade3			= 0;
		CurrentLevel				= 0;
		Coins						= 0f;
		Money						= 0f;
		Crystal						= 0f;
	}

	public void Save()
	{
		SavePlayerData save = new SavePlayerData();
		save.Coins						= Coins;
		save.Money						= Money;
		save.Crystal					= Crystal;
		save.CurrentLevel				= CurrentLevel;
		//save.PurchaseUpgrade1			= PurchaseUpgrade1;
		//save.PurchaseUpgrade2			= PurchaseUpgrade2;
		//save.PurchaseUpgrade3			= PurchaseUpgrade3;

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath+"/PlayerData.json", json);
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath+"/PlayerData.json"))
		{ 
			string json = File.ReadAllText(Application.persistentDataPath+"/PlayerData.json");
			SavePlayerData load = JsonUtility.FromJson<SavePlayerData>(json);
			CurrentLevel				= load.CurrentLevel;
			Coins						= load.Coins;
			Money						= load.Money;
			Crystal						= load.Crystal;
			//PurchaseUpgrade1			= load.PurchaseUpgrade1;
			//PurchaseUpgrade2			= load.PurchaseUpgrade2;
			//PurchaseUpgrade3			= load.PurchaseUpgrade3;
		}
		else
		{
			NewGamePlayerData();
		}
	}

	//public int GetPurchaseUpgrade1()
	//{
	//	return PurchaseUpgrade1;
	//}

	//public int GetPurchaseUpgrade2()
	//{
	//	return PurchaseUpgrade2;
	//}

	//public int GetPurchaseUpgrade3()
	//{
	//	return PurchaseUpgrade3;
	//}

	public int GetCurrentLevel()
	{
		return (int)CurrentLevel;
	}

	public int GetLastFinishedLevel()
	{
		int lastLevel = (int)CurrentLevel - 1;

		if(lastLevel < 0)
			lastLevel = 0;

		return lastLevel;
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

		EventManager.OnActionSend(EventManager.ChangedCoins, Coins, value);
	}

	public void ModifyMoney(float value)
	{
		float newValue = Money + value;

		if(newValue < 0)
			newValue = 0;

		Money = newValue;

		EventManager.OnActionSend(EventManager.ChangedMoney, Money, value);
	}

	public void ModifyCrystal(float value)
	{
		float newValue = Crystal + value;

		if(newValue < 0)
			newValue = 0;

		Crystal = newValue;

		EventManager.OnActionSend(EventManager.ChangedCrystal, Crystal, value);
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

	public void SetCurrentLevel(int value)
	{
		CurrentLevel = value;
	}

	//public void SetPurchaseUpgrade1(int value)
	//{
	//	PurchaseUpgrade1 = value;
	//	EventManager.OnActionSend(EventManager.ChangedUpgrade1, PurchaseUpgrade1, null);
	//}

	//public void SetPurchaseUpgrade2(int value)
	//{
	//	PurchaseUpgrade2 = value;
	//	EventManager.OnActionSend(EventManager.ChangedUpgrade2, PurchaseUpgrade2, null);
	//}

	//public void SetPurchaseUpgrade3(int value)
	//{
	//	PurchaseUpgrade3 = value;
	//	EventManager.OnActionSend(EventManager.ChangedUpgrade3, PurchaseUpgrade3, null);
	//}
}
