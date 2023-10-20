using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управляет запуском рекламы
/// </summary>
public class AdsManager : MonoBehaviour
{
	public static AdsManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	private void ShowInterstitialAds(float delaySecond = 0, Action onComplete = null)
	{
        if(AdsManager.Instance && IAPManager.Instance.CheckBuyNoAds())
		{ 
			onComplete?.Invoke();
			return;
		}

		Debug.Log("Execute: ShowInterstitialAds");

		onComplete?.Invoke();

		//TODO YSO отключено
		//GameManager.Instance.isShowAdsFocus = false;
  //      YsoCorp.GameUtils.YCManager.instance.adsManager.ShowInterstitial(() => {
  //          onComplete?.Invoke();		    
		//}, delaySecond);
	}

	private void ShowRewarderAds(bool isForceRewarded = false, Action onComplete = null)
	{
		Debug.Log("Execute: ShowRewarderAds");

		GameManager.Instance.isShowAdsFocus = false;

		onComplete?.Invoke();

		//TODO YSO отключено
  //      YsoCorp.GameUtils.YCManager.instance.adsManager.ShowRewarded((bool ok) => {
		//	if(ok)
		//		onComplete?.Invoke();	
		//	else if(isForceRewarded)
		//		onComplete?.Invoke();	
		//});
	}

	public void OnShowInterstitialAds_ChangeLevel(Action onComplete = null)
	{
		ShowInterstitialAds(0, onComplete);
	}
}
