using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILevelProgress : BaseGameObject
{
	public TMP_Text TextProgressA;
	public TMP_Text TextProgressB;
	public TMP_Text TextProgressC;
	public TMP_Text TextProgressD;

	public override void OnEnable()
	{
		base.OnEnable();
		UpdateLevelProgress();
	}

	public void UpdateLevelProgress()
	{
		if(Level.currentLevel == 1)
		{
			TextProgressA.SetText("");
			TextProgressB.SetText("1");
			TextProgressC.SetText("2");
			TextProgressD.SetText("3");
		}
		else
		{
			TextProgressA.SetText( (Level.currentLevel-1).ToString() );
			TextProgressB.SetText( (Level.currentLevel).ToString() );
			TextProgressC.SetText( (Level.currentLevel+1).ToString() );
			TextProgressD.SetText( (Level.currentLevel+2).ToString() );
		}
	}
}
