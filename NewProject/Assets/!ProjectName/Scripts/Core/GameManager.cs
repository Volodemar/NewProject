using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public LoadingScriptableObject LoadingData;

	[HideInInspector] public bool isSceneCorrect = false;
    [HideInInspector] public GameData GameData;

	public bool isShowAdsFocus = true;

	private void Awake()
	{
		Instance = this;

		bool isTrueLoading = false;

		//Поиск сохраненного объекта
		foreach (GameObject ddObject in ObjectExtension.GetSavedObjects())
		{
			if (ddObject.TryGetComponent<DontDestroyGameData>(out DontDestroyGameData DDGameData))
			{
				GameData = DDGameData.GameData;

				isTrueLoading = true;
			}
		}

		if(SceneManager.GetActiveScene().name != "Loading")
			LoadingData.LastPlayScene = SceneManager.GetActiveScene().name;

		//Если загрузка сцены идет напрямую, делаем петлю через Loader
		if (!isTrueLoading)
		{
			SceneManager.LoadScene("Loading");
		}
		else
		{ 
			Canvas[] canvases = Resources.FindObjectsOfTypeAll<Canvas>();
			foreach(Canvas canvas in canvases)
				if(canvas.gameObject.name == "-----UI-----")
					canvas.gameObject.SetActive(true);
		}
	}

	private void OnApplicationFocus(bool focus)
	{
#if !UNITY_EDITOR
		if(focus)
		{
			if(isShowAdsFocus)
			{ 
				isShowAdsFocus = false;

				//Показ рекламы при входе в игру
				//AdsManager.instance.ShowInterstitialAds(0, () = > {});			
			}
			else
			{
				isShowAdsFocus = true;
			}
		}
		else
		{
			if (TouchScreenKeyboard.visible)
			{
				isShowAdsFocus = false;
			}
		}
#endif
	}

	public void OnRestartLevel()
	{
		//AppAnalytics.TrackLevelRestart(LevelController.Instance.currentLevel);

		StopAllCoroutines();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void OnStartNextLevel()
	{
		int currentLevel = GameData.PlayerData.CurrentLevel;
		string priorityNextScene = GameData.LevelData.PriorityNextScene;

		StopAllCoroutines();
		if(priorityNextScene == "")
		{ 
			if(currentLevel == 0 || currentLevel >= GameData.DataBase.LoadingData.MaxLevels)
				SceneManager.LoadScene("Level1");
			else
				SceneManager.LoadScene("Level" + (currentLevel + 1).ToString());
		}
		else
		{
			SceneManager.LoadScene(priorityNextScene);
		}
	}

	public void OnLoadGameData()
	{
		GameData.Load();

		Debug.Log("GameData is Load");
	}

	public void OnSaveGameData()
	{
		GameData.Save();

		Debug.Log("GameData is Save");
	}
}
