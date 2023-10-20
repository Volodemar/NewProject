using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "ScriptableObjects/DataBase")]
public class DataBase : ScriptableObject
{
	public VFXScriptableObject VFXData;
	public PrefabsScriptableObject PrefabsData;
	public LoadingScriptableObject LoadingData;
}
