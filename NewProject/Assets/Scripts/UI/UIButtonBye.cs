using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIButtonBye : BaseGameObject
{
    public TMP_Text textCost;
	public TMP_Text textLevel;
	public Image image;

	//private GunScriptableObject _gun;

    public void OnByeUpgrade()
	{
	//	if (CheckSceneComplete())
	//	{
	//		if (GM.GameData.PlayerData.GetCoins() >= _gun.randomCost && _gun.isPurchased == false)
	//		{
	//			_gun.isPurchased = true;
	//			GM.GameData.PlayerData.ModifyCoins(-_gun.randomCost);
	//			GM.GameData.PlayerData.SetPurchaseUpgradeTimer(nextLevel);

	//			Player.SetUpgradeGun(_gun.model);

	//			AppAnalytics.SpendingOfMoney(Level.currentLevel, "UpgradeTimer " + nextLevel.ToString(), (int)cost);

	//			EventManager.OnActionSend(EventManager.WeaponBye, null, null);
	//		}
	//	}
	}

	public void RefreshButton(/*GunScriptableObject gun*/)
	{
	//	if (CheckSceneComplete())
	//	{
	//		textCost.text = gun.randomCost.ToString();
	//		image.sprite = gun.sprite;

	//		Button thisButton = this.GetComponent<Button>();

	//		if(GM.GameData.PlayerData.GetCoins() >= gun.randomCost && gun.isPurchased == false)
	//		{
	//			thisButton.interactable = true;
	//		}
	//		else
	//		{
	//			thisButton.interactable = false;
	//		}

	//		_gun = gun;
	//	}
	}
}
