using UnityEngine;
using UnityEngine.SceneManagement;
//using Facebook.Unity;
//using GameAnalyticsSDK;

public class Loading : MonoBehaviour
{
	public GameObject GameData;
	public GameObject EventManager;

	public LoadingScriptableObject LoadingData;

	private void Start()
	{
		//Загрузка данных игры
		GameData GD = GameData.GetComponent<DontDestroyGameData>().GameData;

		Lexic.NameGenerator generator = this.GetComponent<Lexic.NameGenerator>();

		if(LoadingData.isResetProgress)
			GD.NewGame(generator);
		else if(LoadingData.isCPIVideo)
			GD.NewCPIGame(generator, LoadingData.setCPICoins);
		else
			GD.Load(generator);

		//Запрет удаления и ссылка на объект
		ObjectExtension.DontDestroyOnLoad(GameData); 
		ObjectExtension.DontDestroyOnLoad(EventManager);

		int currentLevel	= GD.PlayerData.CurrentLevel;
		int lastLevel		= GD.PlayerData.LastLevel;
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
			int level = GD.PlayerData.CurrentLevel;
			if(level == 0 || level >= LoadingData.MaxLevels)
				SceneManager.LoadScene("Level1");
			else
				SceneManager.LoadScene("Level" + level.ToString());
		}
	}
}
