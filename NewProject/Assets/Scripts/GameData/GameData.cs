using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lexic;

public class GameData
{
	public PlayerData PlayerData = new PlayerData();
	public LevelData LevelData = new LevelData();
	public HighScoresData HighScoresData = new HighScoresData();
	public DataBase DataBase;

	public void Save()
	{
		PlayerData.Save();
		HighScoresData.Save();

		Debug.Log("GameData is Save...");
	}

	public void Load(NameGenerator generator = null)
	{
		PlayerData.Load();
		HighScoresData.Load(generator);

		Debug.Log("GameData is Load...");
	}

	public void NewGame(NameGenerator generator)
	{
		PlayerData.NewGamePlayerData();
		LevelData.NewGameLevelData();
		HighScoresData.Generate(generator);

		Save();
	}

	public void NewCPIGame(NameGenerator generator, int coins = 0)
	{
		PlayerData.NewGamePlayerData();
		LevelData.NewGameLevelData();
		PlayerData.SetCoins(coins);
		HighScoresData.Generate(generator);
		Save();
	}
}
