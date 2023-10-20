using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class LevelController : BaseGameObject
{
    public static LevelController Instance { get; private set; }

    public int currentLevel;
	public string priorityNextScene;

	private bool isActive = false;
	private List<TMP_Text> poolText = new List<TMP_Text>();

	private void Awake()
	{
        Instance = this;
    }

	private void Start()
	{
		Application.targetFrameRate = 30;
		StartCoroutine(OnLevelInit());
	}

	/// <summary>
	/// Инициализируем уровень
	/// </summary>
	private IEnumerator OnLevelInit()
	{
		InitScene();

		EventManager.OnActionSend(EventManager.SceneComplete, null, null);

		// Инициализация окружения
		GM.GameData.LevelData.SetCoins(0);
		GM.GameData.PlayerData.SetCurrentLevel(currentLevel);	
		GM.GameData.LevelData.PriorityNextScene = priorityNextScene;
		J.gameObject.SetActive(false);
		Player.Init();
		Player.SetPlayerActive(false);

		// Включение стартового экрана
		UI.gameObject.SetActive(true);
		UI.OnShowUIWindowStart(true);

		Debug.Log("Level is Initialize!");

		GM.GameData.Save();

		if(GM.LoadingData.isDebug)
			UI.UIWindowDebug.gameObject.SetActive(true);
		else
			UI.UIWindowDebug.gameObject.SetActive(false);

		EventManager.OnActionSend(EventManager.LevelInit, null, null);

		yield return null;
	}

	/// <summary>
	/// Стартуем уровень
	/// </summary>
	public void OnLevelStart()
	{	
		J.gameObject.SetActive(true);
		Player.SetPlayerActive(true);

		Debug.Log("Level is Started!");

		// Сохранение покупок когда запускаем уровень
		GM.GameData.Save();

		StartCoroutine(LevelTimer(1f));

		//AppAnalytics.TrackLevelStart(currentLevel);

		EventManager.OnActionSend(EventManager.LevelStart, null, null);
	}

	/// <summary>
	/// Таймер каждую секунду, минуту и т.д.
	/// </summary>
	private IEnumerator LevelTimer(float timeInSec)
	{
		isActive = true;

		while(isActive)
		{ 
			yield return new WaitForSeconds(timeInSec);

			CheckLevelEnd();
		}
	}

	/// <summary>
	/// Проверка завершения уровня
	/// </summary>
	private void CheckLevelEnd()
	{
		//OnLevelFailed();
		//OnLevelWin();
	}

	/// <summary>
	/// Победа
	/// </summary>
	public void OnLevelWin()
	{
		isActive = false;

		int coins = Random.Range(1, 11);
		GM.GameData.PlayerData.ModifyCoins(coins);
		GM.GameData.LevelData.ModifyCoins(coins);	
		GM.GameData.HighScoresData.AddScore((int)GM.GameData.PlayerData.GetCoins());

		//AppAnalytics.AccrualOfMoney(currentLevel, (int)GM.GameData.LevelData.GetLevelCoins());
		//AppAnalytics.TrackLevelComplete(currentLevel);

		EventManager.OnActionSend(EventManager.LevelWin, null, null);
		UI.OnShowUIWindowGame(false);
		UI.OnShowUIWindowWin(true);
		J.gameObject.SetActive(false);
		Player.SetPlayerActive(false);

		// Сбрасываем купленные апгрейды
		//GM.GameData.PlayerData.SetPurchaseUpgrade1(0);
		//GM.GameData.PlayerData.SetPurchaseUpgrade2(0);
		//GM.GameData.PlayerData.SetPurchaseUpgrade3(0);

		GM.GameData.Save();
	}

	public void OnLevelFailed()
	{
		isActive = false;

		int coins = Random.Range(1, 11);
		GM.GameData.PlayerData.ModifyCoins(coins);
		GM.GameData.LevelData.ModifyCoins(coins);
		GM.GameData.HighScoresData.AddScore((int)GM.GameData.PlayerData.GetCoins());

		//AppAnalytics.AccrualOfMoney(currentLevel, (int)GM.GameData.LevelData.GetLevelCoins());
		//AppAnalytics.TrackLevelFail(currentLevel, "failed reason");

        EventManager.OnActionSend(EventManager.LevelFailed, null, null);
		UI.OnShowUIWindowGame(false);
		UI.OnShowUIWindowFailed(true);
		J.gameObject.SetActive(false);
		Player.SetPlayerActive(false);

		GM.GameData.Save();
	}

	/// <summary>
	/// Пулл отлетающих текстов
	/// </summary>
	public void SpawnText(Vector3 pos, int money)
	{
		//Если уже есть свободный элемент, то используем его. 
		if(poolText.Where(x => !x.gameObject.activeSelf).Count() > 0)
		{ 
			TMP_Text text = poolText.Where(x => !x.gameObject.activeSelf).First();
			text.transform.position = pos;
			text.SetText("+" + money);
			text.gameObject.SetActive(true);

			//Анимация
			text.transform.DOLocalMoveY(pos.y+3,1).OnComplete(() => { text.gameObject.SetActive(false); });
		}
		else
		{
			TMP_Text text = Instantiate(GM.GameData.DataBase.PrefabsData.gui_text, pos, Quaternion.identity, this.transform).GetComponent<TMP_Text>();
			text.SetText("+" + money);
			poolText.Add(text);

			//Анимация
			text.transform.DOLocalMoveY(pos.y+3,1).OnComplete(() => { text.gameObject.SetActive(false); });
		}
	}
}
