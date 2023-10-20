using UnityEngine;

/// <summary>
/// Базовый объект с доступом к основным элементам игры
/// </summary>
public class BaseGameObject : MonoBehaviour
{
	[HideInInspector] public GameManager GM;
	[HideInInspector] public Joystick J;
	[HideInInspector] public PlayerController Player;
	[HideInInspector] public LevelController Level;
	[HideInInspector] public UIManager UI;
	[HideInInspector] public CameraController Camera;

	[HideInInspector] public bool  isEventCheck = false;

	private bool isObjectInit = false;

	/// <summary>
	/// Example
	/// </summary>
	/*
	public override void GetAction(string ID, object obj, object obj2)
	{
		switch (ID)
		{
			case EventManager.LevelInit:
				InitScene();
				break;
		}
	}
	*/

	/// <summary>
	/// Проверяет полностью доступны объекты в сцене или еще грузится
	/// </summary>
	public bool InitScene()
	{
		if(!isObjectInit)
		{
			bool testIsObjectInit = true;

			if(!GameManager.Instance)
			{ 
				testIsObjectInit = false;
			}
			else
				GM = GameManager.Instance;

			if (!PlayerController.Instance)
			{ 
				testIsObjectInit = false;
			}
			else
				Player = PlayerController.Instance;

			if (!LevelController.Instance)
			{ 
				testIsObjectInit = false;
			}
			else
				Level = LevelController.Instance;

			if(!UIManager.Instance)
			{ 
				testIsObjectInit = false;
			}
			else
				UI = UIManager.Instance;

			if (UI == null || UI.Joystick == null)
			{
				testIsObjectInit = false;
			}
			else
				J = UI.Joystick;

			if(!CameraController.Instance)
			{ 
				testIsObjectInit = false;
			}
			else
				Camera = CameraController.Instance;

			isObjectInit = testIsObjectInit;
		}

		return isObjectInit;
	}

	#region Подписка на обработку событий
		public virtual void OnEnable()
		{
			InitScene();

			if(isEventCheck)
			{ 	
				EventManager.OnAction += GetAction;				
			}
		}

		public virtual void OnDisable()
		{
			if(isEventCheck)
				EventManager.OnAction -= GetAction;
		}

		public virtual void OnDestroy()
		{
			if(isEventCheck)
				EventManager.OnAction -= GetAction;
		}

		/// <summary>
		/// Слушаем события
		/// </summary>
		/// <param name="ID">Идентификатор события</param>
		public virtual void GetAction(string ID, object obj, object obj2)
		{
			switch (ID)
			{
				case EventManager.LevelInit:
					InitScene();
					break;
			}
		}
	#endregion
}
