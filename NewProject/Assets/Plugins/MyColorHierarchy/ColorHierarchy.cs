using System;
using UnityEditor;
using UnityEngine;

namespace MyColorHierarchy
{
	public class ColorHierarchy : MonoBehaviour
	{
#if UNITY_EDITOR
		public HeaderSettings headerSettings = new HeaderSettings();

		private void OnValidate()
		{
			EditorApplication.delayCall += _OnValidate;
		}
		private void _OnValidate()
		{
			if (this == null) {
				return;
			}

			headerSettings.headerText = this.gameObject.name;
			EditorApplication.RepaintHierarchyWindow();
		}
#endif
	}
}
