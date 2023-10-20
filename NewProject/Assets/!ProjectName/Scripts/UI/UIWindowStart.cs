using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWindowStart : UIWindow
{
	public TMP_Text TextLevel;

	private void Start()
	{
		TextLevel.SetText($"LEVEL {Level.currentLevel}");
	}
}
