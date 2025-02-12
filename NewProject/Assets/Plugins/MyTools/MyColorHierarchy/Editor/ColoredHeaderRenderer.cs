using MyColorHierarchy;
using UnityEditor;
using UnityEngine;

namespace MyColorHierarchyEditor
{
	[InitializeOnLoad]
	public static class ColoredHeaderRenderer
	{
		static ColoredHeaderRenderer()
		{
			EditorApplication.hierarchyWindowItemOnGUI += DisplayHeader;
		}

		private static void DisplayHeader(int instanceID, Rect selectionRect)
		{
			var sceneObject = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

			if (sceneObject == null) {
				return;
			}

			var headerComponent = sceneObject.GetComponent<ColorHierarchy>();
			if (headerComponent != null) {
				RenderHeaders(headerComponent, selectionRect);
			}
		}

		private static void RenderHeaders(ColorHierarchy header, Rect selectionRect)
		{
			var headerSettings = header.headerSettings;
			var headerText = headerSettings.headerText;
			var headerColor = headerSettings.headerColor;
			var textAlignment = headerSettings.textAlignmentOptions;
			var fontStyle = headerSettings.fontStyleOptions;
			var fontSize = headerSettings.fontSize;
			var fontColor = headerSettings.fontColor;
			var dropShadow = headerSettings.dropShadow;
			var textOffcet = headerSettings.textOffset;
			var backOffcet = headerSettings.buckgroundOffset;

			var headerFontStyle = new GUIStyle(EditorStyles.label);
			switch (textAlignment) {
				case TextAlignmentOptions.Center:
					headerFontStyle.alignment = TextAnchor.MiddleCenter;
					break;
				case TextAlignmentOptions.Left:
					headerFontStyle.alignment = TextAnchor.MiddleLeft;
					break;
				case TextAlignmentOptions.Right:
					headerFontStyle.alignment = TextAnchor.MiddleRight;
					break;
				default:
					Debug.LogError(header.gameObject.name + " has a invalid text alignment selected.");
					Selection.activeObject = header.gameObject;
					return;
			}

			switch (fontStyle) {
				case FontStyleOptions.Bold:
					headerFontStyle.fontStyle = FontStyle.Bold;
					break;
				case FontStyleOptions.Normal:
					headerFontStyle.fontStyle = FontStyle.Normal;
					break;
				case FontStyleOptions.Italic:
					headerFontStyle.fontStyle = FontStyle.Italic;
					break;
				case FontStyleOptions.BoldAndItalic:
					headerFontStyle.fontStyle = FontStyle.BoldAndItalic;
					break;
				default:
					Debug.LogError(header.gameObject.name + " has a invalid font style selected.");
					Selection.activeObject = header.gameObject;
					return;
			}

			headerFontStyle.fontSize = Mathf.FloorToInt(fontSize);
			headerFontStyle.normal.textColor = fontColor;

			var color = new Color(headerColor.r, headerColor.g, headerColor.b, headerColor.a);
			if (dropShadow) {
				Rect overlayRect = new Rect(selectionRect.x + backOffcet.x, selectionRect.y + backOffcet.y, selectionRect.width + 20, selectionRect.height);

				EditorGUI.DrawRect(overlayRect, color);
				EditorGUI.DropShadowLabel(overlayRect, headerText.ToString(), headerFontStyle);
			}
			else {
				Rect overlayRect = new Rect(selectionRect.x + backOffcet.x, selectionRect.y + backOffcet.y, selectionRect.width + 20, selectionRect.height);

				EditorGUI.DrawRect(overlayRect, color);
				EditorGUI.LabelField(overlayRect, headerText.ToString(), headerFontStyle);
			}
		}
	}
}
