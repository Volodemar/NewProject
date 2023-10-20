using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
public class ItemsScriptableObject : ScriptableObject
{
	public int			Index		= -1;
	public int			Level		= 1;
	public string		Type		= "Тип1";
	public int			TypeIndex	= -1;
	public GameObject	prefab;
	public Sprite		sprite;
}
