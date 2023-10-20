using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
	Init,
	Paused,
	Play
}

public static class StatusManager 
{
	private static GameStatus gameStatus		= GameStatus.Init;
	private static GameStatus gameLastStatus	= GameStatus.Init;

	public static bool IsGameInit 
	{
		get {return gameStatus == GameStatus.Init;}
	}

	public static bool IsGamePaused 
	{
		get {return gameStatus == GameStatus.Paused;}
	}

	public static bool IsGamePlay 
	{
		get {return gameStatus == GameStatus.Play;}
	}

	public static void SetGameStatus(GameStatus newStatus)
	{
		if(gameStatus == newStatus)
			return;

		gameLastStatus = gameStatus;
		gameStatus = newStatus;
	}
}
