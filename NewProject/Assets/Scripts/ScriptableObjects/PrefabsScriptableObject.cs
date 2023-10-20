using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabsData", menuName = "ScriptableObjects/PrefabsData")]
public class PrefabsScriptableObject : ScriptableObject
{
	public GameObject ui_money;
	public GameObject gui_text;
	public List<ItemsScriptableObject> items;
}
