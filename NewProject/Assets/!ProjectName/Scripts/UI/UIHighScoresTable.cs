using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHighScoresTable : BaseGameObject
{
    public UITableItem Top1;
    public UITableItem Top2;
    public UITableItem Top3;
    public UITableItem Your;
    public UITableItem Low1;
    public UITableItem Low2;

	public override void OnEnable()
	{
		base.OnEnable();
		UpdateTable();
	}

    public void UpdateTable()
	{
        if(InitScene())
		{
            float coins = GM.GameData.PlayerData.Score();
            List<HighScoresItem> itemsAll = GM.GameData.HighScoresData.items.OrderBy(p => p.id).ToList();

            HighScoresItem top1Data = itemsAll[0];
            HighScoresItem top2Data = itemsAll[1];
            HighScoresItem top3Data = itemsAll[2];
            HighScoresItem your     = itemsAll.Where(x => x.coins == coins).First();

            Top1.Place.SetText(top1Data.id.ToString());
            Top1.Name.SetText(top1Data.name);
            Top1.Score.SetText(top1Data.coins.ToString());

            Top2.Place.SetText(top2Data.id.ToString());
            Top2.Name.SetText(top2Data.name);
            Top2.Score.SetText(top2Data.coins.ToString());

            Top3.Place.SetText(top3Data.id.ToString());
            Top3.Name.SetText(top3Data.name);
            Top3.Score.SetText(top3Data.coins.ToString());

            Your.Place.SetText(your.id.ToString());
            Your.Name.SetText(your.name);
            Your.Score.SetText(your.coins.ToString());
            Your.SetColor(Color.green);
            Your.SetScale(10);

            if(itemsAll.Where(x => x.id > your.id).Count() >= 1)
			{
                HighScoresItem low1Data = itemsAll.Where(x => x.id > your.id).ToList()[0];
                Low1.Place.SetText(low1Data.id.ToString());
                Low1.Name.SetText(low1Data.name);
                Low1.Score.SetText(low1Data.coins.ToString());
			}
            else
			{
                Low1.Place.SetText("-----");
                Low1.Name.SetText("-----");
                Low1.Score.SetText("-----");
			}

            if(itemsAll.Where(x => x.id > your.id).Count() >= 2)
			{
                HighScoresItem low2Data = itemsAll.Where(x => x.id > your.id).ToList()[1];
                Low2.Place.SetText(low2Data.id.ToString());
                Low2.Name.SetText(low2Data.name);
                Low2.Score.SetText(low2Data.coins.ToString());
			}
            else
			{
                Low2.Place.SetText("-----");
                Low2.Name.SetText("-----");
                Low2.Score.SetText("-----");
			}
		}
	}
}
