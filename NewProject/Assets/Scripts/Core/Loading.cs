using UnityEngine;
using UnityEngine.SceneManagement;
//using Facebook.Unity;
//using GameAnalyticsSDK;

public class Loading : MonoBehaviour
{
	public GameObject DontDestroyGameData;
	public GameObject DontDestroyEventManager;

	public LoadingScriptableObject LoadingData;

	private void Start()
	{
		//Загрузка данных игры
		GameData GD = DontDestroyGameData.GetComponent<DontDestroyGameData>().GameData;

		Lexic.NameGenerator generator = this.GetComponent<Lexic.NameGenerator>();

		if(LoadingData.isResetProgress)
			GD.NewGame(generator);
		else if(LoadingData.isCPIVideo)
			GD.NewCPIGame(generator, LoadingData.setCPICoins);
		else
			GD.Load(generator);

		//Запрет уничтожения объекта с данными
		DontDestroyOnLoad(DontDestroyGameData);
		DontDestroyOnLoad(DontDestroyEventManager);

		//Добавление неудаляемого объекта в ссылки
		ObjectExtension.DontDestroyOnLoad(DontDestroyGameData); 

		int currentLevel	= GD.PlayerData.GetCurrentLevel();
		int lastLevel		= GD.PlayerData.GetLastFinishedLevel();
		//AppAnalytics.Init(currentLevel, lastLevel);

		if(LoadingData.isLoadART)
			SceneManager.LoadScene("LevelART");
		else if(LoadingData.isLoadForCopy)
			SceneManager.LoadScene("LevelForCopy");
		else if(LoadingData.isLoadPrototip)
			SceneManager.LoadScene("Prototip");
		else if(LoadingData.isLoadLastPlay && LoadingData.LastPlayScene != "")
			SceneManager.LoadScene(LoadingData.LastPlayScene);
		else if(LoadingData.isLoadCPI)
			SceneManager.LoadScene(LoadingData.NameCPILevel);
		else
		{ 
			int level = GD.PlayerData.GetCurrentLevel();
			if(level == 0 || level >= LoadingData.MaxLevels)
				SceneManager.LoadScene("Level1");
			else
				SceneManager.LoadScene("Level" + level.ToString());
		}
	}
}
