using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lexic;

[Serializable]
public class SaveHighScoresData 
{
    [SerializeField] public List<HighScoresItem> items;
}

public class HighScoresData
{
    public List<HighScoresItem> items;

    public void Generate(NameGenerator generator)
	{
        items = new List<HighScoresItem>();

		if(generator != null)
		{ 
			int rndCoins = 1;
			for(int i=10;i>0;i--)
			{
				rndCoins    = rndCoins + UnityEngine.Random.Range(999999,999999);
				string rndName  = generator.GetNextRandomName();

				HighScoresItem newItem = new HighScoresItem();
				newItem.id     = i;
				newItem.name   = rndName;
				newItem.coins  = rndCoins;
				items.Add(newItem);
			}

			items = items.OrderBy(x => x.id).ToList();
		}
	}

	public void AddScore(int coins)
	{
		List<HighScoresItem> itemsSort = items.OrderBy(x => x.id).ToList();
		List<HighScoresItem> itemsTop = itemsSort.Where(x => x.coins > coins).ToList();
		List<HighScoresItem> itemsLow = itemsSort.Where(x => x.coins <= coins).ToList();
		List<HighScoresItem> newItemsLow = new List<HighScoresItem>();

		HighScoresItem yourItem = new HighScoresItem();
		int place = itemsTop[itemsTop.Count-1].id+1;
		if(place > 10000)
			place = 10000;

		yourItem.id     = place;
		yourItem.name   = "Your";
		yourItem.coins  = coins;

		foreach(HighScoresItem item in itemsLow)
		{
			HighScoresItem newItem = new HighScoresItem();
			newItem.id     = yourItem.id+newItemsLow.Count+1;
			newItem.name   = item.name;
			newItem.coins  = item.coins;

			if(newItem.id <= 10000)
				newItemsLow.Add(newItem);
		}

		items.Clear();
		items.AddRange(itemsTop);
		items.Add(yourItem);
		items.AddRange(newItemsLow);
	}

	public void Save()
	{
		SaveHighScoresData save = new SaveHighScoresData();
		save.items = items;

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath+"/HighScoresData.json", json);
	}

	public void Load(NameGenerator generator)
	{
		if(File.Exists(Application.persistentDataPath+"/HighScoresData.json"))
		{ 
			string json = File.ReadAllText(Application.persistentDataPath+"/HighScoresData.json");
			SaveHighScoresData load = JsonUtility.FromJson<SaveHighScoresData>(json);
			items = load.items;

			if(items == null)
				Generate(generator);
		}
		else
		{
			Generate(generator);
		}
	}
}

public class HighScoresItem
{
    public int id;
    public string name;
    public int coins;
}
