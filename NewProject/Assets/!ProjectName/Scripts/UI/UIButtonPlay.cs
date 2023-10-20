using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonPlay : BaseGameObject
{
    public void OnPlay()
	{
		Level.OnLevelStart();
		UI.OnShowUIWindowGame(true);
		UI.OnShowUIWindowStart(false);
	}
}
