using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadingData", menuName = "ScriptableObjects/LoadingData")]
public class LoadingScriptableObject : ScriptableObject
{
	public bool isResetProgress = false;
	public bool isDebug			= false;
	public bool isCPIVideo		= false;
	public int  setCPICoins		= 20000;

	[Space]
	public bool isLoadART		= false;
	public bool isLoadForCopy	= false;
	public bool isLoadPrototip	= false;
	public bool isLoadCPI		= false;
	public bool isLoadLastPlay  = false;

	[Space]
	public string LastPlayScene	= "";
	public string NameCPILevel  = "LevelCPI";
	public int MaxLevels		= 1;
}
