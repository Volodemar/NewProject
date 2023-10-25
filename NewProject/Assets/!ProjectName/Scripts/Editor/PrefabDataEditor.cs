#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PrefabsScriptableObject))]
public class PrefabDataEditor : Editor
{
	PrefabsScriptableObject prefabsData = null;
    protected void OnEnable()
    {
        prefabsData = (PrefabsScriptableObject)target;
    }

	public override void OnInspectorGUI()
	{
        base.OnInspectorGUI();

		if (GUILayout.Button("Generate ID for Items"))
		{
			for (int i = 0; i < prefabsData.items.Count; i++)
			{
				prefabsData.items[i].Index = i;
				EditorUtility.SetDirty(prefabsData.items[i]);

				if (prefabsData.items[i].Type == "Тип1")
					prefabsData.items[i].TypeIndex = 0;
				else if (prefabsData.items[i].Type == "Тип2")
					prefabsData.items[i].TypeIndex = 1;
				else if (prefabsData.items[i].Type == "Тип3")
					prefabsData.items[i].TypeIndex = 2;
				else if (prefabsData.items[i].Type == "Тип4")
					prefabsData.items[i].TypeIndex = 3;
				else if (prefabsData.items[i].Type == "Тип5")
					prefabsData.items[i].TypeIndex = 4;
			}
		}

		if (GUI.changed) 
		{
			// записываем изменения над PrefabData в Undo
			Undo.RecordObject(prefabsData, "PrefabData Modify!");
			// помечаем тот самый PrefabData как "грязный" и сохраняем.
			EditorUtility.SetDirty(prefabsData); 
		}
	}
}

#endif