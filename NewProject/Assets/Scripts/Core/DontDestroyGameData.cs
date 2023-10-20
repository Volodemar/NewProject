using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данные игры
/// </summary>
public class DontDestroyGameData : MonoBehaviour
{
	[HideInInspector] public GameData GameData;
	public DataBase DataBase;

	private void Awake()
	{
		GameData = new GameData();
		GameData.DataBase = DataBase;
	}
}
