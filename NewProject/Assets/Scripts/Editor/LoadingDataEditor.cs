using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Добавляет настройки в меню
/// </summary>

public class LoadingDataEditor
{
    [MenuItem("Tools/LoadingSettings")]
    public static void OpenLoadingData()
    {
        LoadingScriptableObject data = AssetDatabase.LoadAssetAtPath<LoadingScriptableObject>("Assets/DataBase/LoadingData.asset");
        Selection.activeObject = data;
    }
}