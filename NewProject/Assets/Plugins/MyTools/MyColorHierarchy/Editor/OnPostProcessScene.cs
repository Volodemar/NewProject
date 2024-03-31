using UnityEditor;
using UnityEditor.Callbacks;

namespace MyColorHierarchyEditor
{
	public static class OnPreprocessScene
	{
		[PostProcessScene]
		private static void OnPostProcessScene()
		{
			if (EditorApplication.isPlaying) {
				return;
			}

			EditorHelper.DeleteAllHeaders();
		}
	}
}
