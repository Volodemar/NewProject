#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

namespace MyColorHierarchy
{
	[System.Serializable]
	public class ColorHierarchyPreset : ScriptableObject
	{
		public List<HeaderSettings> coloredHeaderPreset = new List<HeaderSettings>();
	}
}

#endif