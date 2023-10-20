using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonUpgrade : BaseGameObject
{
	public GameObject upgradeTable;
	public Text textCost;
	public Text textLevel;

    public void OnByeUpgrade()
	{
		//if(CheckSceneComplete())
		//{
		//	int nextLevel = GM.GameData.PlayerData.GetPurchaseUpgradeTimer()+1;
		//	if(nextLevel < GM.GameData.DataBase.UpgradeTimerData.Count)
		//	{
		//		float cost = GM.GameData.DataBase.GetUpgradeTimer(nextLevel).cost;
		//		if(GM.GameData.PlayerData.GetMoney() >= cost)
		//		{ 
		//			GM.GameData.PlayerData.ModifyMoney(-cost);					
		//			GM.GameData.PlayerData.SetPurchaseUpgradeTimer(nextLevel);

		//			Level.InitLevelTimer();

		//			EventManager.OnActionSend(EventManager.ChangedUpgradeTimer, null, null);
		//			//GM.GameData.PlayerData.SetPlayerEnergy(GM.GameData.DataBase.GetUpgradeTimer(nextLevel).addSeconds);

		//			//AppAnalytics.SpendingOfMoney(Level.currentLevel,"UpgradeTimer " + nextLevel.ToString(), (int)cost);
		//		}
		//	}
		//}
	}

	public void RefreshButton()
	{
		//if(CheckSceneComplete())
		//{
		//	Button thisButton = this.GetComponent<Button>();

		//	int nextLevel = GM.GameData.PlayerData.GetPurchaseUpgradeTimer()+1;
		//	if(nextLevel < GM.GameData.DataBase.UpgradeTimerData.Count)
		//	{
		//		float cost = GM.GameData.DataBase.GetUpgradeTimer(nextLevel).cost;
		//		if(GM.GameData.PlayerData.GetMoney() >= cost)
		//		{ 
		//			upgradeTable.SetActive(true);
		//			thisButton.interactable = true;
		//		}
		//		else
		//		{
		//			upgradeTable.SetActive(false);
		//			thisButton.interactable = false;
		//		}
		//	}
		//	else
		//	{		
		//		upgradeTable.SetActive(false);
		//		thisButton.interactable = false;
		//	}

		//	UpdateTexts();
		//}
	}

	public void UpdateTexts()
	{
		//int nextLevel = GM.GameData.PlayerData.GetPurchaseUpgradeTimer()+1;
		//textLevel.text = "LEVEL " + nextLevel.ToString();

		//SOUpgradeTimer item = GM.GameData.DataBase.GetUpgradeTimer(nextLevel);
		//if(item != null)
		//{
		//	textCost.text = item.cost.ToString();
		//}
		//else
		//{
		//	textCost.text = "max";
		//}
	}
}
