using UnityEngine;

/// <summary>
/// Базовый объект с доступом к основным элементам игры
/// </summary>
public class BaseGameObject : MonoBehaviour
{
	[SerializeField] private bool  isEventCheck = false;

	#region Ленивая инициализация свойств
		private GameManager _gm;
		private bool _isGM_Init;
		public GameManager GM 
		{
			get
			{
				if (!_isGM_Init)
				{
					if (!GameManager.Instance)
					{
						_gm = FindObjectOfType<GameManager>();
					}
					else
					{
						_gm = GameManager.Instance;
					}

					_isGM_Init = true;
				}
				return _gm;
			}
		}

		private PlayerController _player;
		private bool _isPlayer_Init;
		public PlayerController Player 
		{
			get
			{
				if (!_isPlayer_Init)
				{
					if (!PlayerController.Instance)
					{
						_player = FindObjectOfType<PlayerController>();
					}
					else
					{
						_player = PlayerController.Instance;
					}

					_isPlayer_Init = true;
				}
				return _player;
			}
		}

		private LevelController _level;
		private bool _isLevel_Init;
		public LevelController Level 
		{
			get
			{
				if (!_isLevel_Init)
				{
					if (!LevelController.Instance)
					{
						_level = FindObjectOfType<LevelController>();
					}
					else
					{
						_level = LevelController.Instance;
					}

					_isLevel_Init = true;
				}
				return _level;
			}
		}

		private UIManager _ui;
		private bool _isUI_Init;
		public UIManager UI 
		{
			get
			{
				if (!_isUI_Init)
				{
					if (!UIManager.Instance)
					{
						_ui = FindObjectOfType<UIManager>();
					}
					else
					{
						_ui = UIManager.Instance;
					}

					_isUI_Init = true;
				}
				return _ui;
			}
		}

		private Joystick _j;
		private bool _isJoystic_Init;
		public Joystick J 
		{
			get
			{
				if (!_isJoystic_Init)
				{
					if (UI.Joystick == null)
					{
						_j = FindObjectOfType<Joystick>();
					}
					else
					{
						_j = UI.Joystick;
					}

					_isJoystic_Init = true;
				}
				return _j;
			}
		}

		private CameraController _camera;
		private bool _isCamera_Init;
		public CameraController Camera 
		{
			get
			{
				if (!_isCamera_Init)
				{
					if (!CameraController.Instance)
					{
						_camera = FindObjectOfType<CameraController>();
					}
					else
					{
						_camera = CameraController.Instance;
					}

					_isCamera_Init = true;
				}
				return _camera;
			}
		}

		private AudioManager _am;
		private bool _isAM_Init;
		public AudioManager Audio 
		{
			get
			{
				if (!_isAM_Init)
				{
					if (!AudioManager.Instance)
					{
						_am = FindObjectOfType<AudioManager>();
					}
					else
					{
						_am = AudioManager.Instance;
					}

					_isAM_Init = true;
				}
				return _am;
			}
		}
	#endregion Ленивая инициализация свойств

	#region Подписка на обработку событий
		public virtual void OnEnable()
		{
			if(isEventCheck)
				EventManager.OnAction += GetAction;					
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
		public virtual void GetAction(int ID, object obj, object obj2)
		{
			//if(ID == EventManager.LevelInit)
				//Debug.Log("Инициализация сцены")
		}

		/* Example
		public override void GetAction(string ID, object obj, object obj2)
		{
			switch (ID)
			{
				case EventManager.ChangedPlayerEnergy:
					ChangeEnergyBar();
					break;
			}
		}
		*/
	#endregion
}
