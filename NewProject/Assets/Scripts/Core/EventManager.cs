using System;

public static class EventManager
{
	/// <summary>
	/// Основные объекты проинициализированы, инициализация уровня еще не завершена
	/// </summary>
	public const string SceneComplete = "SceneComplete";

	/// <summary>
	/// Инициализация уровня завершена
	/// </summary>
	public const string LevelInit = "LevelInit";

	/// <summary>
	/// Инициализация уровня завершена
	/// </summary>
	public const string LevelStart = "LevelStart";

	/// <summary>
	/// Изменено количество монет
	/// </summary>
	public const string ChangedCoins = "ChangedCoins";

	/// <summary>
	/// Изменено количество денег
	/// </summary>
	public const string ChangedMoney = "ChangedMoney";

	/// <summary>
	/// Изменено количество кристаллов
	/// </summary>
	public const string ChangedCrystal = "ChangedCrystal";

	/// <summary>
	/// Проигрыш уровня
	/// </summary>
	public const string LevelFailed = "LevelFailed";

	/// <summary>
	/// Пройден уровень
	/// </summary>
	public const string LevelWin = "LevelWin";

	/// <summary>
	/// Куплен апгрейд 1
	/// </summary>
	public const string ChangedUpgrade1 = "ChangedUpgrade1";

	/// <summary>
	/// Куплен апгрейд 2
	/// </summary>
	public const string ChangedUpgrade2 = "ChangedUpgrade2";

	/// <summary>
	/// Куплен апгрейд 3
	/// </summary>
	public const string ChangedUpgrade3 = "ChangedUpgrade3";

	public static Action<string, object, object> OnAction;

	public static void OnActionSend(string ID, object obj, object obj2)
	{
		OnAction?.Invoke(ID, obj, obj2);
	}
}
