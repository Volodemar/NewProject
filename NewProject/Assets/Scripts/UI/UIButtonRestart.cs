﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonRestart : BaseGameObject
{
	public void OnRestartLevel()
	{
		if(InitScene())
		{ 
			GM.OnRestartLevel();
		}
	}
}
