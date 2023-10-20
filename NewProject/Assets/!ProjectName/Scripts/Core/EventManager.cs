using System;

public static class EventManager
{
	public static int SceneComplete		=> 0;

	public static int LevelInit			=> 1;
	public static int LevelStart		=> 2;
	public static int LevelFailed		=> 3;
	public static int LevelWin			=> 4;

	public static int ChangeScore		=> 5;
	public static int ChangedCoins		=> 6;
	public static int ChangedMoney		=> 7;
	public static int ChangedCrystal	=> 8;

	public static Action<int, object, object> OnAction;

	public static void OnActionSend(int ID, object obj, object obj2)
	{
		OnAction?.Invoke(ID, obj, obj2);
	}
}
