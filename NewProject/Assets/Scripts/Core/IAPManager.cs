using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Доступно после активации Purchase
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.MiniJSON;
*/

/// <summary>
/// Для тестировани Subscribe надо завести подписку на 5 минут в GooglePlay, протестировать и
/// отписаться от подписки в аккаунте Goolge на устройстве. Уточняться в интернете.
/// </summary>

public class IAPManager : MonoBehaviour //, IStoreListener
{
	public static IAPManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	/*
	private static IStoreController		_storeController;
	private static IExtensionProvider	_extensionProvider;

	public static string iapYummysPackConsumableID			= "com.puppy.consumable.yummys_pack";
	public static string iapNoAdsNonConsumableID			= "com.puppy.nonconsumable.no_ads";
	*/

	private void Start()
	{
		/*
		StandardPurchasingModule.Instance().useFakeStoreAlways = true;
		StandardPurchasingModule.Instance().useFakeStoreUIMode = FakeStoreUIMode.DeveloperUser;

		InitializePurchasing();
		*/
	}

	private void InitializePurchasing()
	{
		/*
		ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		builder.AddProduct(iapYummysPackConsumableID, ProductType.Consumable);
		builder.AddProduct(iapNoAdsNonConsumableID, ProductType.NonConsumable);
		UnityPurchasing.Initialize(this, builder);
		*/
	}

	public void BuyYummysPack()
    {
		/*
		ByeProductID(iapYummysPackConsumableID);
		*/
    }

	private void BuyYummysPack_Complete()
	{
		/*
		GameManager.Instance.GameData.PlayerData.ModifyYummys(+50);
		GameManager.Instance.GameData.Save();
		*/
	}

	public void BuyNoAds()
    {
		/*
		ByeProductID(iapNoAdsNonConsumableID);
		*/
    }

	private void BuyNoAds_Complete()
	{
		/*
		PlayerPrefs.SetInt("iapNoADS", 1);
		PlayerPrefs.Save();		

		//обновить кнопки в магазине запретить повторную покупку
		EventManager.OnActionSend(EventManager.ShopPurchased, iapNoAdsNonConsumableID, null);
		GameManager.Instance.GameData.Save();
		*/
	}

	public bool CheckBuyNoAds()
	{
		/*
		#if UNITY_EDITOR
			if(PlayerPrefs.GetInt("iapNoADS", 0) == 1)
				return true;
			else
				return false;	
		#endif

		if (_storeController != null)
		{
			Product product = _storeController.products.WithID(iapNoAdsNonConsumableID);
			if (product != null && product.hasReceipt)
			{
				return true;
			}
		}
		*/
		return false;
	}

	
	private void ByeProductID(string productID)
	{
		/*
		//Отключаем показ рекламы при возврате в приложение
		GameManager.Instance.isShowAdsFocus = false;

		if (_storeController != null)
		{
			Product product = _storeController.products.WithID(productID);
			if (product != null && product.hasReceipt)
			{
				Debug.Log("IAP уже куплено повторная покупка, позволяем купить в Unity: " + product.definition.id);

				if (product.definition.id == iapDoubleProfitNonConsumableID)
				{
					BuyDoubleProfit_Complete();
					return;
				}

				if (product.definition.id == iapNoAdsNonConsumableID)
				{
					BuyNoAds_Complete();
					return;
				}
			}

			if (product != null && product.availableToPurchase)
			{
				Debug.Log("IAP Инициализация покупки: " + product.definition.id);
				_storeController.InitiatePurchase(product);
			}
		}
		*/
	}

	/*
	//Блок проверки покупок
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
	{
		//YummysPack
		else if (string.Equals(e.purchasedProduct.definition.id, iapYummysPackConsumableID, System.StringComparison.Ordinal))
		{
			var receiptDict = (Dictionary<string, object>)Json.Deserialize(e.purchasedProduct.receipt);
			string receiptString = (string)receiptDict["Payload"];

			var payloadDict = (Dictionary<string, object>)Json.Deserialize(receiptString);
			if (payloadDict == null)
			{
				BuyYummysPack_Complete();
				return PurchaseProcessingResult.Complete;
			}

			//Пример проведения через стороннюю SDK
			#region Example
			//#if !UNITY_EDITOR		
				//string originalJson = (string)payloadDict["json"];

				//AnySDK.instance.CheckPay(ItemType.consume, originalJson, (result) => {
				//	Debug.Log("SDK: ticket1ConsumableID checkpay success result:" + result);
				//	_storeController.ConfirmPendingPurchase(e.purchasedProduct);

				//	BuyTicket1_Complete();

				//	return;
				//}, (code, errorMsg) => {
				//	Debug.Log("SDK: ticket1ConsumableID checkpay failed code:"+ code + "; errorMsg:" + errorMsg);
				//	_storeController.ConfirmPendingPurchase(e.purchasedProduct);

				//	BuyTicket1_Complete();

				//	return;		
				//});
			//#endif
			#endregion
		}

		//NoAds
		else if (string.Equals(e.purchasedProduct.definition.id, iapNoAdsNonConsumableID, System.StringComparison.Ordinal))
		{
			var receiptDict = (Dictionary<string, object>)Json.Deserialize(e.purchasedProduct.receipt);
			string receiptString = (string)receiptDict["Payload"];

			var payloadDict = (Dictionary<string, object>)Json.Deserialize(receiptString);
			if (payloadDict == null)
			{
				BuyNoAds_Complete();
				return PurchaseProcessingResult.Complete;
			}
		}

		return PurchaseProcessingResult.Pending;
	}
	*/

	/*
	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		_storeController = controller;
		_extensionProvider = extensions;
	}
	*/

	/*
	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.LogError("Failed to initialize Unity IAP: " + error);
	}
	*/

	/*
	public void OnInitializeFailed(InitializationFailureReason error, string message)
	{
		var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";
		if (!string.IsNullOrEmpty(message))
		{
			errorMessage += $" More details: {message}";
		}

		Debug.Log(errorMessage);
	}
	*/

	/*
	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log("Purchase failed: " + product.definition.id + ", " + failureReason);
	}
	*/
}
