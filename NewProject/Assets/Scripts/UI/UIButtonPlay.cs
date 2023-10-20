using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonPlay : BaseGameObject
{
    public void OnPlay()
	{
		if(InitScene())
		{
			Level.OnLevelStart();
			//Camera.cameraGif.OnShowGif(false);
			UI.OnShowUIWindowGame(true);
			UI.OnShowUIWindowStart(false);
		}
	}
}
