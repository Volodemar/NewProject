using System;
using System.Collections.Generic;
//using Facebook.Unity;
//using GameAnalyticsSDK;
using UnityEngine;

/// <summary>
/// 
/// 1. Импортировать и настроить SDK AppMetrica
/// 2. Импортировать и настроить SDK GameAnalytics
/// 3. Импортировать и настроить SDK Facebook
/// 4. Скопировать скрипт в проект
/// 5. Добавить скрипт на GameObject на сцене (для трекинга play time)
/// 6. На старте проинициализировать в Start аналитику AppAnalytics.Init(...)
///
/// Далее работать с публичными статическими методами - на текущий момент их должно быть достаточно для теста CPI
///  
/// </summary>
public class AppAnalytics : MonoBehaviour
{
    public static AppAnalytics Instance { get; private set; }

/* //Отключено----------------------------------------------------------------------------------

    private static bool _isGameStarted;
    private static int _sessionNumber;
    private static int _userLevel;
    private static int _lastLevel;
    private float _playTimeSeconds;
    private DateTime _lastPlaytimeStart;
    
    // ключи для префсов
    private const string KeyRegDay = "aa_reg_date";
    private const string KeySessionsCount = "aa_sessions_count";
    private const string KeyPlayTimeSeconds = "aa_playtime_seconds";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        SavePlayTime();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!_isGameStarted)
            return;

        if (pauseStatus)
        {
            SavePlayTime();
        }
        else
        {
            _lastPlaytimeStart = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Вызывается на старте приложения
    /// </summary>
    public static void Init(int userLevel, int lastLevel)
    {
        if (_isGameStarted)
            return;
        
        GameAnalytics.Initialize();
        
        if (!FB.IsInitialized) {
            // Initialize the Facebook SDK
            FB.Init(() =>
            {
                if (FB.IsInitialized) {
                    // Signal an app activation App Event
                    FB.ActivateApp();
                    // Continue with Facebook SDK
                    // ...
                } else {
                    Debug.LogError("Failed to Initialize the Facebook SDK");
                }
            });
        } else {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }

        _userLevel = userLevel;
        _lastLevel = lastLevel;
        
        RegisterUser();
        IncrementSession();
        Instance.LoadPlayTime();
        
        TrackGameStart();

        _isGameStarted = true;
    }
    
    /// <summary>
    /// Сохраняет дату первого запуска игры
    /// </summary>
    private static void RegisterUser()
    {
        if (PlayerPrefs.HasKey(KeyRegDay))
            return;

        PlayerPrefs.SetString(KeyRegDay, DateTime.UtcNow.ToString("O"));
    }

    private static void IncrementSession()
    {
        _sessionNumber = PlayerPrefs.GetInt(KeySessionsCount, 0);
        _sessionNumber++;
        PlayerPrefs.SetInt(KeySessionsCount, _sessionNumber);
    }

    private void LoadPlayTime()
    {
        _playTimeSeconds = PlayerPrefs.GetFloat(KeyPlayTimeSeconds, 0);
        _lastPlaytimeStart = DateTime.UtcNow;
    }

    private void SavePlayTime()
    {
        _playTimeSeconds += (float)(DateTime.UtcNow - _lastPlaytimeStart).TotalSeconds;
        PlayerPrefs.SetFloat(KeyPlayTimeSeconds, _playTimeSeconds);
        PlayerPrefs.Save();
    }
    

    private static int GetDaysInGame()
    {
        string regDate = PlayerPrefs.GetString(KeyRegDay, null);
        var date = string.IsNullOrEmpty(regDate) ? DateTime.UtcNow : DateTime.Parse(regDate).ToUniversalTime();
        var lifeTime = DateTime.UtcNow - date;
        return Mathf.RoundToInt((float)lifeTime.TotalDays);
    }

    public static void UpdateUserLevel(int level)
    {
        _userLevel = level;
    }

    public static void UpdateLastLevel(int level)
    {
        _lastLevel = level;
    }

    private static void SendEvents(string eventName, Dictionary<string, object> parameters, bool forceSendAppMetrica = false)
    {
        // добавление универсальных параметров к каждому ивенту
        if (parameters == null)
            parameters = new Dictionary<string, object>();
        
        TryAddParameter("days_in_game", GetDaysInGame());
        TryAddParameter("session_number", _sessionNumber);
        TryAddParameter("user_level", _userLevel);
        TryAddParameter("last_level", _lastLevel);

        float playTimeSec = Instance._playTimeSeconds + (float)(DateTime.UtcNow - Instance._lastPlaytimeStart).TotalSeconds;
        TryAddParameter("total_playtime_sec", Mathf.RoundToInt(playTimeSec));
        TryAddParameter("total_playtime_min", Mathf.RoundToInt(playTimeSec / 60));

        void TryAddParameter(string name, object value)
        {
            if (parameters.ContainsKey(name))
                return;
            
            parameters.Add(name, value);
        }

#if UNITY_EDITOR
        Debug.Log($"{eventName} : {string.Join(", ", parameters)}");
#endif
        
        if (Application.isEditor)
            return;

        if(!_isGameStarted)
            return;

        // AppMetrica
        AppMetrica.Instance.ReportEvent(eventName, parameters);
        
        if (forceSendAppMetrica)
            AppMetrica.Instance.SendEventsBuffer();
        
        // GameAnalytics
        GameAnalytics.NewDesignEvent(eventName, parameters);
    }
    
    private static void TrackGameStart()
    {
        Dictionary<string, object> prms = new Dictionary<string, object>
        {
            { "count", _sessionNumber },
            { "days since reg", GetDaysInGame()}
        };
        
        SendEvents("game_start", prms);
    }

    private static void SetLevelStartTimer()
	{
        PlayerPrefs.SetString("LevelStartTotalSeconds", DateTime.UtcNow.TotalSeconds().ToString());
        PlayerPrefs.Save();
	}

    private static int GetLevelEndTimer()
	{
        long last = Convert.ToInt64(PlayerPrefs.GetString("LevelStartTotalSeconds"));
        int seconds = (int)(DateTime.UtcNow.TotalSeconds() - last);
        return seconds;
	}

    public static void TrackLevelStart(int levelNumber)
    {
        SetLevelStartTimer();

        Dictionary<string, object> prms = new Dictionary<string, object>
        {
            { "level", levelNumber },
            { "days since reg", GetDaysInGame()}
        };
        
        SendEvents("level_start", prms, true);
    }

    public static void TrackLevelComplete(int levelNumber)
    {
        int timeSpentInSeconds = GetLevelEndTimer();

        Dictionary<string, object> prms = new Dictionary<string, object>
        {
            { "level", levelNumber },
            { "time_spent", timeSpentInSeconds },
            { "days since reg", GetDaysInGame()}
        };
        
        SendEvents("level_complete", prms, true);
    }

    public static void TrackLevelFail(int levelNumber, string reason = null)
    {
        int timeSpentInSeconds = GetLevelEndTimer();

        Dictionary<string, object> prms = new Dictionary<string, object>
        {
            { "level", levelNumber },
            { "time_spent", timeSpentInSeconds },
            { "days since reg", GetDaysInGame()}
        };
        
        if (!string.IsNullOrEmpty(reason))
            prms.Add("reason", reason);
        
        SendEvents("level_fail", prms);
    }

    public static void TrackLevelRestart(int levelNumber)
    {
        Dictionary<string, object> prms = new Dictionary<string, object>
        {
            { "level", levelNumber },
            { "days since reg", GetDaysInGame()}
        };
        
        SendEvents("level_restart", prms);
    }

    public static void TrackOpenMainMenu()
    {
        Dictionary<string, object> prms = new Dictionary<string, object>
        {
            { "days since reg", GetDaysInGame()}
        };

        SendEvents("main_menu", prms);
    }

*/ //Отключено----------------------------------------------------------------------------------
}